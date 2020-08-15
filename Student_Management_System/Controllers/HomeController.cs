using Student_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using static DataLibrary.BusinessLogic.StudentProcessor;
using System.Web.Management;
using System.Diagnostics;
using System.Dynamic;
using System.Web.UI.WebControls;

namespace Student_Management_System.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Students()
        {



            // Create an interface class


            /******************************** Getting Students *************************************/
            var StudentData = DLLoadStudents();
            List<StudentModel> students = new List<StudentModel>();




            // Loop through to create a model for each student in data
            foreach (var row in StudentData)
            {
                students.Add(new StudentModel
                {
                    StudentID = row.StudentID,
                    FirstName = row.FirstName,
                    LastName = row.LastName
                });
            }

            /******************************** Getting Courses *************************************/
            var CoursesData = DLGetCourses();
            List<CoursesModel> courses = new List<CoursesModel>();

            foreach (var row in CoursesData)
            {
                courses.Add(new CoursesModel
                {
                    CourseID = row.CoursesID,
                    Name = row.Name,
                    StartTime = row.StartTime.ToString("hh:mm tt"),
                    EndTime = row.EndTime.ToString("hh:mm tt"),
                });
            }


            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in courses)
            {
                items.Add(new SelectListItem { Text = item.Name + "  " + item.StartTime + " - " + item.EndTime, Value = item.CourseID.ToString() });
            }

            ViewBag.CourseTime = items;

            return View(students);
        }


        // GET RegisterStudents() → Display form to register a new student
        public ActionResult RegisterStudents()
        {
            return View();
        }

        // POST RegisterStudents() → Save Form information to STUDENT TABLE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterStudent(StudentModel Newstudent)
        {
            // Call the DataLibrary Project to insert the NewStudent information
            if (ModelState.IsValid)
            {
                int recordsCreated = DLRegisterStudent(
                    Newstudent.FirstName,
                    Newstudent.LastName);
                return RedirectToAction("Students");
            }
            return View();
        }


        // POST EnrollInCourse() → Saves the StudentID & CoursedID to the TAKES TABLE
        [HttpPost]
        public ActionResult EnrollInCourse(CourseEnrollModel CourseEnroll)
        {
            dynamic returnJson = new ExpandoObject();

            //Make the call to BusinessLogic Enroll Function
            if (ModelState.IsValid)
            {
                int ClassesEnrolled = DLEnrollCourse(
                    CourseEnroll.StudentID,
                    CourseEnroll.CourseID);

                switch (ClassesEnrolled)
                {
                    case 1:
                        returnJson.Message = "Success";
                        return Json(returnJson);
                    case -1:
                        returnJson.Message = "You're already enrolled in that class";
                        return Json(returnJson);
                    default:
                        returnJson.Message = "Error";
                        return Json(returnJson);
                }
            }

            return Json(returnJson);
        }



        // GET StudentCourses() → Display the courses a student has enrolled in
        public ActionResult StudentCourses(int id)
        {
            var Courses = DLGetStudentCourses(id);
            List<StudentCoursesModel> studentCourses = new List<StudentCoursesModel>();


            foreach (var row in Courses)
            {
                studentCourses.Add(new StudentCoursesModel
                {
                    StudentID = row.StudentID,
                    CoursesID = row.CoursesID,
                    FirstName = row.FirstName,
                    LastName = row.LastName,
                    CourseName = row.Name,
                    StartTime = row.StartTime.ToString("hh:mm tt"),
                    EndTime = row.EndTime.ToString("hh:mm tt"),
                    ClassRoom = row.ClassRoom,
                });
            }

            return View(studentCourses);
        }

        public ActionResult EditStudent(int id)
        {
            // Retrieve info from the database
            var studentInfo = DLLoadStudentInfo(id);
            StudentModel student = new StudentModel();

            student.StudentID = studentInfo[0].StudentID;
            student.FirstName = studentInfo[0].FirstName;
            student.LastName = studentInfo[0].LastName;


            return View(student);
        }

        [HttpPost]
        public ActionResult EditStudent(StudentModel Newstudent)
        {
 
            DLUpdateStudentInfo(Newstudent.StudentID, Newstudent.FirstName, Newstudent.LastName);

            return RedirectToAction("Students");
        }

        public ActionResult DeleteStudent(int id)
        {
            var studentInfo = DLLoadStudentInfo(id);
            StudentModel student = new StudentModel();

            student.StudentID = studentInfo[0].StudentID;
            student.FirstName = studentInfo[0].FirstName;
            student.LastName = studentInfo[0].LastName;


            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDeleteStudent(StudentModel deleteStudent)
        {
            DLDeleteStudent(deleteStudent.StudentID);
            return RedirectToAction("Students");
        }

        public ActionResult DeleteCourse(int CourseID, int StudentID)
        {
            // get the info for the course
            var courseInfo = DLGetCourse(CourseID);
            // Create a model that has both the courseID and studentID and send that to the View()
            StudentCoursesModel course = new StudentCoursesModel();

            course.StudentID = StudentID;
            foreach (var row in courseInfo)
            {
                course.CoursesID = row.CoursesID;
                course.CourseName = row.Name;
                course.StartTime = row.StartTime.ToString("hh:mm tt");
                course.EndTime = row.EndTime.ToString("hh:mm tt");
                course.ClassRoom = row.ClassRoom;
            }
            // In the view just display the Course info hide the CourseID and StudentId

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCourse(StudentCoursesModel deleteCourse)
        {
            // Pass the StudentID and CourseID to be Deleted form the takes table
            DLDeleteCourse(deleteCourse.StudentID, deleteCourse.CoursesID);

            return RedirectToAction("StudentCourses", new { id = deleteCourse.StudentID });
        }
    }
}