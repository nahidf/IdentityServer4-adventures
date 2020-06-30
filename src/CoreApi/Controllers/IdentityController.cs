using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreApi.Controllers
{
    [Route("identity")]
    [ApiController]

    public class IdentityController : ControllerBase
    {
        [HttpGet, Authorize]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [HttpGet("custom"), Authorize("ApiCustomAccess")]
        public IActionResult GetCustom()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}