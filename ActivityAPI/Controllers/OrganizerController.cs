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
