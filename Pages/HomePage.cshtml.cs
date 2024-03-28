using Azure.Storage.Blobs;
using Azure.Data.Tables;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;


namespace AI_SMS_Student_Help.Pages
{
	public class HomePageModel : PageModel
	{
		public void OnGet()
		{
			Courses = GetCourses();
		}

		List<Course> GetCourses()
		{
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
			builder.DataSource = "usifalc.database.windows.net";
			builder.UserID = "azureuser";
			builder.Password = "Afd1280456!@";
			builder.InitialCatalog = "Chatbot";
			var dataTable = new DataTable();
			using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
			{
				connection.Open();
				using (var sqlCommand = new SqlCommand("SELECT * FROM Courses", connection))
				{
					using (var sqlReader = sqlCommand.ExecuteReader())
					{
						dataTable.Load(sqlReader);
					}
				}
			}

			var courses = new List<Course>();
			foreach (DataRow dataRow in dataTable.Rows)
			{
				var course = new Course
				{
					courseid = (string)dataRow["courseid"],
					name = (string)dataRow["name"],
					status = (string)dataRow["status"],
					semester = (string)dataRow["semester"],
					phone_number = (string)dataRow["name"],
					type = (string)dataRow["type"],


				};
				courses.Add(course);
			}
		
			return courses;

		}
		

		public List<Course> Courses { get; set; }
		public class Course
		{
			public string courseid { get; set; }
			public string name { get; set; }
			public string status { get; set; }
			public string semester { get; set; }
			public string phone_number { get; set; }
			public string type { get; set; } }
	}

	}




	

    

