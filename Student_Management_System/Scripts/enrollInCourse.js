
/*

Click the Enroll In Courses Link
 → Get the student ID and store it in an object
That will open up the Modal
 → Display the Course inside the Modal
Then you can select what courses to take and click register
 → Get the Value of the selected course and store it in the Object
 → Make Post Call to the EnrollCourses Route

 */

let courseEnrollInfo = {
    StudentID: "",
    CourseID: ""
}

// Get the StudentID and Store it
$(".Enroll").click(function () {
    var $row = $(this).closest("tr");    // Find the row
    var $text = $row.find(".stID").text(); // Find the text

    // Assigned StudentID to Object
    courseEnrollInfo.StudentID = $text;
});

$(".CourseSubmit").click(function () {
    // Stores the selected value to the object
    courseEnrollInfo.CourseID = $("#CourseTime").val();

    // Make Post call so the values get stored in the database
    $.ajax({ //Do an ajax post to the controller
        type: 'POST',
        url: '/Home/EnrollInCourse',
        data: JSON.stringify(courseEnrollInfo),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
        },
        success: function (response) {
            alert(response[0].Value);
        }
    });
})

