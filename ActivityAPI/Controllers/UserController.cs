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

    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUser()
        {
            ActivityContext context = new ActivityContext();

            var query = from u in context.Users
                        select new
                        {
                            Id = u.UserId,
                            Ad = u.FirstName,
                            Soyad = u.LastName,
                            Email = u.Email,
                            Şifre = u.Password,

                        };
            return Ok(query);

        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUserByID(int id)

        {
            ActivityContext context = new ActivityContext();
            var query = from u in context.Users
                        where u.UserId == id
                        select new
                        {
                            Id = u.UserId,
                            Ad = u.FirstName,
                            Soyad = u.LastName,
                            Email = u.Email,
                            Şifre = u.Password,

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
        public IActionResult Create(User user)

        {
            ActivityContext context = new ActivityContext();

            User newUser = new User();
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;
            newUser.Email = user.Email;
            newUser.Password = user.Password;

            context.Users.Add(newUser);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetUserByID), new { id = newUser.UserId }, newUser);

        }

        

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id,User user)
        {
            ActivityContext context = new ActivityContext();
            User originalUser = context.Users.Find(id);
            originalUser.FirstName= user.FirstName;
            originalUser.LastName= user.LastName;
            originalUser.Email = user.Email;
            originalUser.Password = user.Password;
            context.SaveChanges();
            return Ok();

        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] 
        public IActionResult Delete(int id)
        {
            ActivityContext context = new ActivityContext();
            User user = context.Users.Find(id);
            context.Users.Remove(user);
            context.SaveChanges();
            return NoContent();
        }
    }
}

