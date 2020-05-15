using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Net4MvcClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            return View((User as ClaimsPrincipal).Claims);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
    }
}