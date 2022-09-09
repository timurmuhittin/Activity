using System;
using System.Collections.Generic;

namespace ActivityAPI.Models
{
    public partial class User
    {
        public User()
        {
            ActivityDetails = new HashSet<ActivityDetail>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<ActivityDetail> ActivityDetails { get; set; }
    }
}
