﻿using System;
using System.Collections.Generic;

namespace TomasosPizzaApplication.Models
{
    public partial class Produkt
    {
        public Produkt()
        {
            MatrattProdukt = new HashSet<MatrattProdukt>();
        }

        public int ProduktId { get; set; }
        public string ProduktNamn { get; set; }

        public virtual ICollection<MatrattProdukt> MatrattProdukt { get; set; }
    }
}
