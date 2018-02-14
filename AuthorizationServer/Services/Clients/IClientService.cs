using AuthorizationServer.Models.ClientViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorizationServer.Services.Clients
{
    public interface IClientService
    {
        Task<(bool Succeeded, string ErrorMsg)> AddAsync(string userId, string clientName, string redirectUrl, string postLogoutRedirectUrl, bool requireConsent);
        Task<(bool Succeeded, string ErrorMsg)> UpdateAsync(string userId, string clientId, string clientName, string redirectUrl, string postLogoutRedirectUrl, bool requireConsent, string allowedScope);

        Task<List<ClientViewModel>> GetByUserIdAsync(string userId);

        Task<ClientInputModel> GetByIdAsync(string userId, string clientId);
    }
}
