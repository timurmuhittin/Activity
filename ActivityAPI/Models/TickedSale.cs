using System;
using System.Collections.Generic;

namespace ActivityAPI.Models
{
    public partial class TickedSale
    {
        public TickedSale()
        {
            Activities = new HashSet<Activity>();
        }

        public int TickedSalesId { get; set; }
        public string Email { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
