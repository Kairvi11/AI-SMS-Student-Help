using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AI_SMS_Student_Help.Pages
{
    public class CreateAccountPageModel : PageModel
    {
        private readonly ILogger<CreateAccountPageModel> _logger;

        public CreateAccountPageModel(ILogger<CreateAccountPageModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }

}
