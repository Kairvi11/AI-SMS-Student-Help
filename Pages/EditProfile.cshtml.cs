using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AI_SMS_Student_Help.Pages
{
    public class EditProfileModel : PageModel
    {
        private readonly ILogger<EditProfileModel> _logger;

        public EditProfileModel(ILogger<EditProfileModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
