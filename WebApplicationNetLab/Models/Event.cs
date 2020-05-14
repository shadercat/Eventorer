using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationNetLab.Models
{
    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Event ID")]
        public int EventID { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }
        [Range(0, 5)]
        [Display(Name = "Importance")]
        public int Credits { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}