
namespace ModelLayer
{
    public class BankRequest
    {
        public string BankaAdi { get; set; }
        public int BankaId { get; set; }
        public int Taksit { get; set; }
        public decimal TaksitFiyat { get; set; }
        public decimal TaksitToplam { get; set; }
        public double VadeFarki { get; set; }
        public int SiparisId { get; set; }
        public string SiparisNo { get; set; }
        public KrediKart KrediKart { get; set; }
        public PaymentMessage PaymentMessage { get; set; }

        public BankRequest()
        {
            this.KrediKart = new KrediKart();
        }
    }
}
