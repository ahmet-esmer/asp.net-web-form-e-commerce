
namespace ModelLayer
{
    public class KullaniciFatura
    {

        public int Id { get; set; }
        public int UyeId { get; set; }
        public string AdSoyad { get; set; }
        public string TCNo { get; set; }
        public string Unvan  { get; set; }
        public string VergiNo { get; set; }
        public string VergiDairesi { get; set; }
        public string FaturaAdresi { get; set; }
        public bool FaturaCinsi { get; set; }
      


        public KullaniciFatura()
        {

        }

    }
}

