using ActivityAPI.Models;
using ActivityAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ActivityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ActivityController : ControllerBase
    {
        
        [HttpGet]
        public IActionResult GetActivity()
        {
            ActivityContext context = new ActivityContext();
            var query = from a in context.Activities
                        join ct in context.Categories on a.CategoryId equals ct.CategoryId
                        join o in context.Organizers on a.OrganizerId equals o.OrganizerId
                        join c in context.Cities on a.CityId equals c.CityId
                        join tp in context.TickedTypes on a.TickedTypeId equals tp.TickedTypeId
                        join ts in context.TickedSales on a.TickedSalesId equals ts.TickedSalesId
                        select new
                        {
                            ActivityID=a.ActivityId,
                            Category=ct.Category1,
                            Organizer=o.FirstName+" "+o.LastName,
                            ActivityName=a.ActivityName,
                            Date = a.Date,
                            DateDeadline=a.DateDeadline,
                            City =c.City1,
                            Description =a.Description,
                            Adress=a.Adress,
                            TickedType=tp.TickedType1,
                            Amout=a.Amout,
                            Price=a.TickedPrice,
                            TickedSale=ts.CompanyName
                        };
            return Ok(query);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetActivityByID(int id)

        {
            ActivityContext context = new ActivityContext();
            var query = from a in context.Activities
                        join ct in context.Categories on a.CategoryId equals ct.CategoryId
                        join o in context.Organizers on a.OrganizerId equals o.OrganizerId
                        join c in context.Cities on a.CityId equals c.CityId
                        join tp in context.TickedTypes on a.TickedTypeId equals tp.TickedTypeId
                        join ts in context.TickedSales on a.TickedSalesId equals ts.TickedSalesId
                        where a.ActivityId==id
                        select new
                        {
                            ActivityID = a.ActivityId,
                            Category = ct.Category1,
                            Organizer = o.FirstName + " " + o.LastName,
                            ActivityName = a.ActivityName,
                            Date = a.Date,
                            DateDeadline = a.DateDeadline,
                            City = c.City1,
                            Description = a.Description,
                            Adress = a.Adress,
                            TickedType = tp.TickedType1,
                            Amout = a.Amout,
                            Price = a.TickedPrice,
                            TickedSale = ts.CompanyName
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
        [Authorize(Roles = "Admin,Organizer")]
        [HttpPost]
        public IActionResult Create(ActivityViewModel activity)

        {
            ActivityContext context = new ActivityContext();

            Activity newActivity = new Activity();
            newActivity.CategoryId = activity.CategoryId;
            newActivity.OrganizerId = activity.OrganizerId;
            newActivity.ActivityName = activity.ActivityName;
            newActivity.Date = activity.Date;
            newActivity.DateDeadline = activity.DateDeadline;
            newActivity.CityId = activity.CityId;
            newActivity.Description = activity.Description;
            newActivity.Adress = activity.Adress;
            newActivity.TickedTypeId = activity.TickedTypeId;
            //context.Activities.Add(new Activity
            //{
            //    ActivityId = activity.ActivityId,
            //    CategoryId = activity.CategoryId,
            //    OrganizerId = activity.OrganizerId,
            //    ActivityName = activity.ActivityName,
            //    Date = activity.Date,
            //    DateDeadline = activity.DateDeadline,
            //    CityId = activity.CityId,
            //    Description = activity.Description,
            //    Adress = activity.Adress,
            //    TickedTypeId =activity.TickedTypeId,

            //});

            context.Activities.Add(newActivity);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetActivityByID), new { id = newActivity.ActivityId}, newActivity);
        }
        [Authorize(Roles = "Admin,Organizer")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, ActivityViewModel activity)

        {
            ActivityContext context = new ActivityContext();

            Activity newActivity = context.Activities.Find(id);
            newActivity.CategoryId = activity.CategoryId;
            newActivity.OrganizerId = activity.OrganizerId;
            newActivity.ActivityName = activity.ActivityName;
            newActivity.Date = activity.Date;
            newActivity.DateDeadline = activity.DateDeadline;
            newActivity.CityId = activity.CityId;
            newActivity.Description = activity.Description;
            newActivity.Adress = activity.Adress;
            newActivity.TickedTypeId = activity.TickedTypeId;
            

            context.SaveChanges();
            return Ok();

        }
        [Authorize(Roles = "Admin,Organizer")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ActivityContext context = new ActivityContext();
            Activity activity = context.Activities.Find(id);
            context.Activities.Remove(activity);
            context.SaveChanges();
            return NoContent();
        }




    }
}
