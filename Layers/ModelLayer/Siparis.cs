using System;

namespace ModelLayer
{
    public class Siparis
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public string AdiSoyadi { get; set; }
        public string OdemeTipi { get; set; }
        public string SiparisNo { get; set; }
        public string SiparisDurumu { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public int TaksitMiktari { get; set; }
        public decimal TaksitliGenelToplami { get; set; }

        public Siparis()
        {

        }
    }
}
