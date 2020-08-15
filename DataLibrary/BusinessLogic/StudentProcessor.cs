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

        public static int DLUpdateStudentInfo( int ID, string FirstName, string LastName)
        {

            var parameters = new { StudentID = ID, FirstName = FirstName, LastName = LastName };

            string sql = @"UPDATE dbo.Students
                            SET FirstName = @FirstName, LastName = @LastName
                            WHERE StudentID = @StudentID";

            return SqlDataAccess.UpdateStudentInfo(sql, parameters);
        }

        // Load Student
        public static List<StudentModel> DLLoadStudents()
        {
            string sql = @"SELECT StudentID, FirstName, LastName From dbo.Students";
            return SqlDataAccess.LoadStudentData<StudentModel>(sql);
        }

        public static List<StudentModel> DLLoadStudentInfo(int id)
        {
            var parameters = new { StudentID = id };
            string sql = @"SELECT StudentID, FirstName, LastName 
                            From dbo.Students
                            WHERE Students.StudentID = @StudentID";

            return SqlDataAccess.LoadStudentInfo<StudentModel>(sql, parameters);
        }


        // Get Available Courses
        public static List<CoursesModel> DLGetCourses()
        {
            string sql = @"SELECT CoursesID, Name, StartTime, EndTime
                            FROM dbo.Courses";

            return SqlDataAccess.GetAvailableCourses<CoursesModel>(sql);
        }

        // Get Course Specific info
        public static List<CoursesModel> DLGetCourse(int courseID)
        {
            var parameters = new { courseID = courseID };
            string sql = @"SELECT CoursesID, Name, StartTime, EndTime, ClassRoom
                            FROM dbo.Courses
                            WHERE Courses.CoursesID = @courseID";

            return SqlDataAccess.GetAvailableCourse<CoursesModel>(sql, parameters);
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

        public static int DLDeleteStudent(int id)
        {
            var parameters = new { StudentID = id };

            string sql = @"DELETE FROM dbo.Students WHERE Students.StudentID = @StudentID";

            return SqlDataAccess.Delete(sql, parameters);

        }

        public static int DLDeleteCourse(int StudentID, int CoursesID)
        {
            var parameters = new { StudentID = StudentID, CoursesID = CoursesID };

            string sql = @"DELETE FROM dbo.Takes 
                            WHERE Takes.StudentID = @StudentID AND Takes.CoursesID = @CoursesID ";

            return SqlDataAccess.Delete(sql, parameters);

        }
    }
}
