using System.ComponentModel.DataAnnotations;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.ViewModels
{
    public class EditDetailsViewModel
    {
        [Required(ErrorMessage = "Ange ditt namn")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Ange din adress")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "Ange ditt postnummer")]
        public string Postalcode { get; set; }
        [Required(ErrorMessage = "Ange din postort")]
        public string City { get; set; }
        [Required(ErrorMessage = "Ange din email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Ange ditt telefonnummer")]
        public string Phone { get; set; }
        public Kund Kund { get; set; }
    }
}
