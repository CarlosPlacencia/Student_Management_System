using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{

        public class CoursesModel
        {
            public int CoursesID { get; set; }

            public string Name { get; set; }

            public DateTime StartTime { get; set; }

            public DateTime EndTime { get; set; }

            public int ClassRoom { get; set; }
        }

}
