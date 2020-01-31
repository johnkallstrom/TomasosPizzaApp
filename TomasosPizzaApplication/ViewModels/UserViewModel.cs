using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Ange ditt användarnamn")]
        [StringLength(50, ErrorMessage = "Användarnamn får max vara 50 tecken")]
        [MinLength(3, ErrorMessage = "Användarnamn måste vara minst 3 tecken")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Ange ditt lösenord")]
        public string Password { get; set; }
        public Kund Customer { get; set; }
    }
}
