using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AI_SMS_Student_Help.Pages
{
    public class RegisterModel : PageModel
    {
        public void OnGet()
        {
        }
        public void Configure(IApplicationBuilder app)      //adds in photo for use in background...
        {
            app.UseStaticFiles();
        }
    }
}
