using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Core_WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class IdentityController : Controller
    {
        //[Authorize]
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        //}

        [Authorize]
        [HttpGet]
        public string Get()
        {
            return "成功访问到API（CORE）";
        }
    }
}