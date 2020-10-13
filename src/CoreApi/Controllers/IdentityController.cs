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

        [HttpGet("read"), Authorize("ReadAccessPolicy")]
        public IActionResult GetRead()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [HttpGet("delete"), Authorize("DeleteAccessPolicy")]
        public IActionResult GetDelete()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}