using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest("Hatalı şifre ya da parola");
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);

            if (!result.Success)
            {
                return BadRequest("Token oluşturulamadı");
            }

            return Ok(result.Data);
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExcists = _authService.UserExcists(userForRegisterDto.Email);
            if (!userExcists.Success)
            {
                return BadRequest("Bu kullanıcı zaten var");
            }

            var registerResult = _authService.Register(userForRegisterDto);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("refreshtokenlogin")]
        public IActionResult RefreshTokenLogin(string refreshToken)
        {
            var userToLogin = _authService.RefreshTokenToLogin(refreshToken);

            if (!userToLogin.Success)
            {
                return BadRequest("Hatalı veya süresi geçmiş refresh token değeri");
            }

            var token = _authService.CreateAccessToken(userToLogin.Data);

            if (!token.Success)
            {
                return BadRequest("Token oluşturulamadı");
            }
            return Ok(token);
        }
    }
}
