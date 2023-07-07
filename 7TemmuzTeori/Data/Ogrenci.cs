using System;
using System.Collections.Generic;

namespace _7TemmuzTeori.Data
{
    public partial class Ogrenci
    {
        public Ogrenci()
        {
            TakimUyeleri = new HashSet<Ogrenci>();
            FavoriSehirler = new HashSet<Sehir>();
        }

        public int Id { get; set; }
        public string Ad { get; set; } = null!;
        public string Soyad { get; set; } = null!;
        public string Cinsiyet { get; set; } = null!;
        public DateTime? DogumTarihi { get; set; }
        public bool? EvliMi { get; set; }
        public decimal? MezuniyetNotu { get; set; }
        public string? KanGrubu { get; set; }
        public bool? CovidGecirdiMi { get; set; }
        public int? DogumYeriId { get; set; }
        public int? TakimLideriId { get; set; }

        public virtual Sehir? DogumYeri { get; set; }
        public virtual Ogrenci? TakimLideri { get; set; }
        public virtual IletisimBilgi? IletisimBilgileri { get; set; }
        public virtual ICollection<Ogrenci> TakimUyeleri { get; set; }

        public virtual ICollection<Sehir> FavoriSehirler { get; set; }
    }
}
