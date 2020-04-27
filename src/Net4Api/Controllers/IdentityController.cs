using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace NetApi.Controllers
{
    public class IdentityController : ApiController
    {
        [HttpGet]
        [Route("identity")]
        [Authorize]
        public dynamic Get()
        {
            var principal = User as ClaimsPrincipal;

            return from c in principal.Identities.First().Claims
                   select new
                   {
                       c.Type,
                       c.Value
                   };
        }

        [HttpGet, Route("verify"), AllowAnonymous]
        public IHttpActionResult GetFree()
        {
            return Ok("Its not authorized!");
        }
    }
}
