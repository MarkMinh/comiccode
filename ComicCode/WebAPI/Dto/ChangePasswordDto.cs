using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dto
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "Old password is required.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required.")]
        [MinLength(6, ErrorMessage = "New password must be at least 6 characters.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Re-enter new password is required.")]
        [Compare("NewPassword", ErrorMessage = "New password and confirmation do not match.")]
        public string ReNewPassword { get; set; }
    }
}
