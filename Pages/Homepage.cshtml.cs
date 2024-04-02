using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_SMS_Student_Help.Pages
{
    public class HomepageModel : PageModel
    {
        public List<Course> Courses { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=<your_storage_account_name>;AccountKey=<your_storage_account_key>;EndpointSuffix=core.windows.net";
            TableServiceClient tableServiceClient = new TableServiceClient(connectionString);
            TableClient tableClient = tableServiceClient.GetTableClient("course");

            var response = await tableClient.QueryAsync<Course>(filter: "PartitionKey eq 'course'");
            Courses = response.ToList();

            return Page();
        }
    }

    [Table("course")]
    public class Course
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string Semester { get; set; }
        public string TaughtBy { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}