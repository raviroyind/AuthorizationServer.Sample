using System.ComponentModel.DataAnnotations;

namespace AuthorizationServer.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0}的最小长度是{2}, 最大长度是{1}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不一致.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
