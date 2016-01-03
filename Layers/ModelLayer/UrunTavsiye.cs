
namespace ModelLayer
{
   public class UrunTavsiye
    {

        private int id;
        private int tavAnaUrunId;
        private int tavUrunId;  
        private int tavSatisMiktari;
        private decimal urunFiyat; 
        private decimal uIndirimFiyat;
        private string urunAdi;
        private string resimAdi;
        private string doviz;
        private string link;

        public string Link
        {
            get { return link; }
            set { link = value; }
        }

        public string Doviz
        {
            get { return doviz; }
            set { doviz = value; }
        }

        public string ResimAdi
        {
            get { return resimAdi; }
            set { resimAdi = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int TavAnaUrunId
        {
            get { return tavAnaUrunId; }
            set { tavAnaUrunId = value; }
        }
        public int TavUrunId
        {
            get { return tavUrunId; }
            set { tavUrunId = value; }
        }
        public int TavSatisMiktari
        {
            get { return tavSatisMiktari; }
            set { tavSatisMiktari = value; }
        }
        public decimal UrunFiyat
        {
            get { return urunFiyat; }
            set { urunFiyat = value; }
        }
        public decimal UIndirimFiyat
        {
            get { return uIndirimFiyat; }
            set { uIndirimFiyat = value; }
        }
        public string UrunAdi
        {
            get { return urunAdi; }
            set { urunAdi = value; }
        }

        public UrunTavsiye(int id, int tavAnaUrunId, int tavUrunId, int tavSatisMiktari, decimal urunFiyat, decimal uIndirimFiyat, string urunAdi, string doviz)
        {
            this.id = id;
            this.tavAnaUrunId = tavAnaUrunId;
            this.tavUrunId = tavUrunId;
            this.tavSatisMiktari = tavSatisMiktari;
            this.urunFiyat = urunFiyat;
            this.uIndirimFiyat = uIndirimFiyat;
            this.urunAdi = urunAdi;
            this.doviz = doviz;
        }


        //public UrunTavsiye(int tavUrunId, decimal urunFiyat, decimal uIndirimFiyat, string urunAdi, string resimAdi, string doviz)
        //{
        //    this.tavUrunId = tavUrunId;
        //    this.urunFiyat = urunFiyat;
        //    this.uIndirimFiyat = uIndirimFiyat;
        //    this.urunAdi = urunAdi;
        //    this.resimAdi = resimAdi;
        //    this.doviz = doviz;
        //}

        public UrunTavsiye()
        {
            // TODO: Complete member initialization
        }
    }
}
