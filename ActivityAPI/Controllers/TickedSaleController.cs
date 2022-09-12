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
    public class TickedSaleController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetTickedSale()
        {
            ActivityContext context = new ActivityContext();
            var query = from ts in context.TickedSales
                        select new
                        {
                            Id = ts.TickedSalesId,
                            Email = ts.Email,
                            CompanyName = ts.CompanyName,
                            Password=ts.Password,

                        };
            return Ok(query);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetTickedSaleByID(int id)
        {
            ActivityContext context = new ActivityContext();
            var query = from ts in context.TickedSales
                        where ts.TickedSalesId == id
                        select new
                        {
                            Id = ts.TickedSalesId,
                            Email = ts.Email,
                            CompanyName = ts.CompanyName,
                            Password = ts.Password,

                        };
            if (query == null)
            {
                return NotFound();
                //return BadRequest();
            }
            else
            {
                return Ok(query);
            }
        }
        [HttpPost]
        public IActionResult Create(TickedSale tickedSale)

        {
            ActivityContext context = new ActivityContext();

            TickedSale newTickedSale = new TickedSale();
            newTickedSale.CompanyName = tickedSale.CompanyName;
            newTickedSale.Password = tickedSale.Password;
            newTickedSale.Email = tickedSale.Email;

            context.TickedSales.Add(newTickedSale);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetTickedSaleByID), new { id = newTickedSale.TickedSalesId }, newTickedSale);

        }

        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, TickedSale tickedSale)

        {
            ActivityContext context = new ActivityContext();
            TickedSale originalTicketSale= context.TickedSales.Find(id);
            originalTicketSale.CompanyName = tickedSale.CompanyName;
            originalTicketSale.Password = tickedSale.Password;
            originalTicketSale.Email = tickedSale.Email;
            context.SaveChanges();
            return Ok();

        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            ActivityContext context = new ActivityContext();
            TickedSale tickedSale = context.TickedSales.Find(id);
            context.TickedSales.Remove(tickedSale);
            context.SaveChanges();
            return NoContent();
        }


    }
}
