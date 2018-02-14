using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Framework_Hybrid.Controllers
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
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> CallCoreApi()
        {
            var token = (User as ClaimsPrincipal).FindFirst("access_token").Value;
            var client = new HttpClient();
            client.SetBearerToken(token);

            var json = await client.GetStringAsync("http://localhost:5001/api/identity");
            return Content(json);
        }

        [HttpGet]
        public async Task<ActionResult> CallFrameworkApi()
        {
            var token = (User as ClaimsPrincipal).FindFirst("access_token").Value;
            var client = new HttpClient();
            client.SetBearerToken(token);

            var json = await client.GetStringAsync("http://localhost:5009/api/identity");
            return Content(json);
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetInfo()
        {
            var user = (User as ClaimsPrincipal);

            return new EmptyResult();
        }
    }
}