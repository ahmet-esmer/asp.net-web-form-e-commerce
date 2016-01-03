
namespace ModelLayer
{
    public class SiparisResponse
    {
        public int UyeId { get; set; }
        public string UyeAdi { get; set; }
        public string SiparisNo { get; set; }
        public string FiyatToplam { get; set; }
        public string KDVToplam { get; set; }
        public string KargoFiyat { get; set; }
        public string BirimFiyat { get; set; }
        public string Mail { get; set; }
        public string Tarih { get; set; }
        public string HavaleVeKapi { get; set; }
        public string BankaAdi { get; set; }
        public string OdemeTipi { get; set; }
        public string TeslimAlan { get; set; }
        public string Adres { get; set; }
        public decimal Indirim { get; set; }

        public string Durum { get; set; }
        public decimal KullanilanPara { get; set; }
        public int Taksit { get; set; }
        public string Mesaj { get; set; }

        public KullaniciFatura Fatura { get; set; }

        public KullaniciAdres Adress { get; set; }

        public SiparisResponse()
        {
        }




       
    }
}
