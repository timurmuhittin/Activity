using System;
using System.Collections.Generic;

namespace ActivityAPI.Models
{
    public partial class Activity
    {
        public Activity()
        {
            ActivityDetails = new HashSet<ActivityDetail>();
        }

        public int ActivityId { get; set; }
        public int CategoryId { get; set; }
        public int OrganizerId { get; set; }
        public string ActivityName { get; set; } = null!;
        public DateTime Date { get; set; }
        public DateTime DateDeadline { get; set; }
        public int CityId { get; set; }
        public string Description { get; set; } = null!;
        public string Adress { get; set; } = null!;
        public int TickedTypeId { get; set; }
        public int? Amout { get; set; }
        public decimal? TickedPrice { get; set; }
        public int? TickedSalesId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual City City { get; set; } = null!;
        public virtual Organizer Organizer { get; set; } = null!;
        public virtual TickedSale? TickedSales { get; set; }
        public virtual TickedType TickedType { get; set; } = null!;
        public virtual ICollection<ActivityDetail> ActivityDetails { get; set; }
    }
}
