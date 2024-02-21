using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;

namespace AI_SMS_Student_Help.Pages
{
    public class UploadDocumentModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadDocumentModel(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public IFormFile Document { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string filePath = Path.Combine(uploadsFolder, Document.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Document.CopyToAsync(stream);
            }

            ViewData["Message"] = $"Document '{Document.FileName}' uploaded successfully!";
            return Page();
        }
    }
}
