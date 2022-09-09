using System;
using System.Collections.Generic;

namespace ActivityAPI.Models
{
    public partial class TickedType
    {
        public TickedType()
        {
            Activities = new HashSet<Activity>();
        }

        public int TickedTypeId { get; set; }
        public string TickedType1 { get; set; } = null!;

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
