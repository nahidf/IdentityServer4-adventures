using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcClient.Models;
using Newtonsoft.Json.Linq;

namespace MvcClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        public async Task<IActionResult> CallApi()
        {
            var idToken = await HttpContext.GetTokenAsync("id_token");
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync("https://localhost:5011/identity");
            var content = "";
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

        public async Task<IActionResult> CallNetApi()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync("https://localhost:5014/identity");
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
    }
}
