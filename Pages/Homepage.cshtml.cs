using Azure;
using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace AI_SMS_Student_Help.Pages
{
    //[Table("course")]
    //public class Course
    //{
    //    public string PartitionKey { get; set; }
    //    public string RowKey { get; set; }
    //    public string CourseId { get; set; }
    //    public string CourseName { get; set; }
    //    public string Semester { get; set; }
    //    public string TaughtBy { get; set; }
    //    public string Type { get; set; }
    //    public string Status { get; set; }
    //}
    [Table("courses")]
    public record Course : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string Semester { get; set; }
        public string TaughtBy { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }

        public ETag ETag { get; set; } = default!;

        public DateTimeOffset? Timestamp { get; set; } = default!;
    }

    public class HomepageModel : PageModel
    {
        public List<Course> Courses { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=aismschatbot;AccountKey=lv3AB/ySWQ/iy3uDJw504AV/vrqAeo+7qNs37kH7QU7+rDu66ELoKBecLJcW2ji5vVhsmsOAe+M5+AStBMEBgQ==;EndpointSuffix=core.windows.net";
            TableServiceClient tableServiceClient = new TableServiceClient(connectionString);
            TableClient tableClient = tableServiceClient.GetTableClient("courses");

            //var response = await tableClient.QueryAsync<Course>(filter: "PartitionKey eq 'course'");
            var product = await tableClient.GetEntityAsync<Course>(rowKey: "RowKey", partitionKey: "PartitionKey");
            //var product1 = await tableClient.QueryAsync<Course>(filter: "PartitionKey eq 'course'");
            //Courses = response.ToList();
            Console.WriteLine(product.Value.CourseName);

            return Page();
        }
    }
}