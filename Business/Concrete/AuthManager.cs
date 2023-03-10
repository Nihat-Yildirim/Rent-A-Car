using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        ITokenHelper _tokenHelper;
        IUserService _userService;

        public AuthManager(ITokenHelper tokenHelper, IUserService userService)
        {
            _tokenHelper = tokenHelper;
            _userService = userService;
        }

        public IDataResult<Token> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user).Data;
            var accessToken = _tokenHelper.CreateAccessToken(user, claims);

            var userRefreshToken = new RefreshToken
            {
                RefreshTokenValue = user.RefreshToken,
                RefreshTokenEndDate = user.RefreshTokenEndDate
            };

            return new SuccessDataResult<Token>(new Token
            {
                AccessToken = accessToken,
                RefreshToken = userRefreshToken
            });
        }

        public IDataResult<Token> CreateToken(User user)
        {
            var claims = _userService.GetClaims(user).Data;
            var accessToken = _tokenHelper.CreateAccessToken(user, claims);
            var refreshToken = _tokenHelper.CreateRefreshToken();
            _userService.UpdateUserRefreshToken(user, refreshToken);
            return new SuccessDataResult<Token>(new Token
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email).Data;
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>();
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>();
            }

            return new SuccessDataResult<User>(userToCheck);
        }

        public IDataResult<User> RefreshTokenToLogin(string refreshToken)
        {
            var resultUser = _userService.GetByRefreshToken(refreshToken).Data;
            var result = BusinessRules.Run(CheckIfRefreshTokenEndDate(resultUser.RefreshTokenEndDate));
            if (result != null)
            {
                return new ErrorDataResult<User>();
            }
            return new SuccessDataResult<User>(resultUser);
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var refreshTokenResult = _tokenHelper.CreateRefreshToken();
            var user = new User
            {
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Email = userForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RefreshToken = refreshTokenResult.RefreshTokenValue,
                RefreshTokenEndDate = refreshTokenResult.RefreshTokenEndDate,
                Status = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user);
        }

        public IResult UserExcists(string email)
        {
            if (_userService.GetByMail(email).Data != null)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

        private IResult CheckIfRefreshTokenEndDate(DateTime refreshTokenEndDate)
        {
            if (refreshTokenEndDate == DateTime.Now)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
    }
}