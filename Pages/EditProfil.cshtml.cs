using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AI_SMS_Student_Help.Pages
{
    public class EditProfilModel : PageModel
    {
        private readonly ILogger<EditProfilModel> _logger;

        public EditProfilModel(ILogger<EditProfilModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
