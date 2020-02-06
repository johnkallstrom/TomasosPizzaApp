using System.ComponentModel.DataAnnotations;

namespace TomasosPizzaApplication.ViewModels
{
    public class ChangeUsernameViewModel
    {
        [Required(ErrorMessage = "Ange ditt användarnamn")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Ange ditt lösenord")]
        public string Password { get; set; }
    }
}
