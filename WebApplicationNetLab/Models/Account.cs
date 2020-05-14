using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationNetLab.Models
{
    public class Account
    {
        public int ID { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(100, ErrorMessage = "Nickname cannot be longer than 100 characters.")]
        public string Nickname { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}