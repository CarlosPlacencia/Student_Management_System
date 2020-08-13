using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student_Management_System.Models
{
    public class StudentCoursesModel
    {
        public int StudentID { get; set; }

        public int CoursesID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CourseName { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public int ClassRoom { get; set; }
    }
}