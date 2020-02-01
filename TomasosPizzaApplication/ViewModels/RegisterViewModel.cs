using System.ComponentModel.DataAnnotations;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ange ditt användarnamn")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Ange ditt lösenord")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Bekräfta ditt lösenord")]
        [Compare("Password", ErrorMessage = "Lösenorden matchar inte")]
        public string ConfirmPassword { get; set; }
        public Kund Kund { get; set; }
    }
}
