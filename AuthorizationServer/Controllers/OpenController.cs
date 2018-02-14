using AuthorizationServer.Models.ClientViewModels;
using AuthorizationServer.Services.Clients;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthorizationServer.Controllers
{
    public class OpenController : BaseController
    {
        private readonly IClientService _clientService;

        public OpenController(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<IActionResult> Index()
        {
            var clients = await _clientService.GetByUserIdAsync(UserId);

            if (clients?.Count > 0)
            {
                return View(clients);
            }

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClientInputModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _clientService.AddAsync(UserId, model.ClientName, model.RedirectUrl, model.PostLogoutRedirectUrl, model.RequireConsent);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMsg);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string clientId)
        {
            var result = await _clientService.GetByIdAsync(UserId, clientId);

            if (result != null)
            {
                return View(result);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(ClientInputModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _clientService.UpdateAsync(UserId, model.ClientId, model.ClientName, model.RedirectUrl, model.PostLogoutRedirectUrl, model.RequireConsent,model.AllowedScope);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMsg);
                }
            }

            return View(model);
        }
    }
}