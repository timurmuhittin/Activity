using ActivityAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ActivityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TickedTypeController : Controller
    {
        [HttpGet]
        public IActionResult GetTickedType()
        {
            ActivityContext context = new ActivityContext();
            var query = from tp in context.TickedTypes
                        select new
                        {
                            Id = tp.TickedTypeId,
                            TickedType = tp.TickedType1,

                        };
            return Ok(query);
        }

        [HttpPost]
        public IActionResult Create(TickedType tickedType)

        {
            ActivityContext context = new ActivityContext();

            TickedType newTickedType = new TickedType();
            newTickedType.TickedType1 = tickedType.TickedType1;


            context.TickedTypes.Add(newTickedType);
            context.SaveChanges();

            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Bilet türü eklenemedi!");

            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, TickedType tickedType)

        {
            ActivityContext context = new ActivityContext();
            TickedType originalTickedType = context.TickedTypes.Find(id);
            originalTickedType.TickedType1 = tickedType.TickedType1;
            context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ActivityContext context = new ActivityContext();
            TickedType tickedType = context.TickedTypes.Find(id);
            context.TickedTypes.Remove(tickedType);
            context.SaveChanges();
            return NoContent();
        }

    }
}
