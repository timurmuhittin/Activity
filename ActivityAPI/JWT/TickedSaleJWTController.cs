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
    public class TickedSaleJWTController : Controller
    {
        [HttpPost]
        public IActionResult GetToken(TickedSale tickedSale)
        {
            ActivityContext context = new ActivityContext();
            TickedSale newTickedSale = new TickedSale();

            newTickedSale.CompanyName= tickedSale.CompanyName;
            newTickedSale.Email= tickedSale.Email;
            newTickedSale.Password= tickedSale.Password;

            if (newTickedSale.Email == newTickedSale.Email && newTickedSale.Password == newTickedSale.Password)
            {
                List<Claim> claims = new List<Claim>();

                claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, tickedSale.Email));

                claims.Add(new Claim(ClaimTypes.Role, "TickedSale"));

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
