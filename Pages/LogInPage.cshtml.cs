using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AI_SMS_Student_Help.Pages
{
    public class LogInPageModel : PageModel
    {
        private readonly ILogger<LogInPageModel> _logger;

        public LogInPageModel(ILogger<LogInPageModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
