using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AI_SMS_Student_Help.Pages
{
    public class AddClassModel : PageModel
    {
        private readonly ILogger<AddClassModel> _logger;

        public AddClassModel(ILogger<AddClassModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}