DECLARE @StudentID AS INT = 3;

SELECT 
	Students.StudentID,Students.FirstName, Students.LastName,
	Courses.CoursesID, Courses.Name, Courses.StartTime, Courses.EndTime, Courses.ClassRoom
FROM Takes
	INNER JOIN Students ON Takes.StudentID = Students.StudentID
	INNER JOIN Courses ON Takes.CoursesID = Courses.CoursesID
WHERE 
	Students.StudentID = @StudentID;

--SELECT Courses.*
--FROM Courses
--Join Takes ON Courses.CoursesID = Takes.CoursesID
--Join Students ON Takes.StudentID = Students.StudentID
--WHERE Students.StudentID = @StudentID;




