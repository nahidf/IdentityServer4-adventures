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

        [HttpGet("me"), Authorize]
        public IActionResult GetMyIdentity()
        {
            if (User.Identity.Name == null)
                return Forbid();

            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [HttpGet("me/read-access"), Authorize("ReadAccessPolicy")]
        public IActionResult GetMyIdentityByReadAccess()
        {
            if (User.Identity.Name == null)
                return Forbid();

            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [HttpGet("me/delete-access"), Authorize("DeleteAccessPolicy")]
        public IActionResult GetMyIdentityByDeleteAccess()
        {
            if (User.Identity.Name == null)
                return Forbid();

            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [HttpGet("read-access"), Authorize("ReadAccessPolicy")]
        public IActionResult GetByReadAccess()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [HttpGet("delete-access"), Authorize("DeleteAccessPolicy")]
        public IActionResult GetByDeleteAccess()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

       
    }
}