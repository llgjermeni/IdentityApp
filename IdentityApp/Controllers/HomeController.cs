using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers
{
    public class HomeController: Controller
    {
        [Route("")]
        public  IActionResult Index()
        {
            return View();
        }

        [Route("Userdata")]
        [Authorize]
        public  IActionResult UserData()
        {
            return View();
        }
    }
}
