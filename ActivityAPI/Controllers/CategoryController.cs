using ActivityAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ActivityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        [HttpGet]
        public IActionResult GetCity()
        {
            ActivityContext context = new ActivityContext();
            var query = from cat in context.Categories
                        select new
                        {
                            Id = cat.CategoryId,
                            Category=cat.Category1
                        };
            return Ok(query);
        }
        [HttpPost]
        public IActionResult Create(Category category)

        {
            ActivityContext context = new ActivityContext();

            Category newCategory= new Category();
            newCategory.Category1 = category.Category1;


            context.Categories.Add(newCategory);
            context.SaveChanges();

            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Category eklenemedi!");

            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, Category category)

        {
            ActivityContext context = new ActivityContext();
            Category originalCategory = context.Categories.Find(id);
            originalCategory.Category1 = category.Category1;
            context.SaveChanges();
            return Ok();

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ActivityContext context = new ActivityContext();
            Category category = context.Categories.Find(id);
            context.Categories.Remove(category);
            context.SaveChanges();
            return NoContent();
        }


    }
}
