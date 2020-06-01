using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LTCSDL.BLL;
using LTCSDL.Common.Req;
using LTCSDL.Common.Rsp;
using LTCSDL.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LTCSDL.Web.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class DangNhapController : ControllerBase
    {
        public DangNhapController(IOptions<JWTSettings> jwtsettings) {
            _svc = new DangNhapSvc();
            _jwtsettings = jwtsettings.Value;
            _context = new MyPhamContext();
        }

        [HttpPost("get-by-id")]
        public IActionResult getDangNhapyID([FromBody] SimpleReq req)
        {
            var res = new SingleRsp();
            res = _svc.Read(req.Id);
            return Ok(res);
        }


        [HttpPost("get-by-userName")]
        public IActionResult LoginAsync([FromBody] LoginReq req)
        
        {
            var res = new SingleRsp();

            User user = _svc.findByUserNameAndPassWord(req);
            /*UserWithToken userWithToken = null;*/

            if (user != null)
            {
/*                RefreshToken refreshToken = GenerateRefreshToken();
*//*                user.RefreshTokenNavigation.Add(refreshToken);
*//*                _context.RefreshToken.Add(refreshToken);
*/                _context.SaveChangesAsync();

                /*userWithToken = new UserWithToken(user);*/
/*                user.RefreshToken = refreshToken.Token;
*/                user.AccessToken = GenerateAccessToken(user);
            }
            else {
                res.SetMessage("Invalid Username or Password");
            }

            /*if (userWithToken == null)
            {
                res.SetMessage("Cannot create token");
            }*/
            /*else
            {
                userWithToken.AccessToken = GenerateAccessToken(user);
            }*/

            //sign your token here here..
            res.Data = user;
            return Ok(res);
        }



        [HttpPost("create-new-user")]
        public IActionResult CreateNewUser([FromBody]CreateNewUserAccountReq req)
        {
            var res = new SingleRsp();
            res = _svc.CreateNewUser(req);
            return Ok(res);
        }

        [HttpPost("update-user-imformation")]
        public IActionResult UpdateUser([FromBody]CreateNewUserAccountReq req)
        {
            var res = new SingleRsp();
            res = _svc.UpdateUser(req);
            return Ok(res);
        }

        [HttpPost("remove-user")]

        public IActionResult deleteUserName([FromBody] CreateNewUserAccountReq req)
        {
            var res = new SingleRsp();
            res = _svc.RemoveUser(req);
            return Ok(res);
        }

        /*[HttpPost("RefreshToken")]
        public async Task<ActionResult<User>> RefreshToken([FromBody] RefreshRequest refreshRequest)
        {
            User user = await GetUserFromAccessToken(refreshRequest.AccessToken);

            if (user != null && ValidateRefreshToken(user, refreshRequest.RefreshToken))
            {
                *//*UserWithToken userWithToken = new UserWithToken(user);*//*
                user.AccessToken = GenerateAccessToken(user);

                return user;
            }

            return null;
        }*/

        


       /* private bool ValidateRefreshToken(User user, string refreshToken)
        {

            RefreshToken refreshTokenUser = _context.RefreshToken.Where(rt => rt.Token == refreshToken)
                                                .OrderByDescending(rt => rt.ExpiryDate)
                                                .FirstOrDefault();

            if (refreshTokenUser != null && refreshTokenUser.UserId == user.Id
                && refreshTokenUser.ExpiryDate > DateTime.UtcNow)
            {
                return true;
            }

            return false;
        }*/
        /*private RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken();

            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken.Token = Convert.ToBase64String(randomNumber);
            }
            refreshToken.ExpiryDate = DateTime.UtcNow.AddDays(1);

            return refreshToken;
        }*/

        //Chuyển respone sang token
        private string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //Mã hóa thông tin
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
                    new Claim(ClaimTypes.Role, Convert.ToString(user.Role.Code)),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        private async Task<User> GetUserFromAccessToken(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                SecurityToken securityToken;
                var principle = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);

                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var userId = principle.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var roleID = principle.FindFirst(ClaimTypes.Role)?.Value;

                }
            }
            catch (Exception)
            {
                return new User();
            }

            return new User();
        }




        private readonly DangNhapSvc _svc;
        private readonly JWTSettings _jwtsettings;
        private readonly MyPhamContext _context;

    }
}
