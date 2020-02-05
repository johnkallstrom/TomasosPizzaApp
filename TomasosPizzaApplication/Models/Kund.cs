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
        public string Namn { get; set; }
        [Required(ErrorMessage = "Ange din adress")]
        public string Gatuadress { get; set; }
        [Required(ErrorMessage = "Ange ditt postnummer")]
        public string Postnr { get; set; }
        [Required(ErrorMessage = "Ange din postort")]
        public string Postort { get; set; }
        [Required(ErrorMessage = "Ange din email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Ange ditt telefonnummer")]
        public string Telefon { get; set; }
        public string AnvandarNamn { get; set; }
        public string Losenord { get; set; }

        public virtual ICollection<Bestallning> Bestallning { get; set; }
    }
}
