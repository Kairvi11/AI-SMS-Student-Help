using Azure.Storage.Blobs;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AI_SMS_Student_Help.Pages
{
    public class UploadDocumentModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadDocumentModel(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            Document = new FormFile(new MemoryStream(), 0, 0, null, string.Empty);
        }

        [BindProperty]
        public IFormFile Document { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string connectionString = "DefaultEndpointsProtocol=https;AccountName=aismschatbot;AccountKey=lv3AB/ySWQ/iy3uDJw504AV/vrqAeo+7qNs37kH7QU7+rDu66ELoKBecLJcW2ji5vVhsmsOAe+M5+AStBMEBgQ==;EndpointSuffix=core.windows.net"; // replace with your Azure Storage account connection string
            string containerName = "aimsblobs";

            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            if (!containerClient.Exists())
            {
                await containerClient.CreateIfNotExistsAsync();
            }

            BlobClient blobClient = containerClient.GetBlobClient(Document.FileName) ?? throw new Exception("Blob client is null.");
            using (var stream = new MemoryStream())
            {
                await Document.CopyToAsync(stream);
                stream.Position = 0;
                await blobClient.UploadAsync(stream, true); // true to overwrite the blob if it exists
            }

            ViewData["Message"] = $"Document '{Document.FileName}' uploaded successfully!";
            return Page();
        }
    }
}