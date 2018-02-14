using Framework_WebAPI.Filters;
using System.Web.Http;

namespace Framework_WebAPI.Controllers
{
    [JwtAuthentication]
    public class IdentityController : ApiController
    {
        public string Get()
        {
            return "成功访问到API（Framework）";
        }
    }
}
