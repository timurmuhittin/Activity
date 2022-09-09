using ActivityAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ActivityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CityController : ControllerBase
    {
        
        [HttpGet]
        public IActionResult GetCity()
        {
            ActivityContext context = new ActivityContext();
            var query = from c in context.Cities
                        select new
                        {
                            Id = c.CityId,
                            sehir=c.City1,

                        };
            return Ok(query);
        }


        [HttpGet("{id}")]
        public IActionResult GetCityByID(int id)

        {
            ActivityContext context = new ActivityContext();
            var query = from c in context.Cities
                        where c.CityId == id
                        select new
                        {
                            Id=c.CityId,
                            sehir=c.City1

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
        public IActionResult Create(City city)

        {
            ActivityContext context = new ActivityContext();

            City newCity= new City();
            newCity.City1 = city.City1;
           

            context.Cities.Add(newCity);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetCityByID), new { id = newCity.CityId }, newCity);

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, City city)

        {
            ActivityContext context = new ActivityContext();
            City originalCity = context.Cities.Find(id);
            originalCity.City1 = city.City1;
            context.SaveChanges();
            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ActivityContext context = new ActivityContext();
            City city= context.Cities.Find(id);
            context.Cities.Remove(city);
            context.SaveChanges();
            return NoContent();
        }

    }
}
