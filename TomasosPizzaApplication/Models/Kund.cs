using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TomasosPizzaApplication.Models
{
    public partial class Kund
    {
        public Kund()
        {
            Bestallning = new HashSet<Bestallning>();
        }

        public int KundId { get; set; }
        [Required(ErrorMessage = "Ange ditt namn")]
        [StringLength(100, ErrorMessage = "Namn får max bestå av 100 tecken")]
        [MinLength(3, ErrorMessage = "Namn måste bestå av minst 3 tecken")]
        public string Namn { get; set; }
        [Required(ErrorMessage = "Ange din adress")]
        [StringLength(50, ErrorMessage = "Adress får max bestå av 50 tecken")]
        [MinLength(5, ErrorMessage = "Adress måste bestå av minst 5 tecken")]
        public string Gatuadress { get; set; }
        [Required(ErrorMessage = "Ange ditt postnummer")]
        [StringLength(20, ErrorMessage = "Postnummer får max bestå av 20 tecken")]
        [MinLength(5, ErrorMessage = "Postnummer måste bestå av minst 5 tecken")]
        public string Postnr { get; set; }
        [Required(ErrorMessage = "Ange din postort")]
        [StringLength(100, ErrorMessage = "Postort får max bestå av 100 tecken")]
        [MinLength(5, ErrorMessage = "Postort måste bestå av minst 5 tecken")]
        public string Postort { get; set; }
        [Required(ErrorMessage = "Ange din email")]
        [StringLength(50, ErrorMessage = "Email får max bestå av 50 tecken")]
        [MinLength(5, ErrorMessage = "Email måste bestå av minst 5 tecken")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Ange ditt telefonnummer")]
        [StringLength(50, ErrorMessage = "Telefonnummer får max bestå av 50 tecken")]
        [MinLength(5, ErrorMessage = "Telefonnummer måste bestå av minst 5 tecken")]
        public string Telefon { get; set; }
        [Required(ErrorMessage = "Ange ditt användarnamn")]
        [StringLength(20, ErrorMessage = "Användarnamn får max bestå av 20 tecken")]
        [MinLength(5, ErrorMessage = "Användarnamn måste bestå av minst 5 tecken")]
        public string AnvandarNamn { get; set; }
        [Required(ErrorMessage = "Ange ditt lösenord")]
        [StringLength(20, ErrorMessage = "Lösenord får max bestå av 20 tecken")]
        [MinLength(5, ErrorMessage = "Lösenord måste bestå av minst 5 tecken")]
        public string Losenord { get; set; }

        public virtual ICollection<Bestallning> Bestallning { get; set; }
    }
}
