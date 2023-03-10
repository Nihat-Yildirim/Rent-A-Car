using Business.Abstract;
using Core.Aspect.Autofac.Performance;
using Core.Aspect.Autofac.Transaction;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [TransactionScopeAspect]
        [PerformanceAspect(10)]
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult();
        }

        [PerformanceAspect(15)]
        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        [PerformanceAspect(15)]
        public IDataResult<User> GetByRefreshToken(string refreshToken)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.RefreshToken == refreshToken));
        }

        [PerformanceAspect(15)]
        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        [PerformanceAspect(10)]
        public IResult UpdateUserRefreshToken(User user, RefreshToken refreshToken)
        {
            user.RefreshToken = refreshToken.RefreshTokenValue;
            user.RefreshTokenEndDate = refreshToken.RefreshTokenEndDate;
            _userDal.Update(user);
            return new SuccessResult();
        }

    }
}