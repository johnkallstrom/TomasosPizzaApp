using System.ComponentModel.DataAnnotations;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Ange ditt nuvarande lösenord")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "Ange nytt lösenord")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Bekräfta nytt lösenord")]
        [Compare("NewPassword", ErrorMessage = "Lösenorden matchar inte")]
        public string ConfirmNewPassword { get; set; }
        public Kund Kund { get; set; }
    }
}
