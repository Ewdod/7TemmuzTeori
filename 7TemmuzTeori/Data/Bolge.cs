using System;
using System.Collections.Generic;

namespace _7TemmuzTeori.Data
{
    public partial class Bolge
    {
        public Bolge()
        {
            Sehirler = new HashSet<Sehir>();
        }

        public int Id { get; set; }
        public string BolgeAd { get; set; } = null!;

        public virtual ICollection<Sehir> Sehirler { get; set; }
    }
}
