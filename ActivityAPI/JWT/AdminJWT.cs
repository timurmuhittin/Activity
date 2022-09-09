using ActivityAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ActivityAPI.JWT
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminJWT : Controller
    {
        [HttpPost]
        public IActionResult GetToken(Admin admin)
        {
            if (admin.Username == "marty" && admin.Password == "123")
            {
                List<Claim> claims = new List<Claim>();
                ;
                claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, admin.Username));

                claims.Add(new Claim(ClaimTypes.Role, "Admin"));

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
