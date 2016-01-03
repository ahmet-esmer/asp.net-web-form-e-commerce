
namespace ModelLayer
{
    public class SiparisKayit
    {

        public int UyeId { get; set; }
        public string SiparisNo { get; set; }
        public string BankaAdi { get; set; }
        public decimal BirimToplam { get; set; }
        public decimal KdvTutar { get; set; }
        public decimal GenelToplam { get; set; }
        public decimal KullanilanPara { get; set; }
        public decimal Indirim { get; set; }
        public int KargoId { get; set; }
        public decimal KargoFiyat { get; set; }
        public int TaksitMiktari { get; set; }
        public decimal AylikTaksitTutari { get; set; }
        public decimal TaksitliGenelToplam { get; set; }
        public int AdresId { get; set; }
        public int FaturaId { get; set; }
        public string Mesaj { get; set; }
        public SiparisDurum SiparisDurum { get; set; }
        public SiparisTuru SiparisTuru { get; set; }

        public SiparisKayit()
        {
        }
    }
}
