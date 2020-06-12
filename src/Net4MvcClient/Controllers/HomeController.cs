using Net4MvcClient.Infrastructure;
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

        [MyAuthorize(Users = "Bob Smith")]
        public ActionResult Authorized()
        {
            return View((User as ClaimsPrincipal));
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Unauthorized()
        {
            ViewBag.Message = "You are not authorized!.";

            return View();
        }

        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
    }
}