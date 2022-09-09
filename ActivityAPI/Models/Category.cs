using System;
using System.Collections.Generic;

namespace ActivityAPI.Models
{
    public partial class Category
    {
        public Category()
        {
            Activities = new HashSet<Activity>();
        }

        public int CategoryId { get; set; }
        public string Category1 { get; set; } = null!;

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
