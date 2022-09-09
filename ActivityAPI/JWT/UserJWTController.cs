using ActivityAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ActivityAPI.JWT
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserJWTController : Controller
    {
        [HttpPost]
        //[Route("~/token")]
        public IActionResult GetToken(User user)
        {
            ActivityContext context = new ActivityContext();
            User newUser = new User();

            newUser.Email = user.Email;
            newUser.Password = user.Password;
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;


            if (newUser.Email == user.Email && newUser.Password == user.Password)
            {
                List<Claim> claims = new List<Claim>();

                claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.Email));

                claims.Add(new Claim(ClaimTypes.Role, "User"));

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("2t82u5b2t82u5b2t82u5b2t82u5b2t82u5b"));
                SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: "www.gauss.com",
                    audience: "www.gauss.com",
                    claims: claims,
                    signingCredentials: signingCredentials,
                    expires: DateTime.Now.AddMinutes(30)
                );

                string jwt = handler.WriteToken(token);
                return Ok(jwt);

            }

            return NotFound("Kullanıcı bulunamadı");
        }
    }
}
