using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Student_Management_System.Models
{
    public class StudentModel
    {
        public int StudentID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name Required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name Required")]
        public string LastName { get; set; }

    }
}