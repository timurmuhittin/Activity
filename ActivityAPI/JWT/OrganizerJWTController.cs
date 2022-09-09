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
    public class OrganizerJWTController : Controller
    {
        [HttpPost]
        public IActionResult GetToken(Organizer organizer)
        {
            ActivityContext context = new ActivityContext();
            Organizer newOrganizer = new Organizer();

            newOrganizer.FirstName = organizer.FirstName;
            newOrganizer.LastName = organizer.LastName;
            newOrganizer.Email = organizer.Email;
            newOrganizer.Password = organizer.Password;

            if (newOrganizer.Email == organizer.Email && newOrganizer.Password == organizer.Password)
            {
                List<Claim> claims = new List<Claim>();

                claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, organizer.Email));

                claims.Add(new Claim(ClaimTypes.Role, "Organizer"));

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
