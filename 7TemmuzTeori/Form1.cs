using _7TemmuzTeori.Data;
using Microsoft.EntityFrameworkCore;



namespace _7TemmuzTeori
{
    public partial class Form1 : Form
    {


        Boost13DbContext db = new Boost13DbContext();
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Boost13DbContext context = new Boost13DbContext())
            {

                //var result = context.Ogrenciler.SqlQuery("SELECT * FROM Ogrenciler")
                //.ToList();
                var result2 = context.Ogrenciler.Select(x => new { x.Id, x.Ad, x.Soyad }).ToList();

                dataGridView1.DataSource = result2;

                //var result3 = context;

                //dataGridView1.DataSource = result3;

                //var result = context.Ogrenciler.Select(x =>
                //new { x.Ad, x.Soyad }).ToList();
                //dataGridView1.DataSource = result;


                //dataGridView1.AutoGenerateColumns = false; // Otomatik sütun üretimini devre dýþý býrakýn

                //dataGridView1.Columns.Add("Ad", "Ad"); // "Ad" sütunu ekle
                //dataGridView1.Columns.Add("Soyad", "Soyad"); // "Soyad" sütunu ekle

                //var result = context.Ogrenciler.ToList();
                //dataGridView1.DataSource = result;



                // SQL sorgusunu tetikleme kodunu buraya yazabilirsiniz
                // Örneðin: var result = context.TabloAdi.SqlQuery("SELECT * FROM TabloAdi").ToList();
                //         foreach (var item in result)
                //         {
                //             // Verileri iþleme kodunu buraya yazabilirsiniz
                //         }
            }

        }

        private void button2_Click(object sender, EventArgs e)

        {

            using (Boost13DbContext context = new Boost13DbContext())
            {


                //var result2 = context.Ogrenciler.Include(x => x.TakimLideri).Select(x => new { x.Ad, x.Soyad, takimlideriad = x.TakimLideri.Ad, takimlideriyas = SqlFunctions.DateDiff("year", x.TakimLideri.DogumTarihi, DateTime.Now) }).ToList();

                //dataGridView1.DataSource = result2;

                dataGridView1.DataSource = context.Ogrenciler
                 .Include(e => e.TakimLideri)
                 .Select(e => new
                 {
                     e.Ad,
                     e.Soyad,
                     takimLider = e.TakimLideri.Ad,
                     takimLiderYas = ((DateTime.Now - e.TakimLideri.DogumTarihi) / 365)
                 })
                 .ToList();

                //var result2 = context.Ogrenciler.Include(x => x.Sehirler).Select(x => new { x.Id, x.Ad, x.Soyad }).ToList();

                //dataGridView1.DataSource = result2;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            using (Boost13DbContext context = new Boost13DbContext())
            {
                dataGridView1.DataSource = context.Sehirler
                .Include(e => e.Sevenler)
                .Select(e => new
                {
                    e.SehirAd,
                    SevenlerAd = string.Join(", ", e.Sevenler.Select(x => x.Ad))
                }).ToList()
        .Where(x => !string.IsNullOrEmpty(x.SevenlerAd));

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = db.Ogrenciler
                 .Include(e => e.IletisimBilgileri)
                 .Select(e => new
                 {
                     tamad = e.Ad + " " + e.Soyad,
                     eposta = e.IletisimBilgileri.Email,
                     telefon = e.IletisimBilgileri.Telefon,
                     adres = e.IletisimBilgileri.Adres


                 })
                 .ToList();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Ogrenciler
                .Where(e => e.Cinsiyet == "e")
                .Select(e => new
                {
                    e.Ad,
                    favoriSehirler = string.Join(", ", e.Sehirler.OrderBy(s => s.Id).Select(s => s.SehirAd))

                })
                .ToList();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Ogrenciler
                    .Where(e => e.DogumYeri != null)
                    .Select(e => new
                    {
                        e.Ad,
                        e.DogumYeri.SehirAd,
                        dogumNufus = e.DogumYeri.Nufus
                    })
                   .ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Bolgeler
                    //.Include(e => e.Sehirler)
                    .Select(e => new
                    {
                        e.BolgeAd,
                        sehirler = (e.Sehirler.Select(s => s.SehirAd)).Count()
                    })
                   .ToList();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Bolgeler.Include(e => e.Sehirler)
            //.Where(e => e.BolgeAd != null)
            .Select(e => new
            {
                e.BolgeAd,
                OgrenciIsimleri = string.Join(", ", e.Sehirler.SelectMany(s => s.Doganlar).Select(o => o.Ad))
            })
            .ToList();




        }

        private void button12_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Ogrenciler
                 //.Include(e => e.IletisimBilgileri)
                 .Select(e => new
                 {
                     tamad = e.Ad + " " + e.Soyad,
                     eposta = e.IletisimBilgileri.Email,
                     telefon = e.IletisimBilgileri.Telefon,
                     adres = e.IletisimBilgileri.Adres


                 })
                 .ToList();
        }
    }
}