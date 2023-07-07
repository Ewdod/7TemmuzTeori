using System;
using System.Collections.Generic;

namespace _7TemmuzTeori.Data
{
    public partial class Sehir
    {
        public Sehir()
        {
            Ogrenciler = new HashSet<Ogrenci>();
            Sevenler = new HashSet<Ogrenci>();
        }

        public int Id { get; set; }
        public int BolgeId { get; set; }
        public string SehirAd { get; set; } = null!;
        public int Nufus { get; set; }

        public virtual Bolge Bolge { get; set; } = null!;
        public virtual ICollection<Ogrenci> Ogrenciler { get; set; }

        public virtual ICollection<Ogrenci> Sevenler { get; set; }
    }
}
