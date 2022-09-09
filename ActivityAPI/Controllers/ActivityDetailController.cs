using ActivityAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ActivityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityDetailController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin,Organizer")]
        public IActionResult GetActivityDetail()
        {
            ActivityContext context = new ActivityContext();
            var query = from ad in context.ActivityDetails
                        join u in context.Users on ad.UserId equals u.UserId
                        join a in context.Activities on ad.ActivityId equals a.ActivityId
                        select new
                        {
                            ActivityDetail = ad.ActivityDetailId,
                            ActivityName = a.ActivityName,
                            UserName = u.FirstName + " " + u.LastName

                        };
            return Ok(query);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Organizer")]
        public IActionResult GetActivityDetailByID(int id)

        {
            ActivityContext context = new ActivityContext();
            var query = from ad in context.ActivityDetails
                        join u in context.Users on ad.UserId equals u.UserId
                        join a in context.Activities on ad.ActivityId equals a.ActivityId
                        where ad.ActivityDetailId == id
                        select new
                        {
                            ActivityDetail = ad.ActivityDetailId,
                            ActivityName = a.ActivityName,
                            UserName = u.FirstName + " " + u.LastName

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
        [Authorize(Roles = "Admin,Organizer")]
        public IActionResult Create(ActivityDetail activityDetail)

        {
            ActivityContext context = new ActivityContext();

            ActivityDetail newActivityDetail= new ActivityDetail();
            newActivityDetail.ActivityDetailId= activityDetail.ActivityDetailId;
            newActivityDetail.UserId= activityDetail.UserId;
            newActivityDetail.ActivityId= activityDetail.ActivityId;


            context.ActivityDetails.Add(newActivityDetail);
            context.SaveChanges();

            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "ActivityDetail eklenemedi!");

            }

        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Organizer")]
        public IActionResult Update(int id, ActivityDetail activityDetail)

        {
            ActivityContext context = new ActivityContext();
            ActivityDetail originalActivityDetail = context.ActivityDetails.Find(id);
            originalActivityDetail.ActivityId = activityDetail.ActivityId;
            originalActivityDetail.UserId= activityDetail.UserId;
            context.SaveChanges();
            return Ok();

        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            ActivityContext context = new ActivityContext();
            ActivityDetail activityDetail = context.ActivityDetails.Find(id);
            context.ActivityDetails.Remove(activityDetail);
            context.SaveChanges();
            return NoContent();
        }



    }
}
