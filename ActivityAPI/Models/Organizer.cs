using System;
using System.Collections.Generic;

namespace ActivityAPI.Models
{
    public partial class Organizer
    {
        public Organizer()
        {
            Activities = new HashSet<Activity>();
        }

        public int OrganizerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
