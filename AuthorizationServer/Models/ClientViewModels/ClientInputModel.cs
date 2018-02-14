using System.ComponentModel.DataAnnotations;

namespace AuthorizationServer.Models.ClientViewModels
{
    public class ClientInputModel
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        [Required]
        [Display(Name = "客户端名称")]
        public string ClientName { get; set; }

        [Required]
        [Display(Name = "跳转地址")]
        public string RedirectUrl { get; set; }

        [Required]
        [Display(Name = "登出地址")]
        public string PostLogoutRedirectUrl { get; set; }

        [Display(Name = "是否开启同意界面")]
        public bool RequireConsent { get; set; }
        public string AllowedScope { get; set; }
    }
}
