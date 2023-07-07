using System;
using System.Collections.Generic;

namespace _7TemmuzTeori.Data
{
    public partial class IletisimBilgi
    {
        public int Id { get; set; }
        public string? Telefon { get; set; }
        public string? Email { get; set; }
        public string? Adres { get; set; }

        public virtual Ogrenci Ogrenci { get; set; } = null!;
    }
}
