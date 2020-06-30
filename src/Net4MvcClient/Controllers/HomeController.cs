using Net4MvcClient.Infrastructure;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
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

        [Authorize]
        public async Task<ActionResult> CallCoreApi()
        {
            var user = User as ClaimsPrincipal;
            var accessToken = user.FindFirst("access_token").Value;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync("http://localhost:5001/identity");
            string content;
            if (!response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
                ViewBag.Json = content;
            }
            else
            {
                content = await response.Content.ReadAsStringAsync();
                ViewBag.Json = JArray.Parse(content).ToString();
            }

            return View("Json");
        }

        [Authorize]
        public async Task<ActionResult> CallNet4Api()
        {
            var user = User as ClaimsPrincipal;
            var accessToken = user.FindFirst("access_token").Value;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync("http://localhost:5004/identity");
            string content;
            if (!response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
                ViewBag.Json = content;
            }
            else
            {
                content = await response.Content.ReadAsStringAsync();
                ViewBag.Json = JArray.Parse(content).ToString();
            }

            return View("Json");
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