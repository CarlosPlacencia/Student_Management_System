using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class StudentProcessor
    {
        // Register Student
        public static int DLRegisterStudent(string firstName, string lastName)
        {
            StudentModel data = new StudentModel
            {
                FirstName = firstName,
                LastName = lastName
            };

            string sql = @"INSERT INTO dbo.Students (FirstName, LastName)
                            VALUES (@FirstName, @LastName)";

            return SqlDataAccess.SaveStudentData(sql, data);
        }

        // Load Student
        public static List<StudentModel> DLLoadStudents()
        {
            string sql = @"SELECT StudentID, FirstName, LastName From dbo.Students";
            return SqlDataAccess.LoadStudentData<StudentModel>(sql);
        }


        // Get Available Courses
        public static List<CoursesModel> DLGetCourses()
        {
            string sql = @"SELECT CoursesID, Name, StartTime, EndTime
                            FROM dbo.Courses";

            return SqlDataAccess.GetAvailableCourses<CoursesModel>(sql);
        }


        // Enroll Student To Course
        public static int DLEnrollCourse(int StudentID, int CourseID)
        {
            string addCourse = "spInsertCourse";

            CourseEnrollModel data = new CourseEnrollModel
            {
                StudentID = StudentID,
                CoursesID = CourseID
            };

            //string sql = @"if not exists
            //                    (select * from dbo.Takes where @StudentID = Takes.StudentID AND @CoursesID = Takes.CoursesID)
            //               INSERT INTO dbo.Takes (StudentID, CoursesID)
            //               VALUES (@StudentID, @CoursesID)";

            return SqlDataAccess.SaveStudentTakes(addCourse, data);
        }


        // Get Courses Student is Taking
        public static List<StudentCoursesModel> DLGetStudentCourses(int id)
        {
            var parameters = new { StudentID = id };
            string sql = @"SELECT 
	                            Students.StudentID,Students.FirstName, Students.LastName,
	                            Courses.CoursesID, Courses.Name, Courses.StartTime, Courses.EndTime, Courses.ClassRoom
                            FROM Takes
	                            INNER JOIN Students ON Takes.StudentID = Students.StudentID
	                            INNER JOIN Courses ON Takes.CoursesID = Courses.CoursesID
                            WHERE 
	                            Students.StudentID = @StudentID;";
            return SqlDataAccess.GetStudentsCourses<StudentCoursesModel>(sql, parameters);
        }
    }
}
