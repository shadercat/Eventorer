using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationNetLab.Models
{
    public class Account
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}