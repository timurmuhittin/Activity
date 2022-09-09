using ActivityAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ActivityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizerController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetOrganizer()
        {
            ActivityContext context = new ActivityContext();
            var query = from o in context.Organizers
                        select new
                        {
                            Id = o.OrganizerId,
                            FirstName = o.FirstName,
                            LastName = o.LastName,
                            Email = o.Email,
                            Password = o.Password,
                        };
            return Ok(query);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetOrganizerByID(int id)

        {
            ActivityContext context = new ActivityContext();
            var query = from o in context.Organizers
                        where o.OrganizerId == id
                        select new
                        {
                            Id = o.OrganizerId,
                            FirstName = o.FirstName,
                            LastName = o.LastName,
                            Email = o.Email,
                            Password = o.Password,
                        };
            if (query == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(query);
            }
        }
        [HttpPost]
        public IActionResult Create(Organizer organizer)

        {
            ActivityContext context = new ActivityContext();

            Organizer newOrganizer = new Organizer();
            newOrganizer.FirstName = organizer.FirstName;
            newOrganizer.LastName = organizer.LastName;
            newOrganizer.Email = organizer.Email;
            newOrganizer.Password = organizer.Password;

            context.Organizers.Add(newOrganizer);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetOrganizerByID), new { id = newOrganizer.OrganizerId }, newOrganizer);

        }
        //[HttpPost]
        ////[Route("~/token")]
        //public IActionResult GetToken(Organizer organizer)
        //{
        //    ActivityContext context = new ActivityContext();
        //    Organizer newOrganizer= new Organizer();

        //    newOrganizer.FirstName = organizer.FirstName;
        //    newOrganizer.LastName =organizer.LastName;
        //    newOrganizer.Email =organizer.Email;
        //    newOrganizer.Password =organizer.Password;


        //    if (newOrganizer.Email == organizer.Email && newOrganizer.Password == organizer.Password)
        //    {
        //        List<Claim> claims = new List<Claim>();

        //        claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, organizer.Email));

        //        claims.Add(new Claim(ClaimTypes.Role, "Organizer"));

        //        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        //        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("2t82u5b2t82u5b2t82u5b2t82u5b2t82u5b"));
        //        SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //        JwtSecurityToken token = new JwtSecurityToken(
        //            issuer: "www.gauss.com",
        //            audience: "www.gauss.com",
        //            claims: claims,
        //            signingCredentials: signingCredentials,
        //            expires: DateTime.Now.AddMinutes(30)
        //        );

        //        string jwt = handler.WriteToken(token);
        //        return Ok(jwt);

        //    }

        //    return NotFound("Kullanıcı bulunamadı");
        //}

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, Organizer organizer)

        {
            ActivityContext context = new ActivityContext();

            Organizer originalOrganizer = context.Organizers.Find(id);
            originalOrganizer.FirstName = organizer.FirstName;
            originalOrganizer.LastName = organizer.LastName;
            originalOrganizer.Email = organizer.Email;
            originalOrganizer.Password = organizer.Password;
            context.SaveChanges();
            return Ok();

        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            ActivityContext context = new ActivityContext();
            Organizer organizer = context.Organizers.Find(id);
            context.Organizers.Remove(organizer);
            context.SaveChanges();
            return NoContent();
        }


    }
}
