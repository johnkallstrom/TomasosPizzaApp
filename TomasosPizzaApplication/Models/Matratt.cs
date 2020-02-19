using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TomasosPizzaApplication.Models
{
    public partial class Matratt
    {
        public Matratt()
        {
            BestallningMatratt = new HashSet<BestallningMatratt>();
            MatrattProdukt = new HashSet<MatrattProdukt>();
        }

        public int MatrattId { get; set; }
        [Required(ErrorMessage = "Ange maträttens namn")]
        public string MatrattNamn { get; set; }
        [Required(ErrorMessage = "Ange maträttens beskrivning")]
        public string Beskrivning { get; set; }
        [Required(ErrorMessage = "Ange maträttens pris")]
        public int Pris { get; set; }
        [Required(ErrorMessage = "Ange maträttens kategori")]
        public int MatrattTyp { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual MatrattTyp MatrattTypNavigation { get; set; }
        public virtual ICollection<BestallningMatratt> BestallningMatratt { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<MatrattProdukt> MatrattProdukt { get; set; }
    }
}
