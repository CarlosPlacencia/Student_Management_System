using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student_Management_System.Models
{
    public class CoursesModel
    {
        public int CourseID { get; set; }
        public string Name { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

    }
}