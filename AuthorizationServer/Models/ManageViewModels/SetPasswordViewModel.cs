using System.ComponentModel.DataAnnotations;

namespace AuthorizationServer.Models.ManageViewModels
{
    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0}的最小长度是{2}, 最大长度是{1}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("NewPassword", ErrorMessage = "新密码和确认密码不一致.")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
