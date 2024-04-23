using Azure;
using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AI_SMS_Student_Help.Pages
{
    public class AddClassModel : PageModel
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string Semester { get; set; }
        public string TaughtBy { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }

        public class Course : ITableEntity
        {
            public string PartitionKey { get; set; }
            public string RowKey { get; set; }

            public string CourseId { get; set; }
            public string CourseName { get; set; }
            public string Semester { get; set; }
            public string TaughtBy { get; set; }
            public string Type { get; set; }
            public string Status { get; set; }

            public DateTimeOffset? Timestamp { get; set; }
            public ETag ETag { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var courseId = Request.Form["CourseId"];
            var courseName = Request.Form["CourseName"];

            if (string.IsNullOrEmpty(courseId) || string.IsNullOrEmpty(courseName))
            {
                ModelState.AddModelError("CourseId", "Course ID and Course Name are required.");
                return Page();
            }

            var course = new Course
            {
                PartitionKey = courseId,
                RowKey = courseName,

                CourseId = courseId,
                CourseName = courseName,
                Semester = Request.Form["Semester"],
                TaughtBy = Request.Form["TaughtBy"],
                Type = Request.Form["Type"],
                Status = Request.Form["Status"],

                Timestamp = DateTimeOffset.UtcNow,
                ETag = new ETag("*")
            };

            var tableClient = new TableClient("DefaultEndpointsProtocol=https;AccountName=aismschatbot;AccountKey=lv3AB/ySWQ/iy3uDJw504AV/vrqAeo+7qNs37kH7QU7+rDu66ELoKBecLJcW2ji5vVhsmsOAe+M5+AStBMEBgQ==;EndpointSuffix=core.windows.net", "courses");
            await tableClient.AddEntityAsync(course);

            TempData["Message"] = "Class added successfully!";

            return Page();
        }
    }
}