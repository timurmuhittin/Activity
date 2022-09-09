using System;
using System.Collections.Generic;

namespace ActivityAPI.Models
{
    public partial class City
    {
        public City()
        {
            Activities = new HashSet<Activity>();
        }

        public int CityId { get; set; }
        public string City1 { get; set; } = null!;

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
