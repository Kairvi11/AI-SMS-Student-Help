using Azure.Storage.Blobs;
using Azure.Data.Tables;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Azure;
using System.Text;
using System.Security.Cryptography;

namespace AI_SMS_Student_Help.Pages
{
    public class UploadDocumentModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private BlobServiceClient BlobServiceClient { get; set; }
        private bool ContainerExistsCache { get; set; } = false;

        public UploadDocumentModel(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            Document = new FormFile(new MemoryStream(), 0, 0, null, string.Empty);
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=aismschatbot;AccountKey=lv3AB/ySWQ/iy3uDJw504AV/vrqAeo+7qNs37kH7QU7+rDu66ELoKBecLJcW2ji5vVhsmsOAe+M5+AStBMEBgQ==;EndpointSuffix=core.windows.net";
            BlobServiceClient = new BlobServiceClient(connectionString);
        }

        [BindProperty]
        public IFormFile Document { get; set; }

        [BindProperty]
        public string CourseId { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string containerName = "aimsblobs";
            BlobContainerClient containerClient = BlobServiceClient.GetBlobContainerClient(containerName);

            if (!ContainerExistsCache)
            {
                ContainerExistsCache = await containerClient.ExistsAsync();
                if (!ContainerExistsCache)
                {
                    await containerClient.CreateIfNotExistsAsync();
                }
            }

            BlobClient blobClient = containerClient.GetBlobClient(Document.FileName) ?? throw new Exception("Blob client is null.");
            using (var stream = new MemoryStream())
            {
                await Document.CopyToAsync(stream);
                stream.Position = 0;

                await blobClient.UploadAsync(stream, true);
            }

            TableServiceClient tableServiceClient = new TableServiceClient("DefaultEndpointsProtocol=https;AccountName=aismschatbot;AccountKey=lv3AB/ySWQ/iy3uDJw504AV/vrqAeo+7qNs37kH7QU7+rDu66ELoKBecLJcW2ji5vVhsmsOAe+M5+AStBMEBgQ==;EndpointSuffix=core.windows.net");
            TableClient tableClient = tableServiceClient.GetTableClient("documents");

            await InsertMetadataAsync(tableClient, CourseId, blobClient.Uri.ToString());

            ViewData["Message"] = $"Document '{Document.FileName}' uploaded successfully and added to the documents table!";
            return Page();
        }

        private async Task InsertMetadataAsync(TableClient tableClient, string courseId, string blobUri)
        {
            DocumentEntity documentEntity = new DocumentEntity(courseId, Document.FileName)
            {
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow,
                BlobUri = blobUri
            };

            await tableClient.AddEntityAsync(documentEntity);
        }
    }

    public class DocumentEntity : ITableEntity
    {
        public DocumentEntity() { }

        public DocumentEntity(string courseId, string rowKey)
        {
            PartitionKey = courseId;
            RowKey = rowKey;
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public ETag ETag { get; set; } = new ETag("*");
        public DateTimeOffset? Timestamp { get; set; } = DateTimeOffset.UtcNow;
        public string BlobUri { get; set; }
    }
}
