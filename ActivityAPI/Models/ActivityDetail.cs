using System;
using System.Collections.Generic;

namespace ActivityAPI.Models
{
    public partial class ActivityDetail
    {
        public int ActivityDetailId { get; set; }
        public int UserId { get; set; }
        public int ActivityId { get; set; }

        public virtual Activity? Activity { get; set; } = null!;
        public virtual User? User { get; set; } = null!;
    }
}
