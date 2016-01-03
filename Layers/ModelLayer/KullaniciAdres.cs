
namespace ModelLayer
{
    public class KullaniciAdres
    {

        public int Id { get; set; }
        public int UyeId { get; set; }
        public string Adres { get; set; }
        public string TeslimAlan { get; set; }
        public string Telefon { get; set; }
        public string Sehir { get; set; }
        public int SehirId { get; set; }
     

        public KullaniciAdres()
        {

        }

    }
}

