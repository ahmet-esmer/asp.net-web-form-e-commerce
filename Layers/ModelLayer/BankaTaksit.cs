
namespace ModelLayer
{
    public class BankaTaksit
    {
        public int Id  {get; set;}
        public int Taksit  {get; set;}
        public decimal TaksitFiyat  {get; set;}
        public decimal TaksitToplam  {get; set;}
        public string BankaAdi  {get; set;}
        public int BankaId { get; set; }
        public double VadeFarki  {get; set;}


        public BankaTaksit(int taksit, decimal taksitFiyat, decimal taksitToplam)
        {
            this.Taksit = taksit;
            this.TaksitFiyat = taksitFiyat;
            this.TaksitToplam = taksitToplam;
        }

        public BankaTaksit(int id, int taksit, decimal taksitFiyat, decimal taksitToplam)
        {
            this.Id = id;
            this.Taksit = taksit;
            this.TaksitFiyat = taksitFiyat;
            this.TaksitToplam = taksitToplam;
        }

        public BankaTaksit()
        {
        }

    }
}
