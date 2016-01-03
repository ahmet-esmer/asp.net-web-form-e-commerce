using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelLayer
{
    public class KrediKartOdeme 
    {


        public int SiparisId { get; set; }
        public string SiparisNo { get; set; }
        public string UyeAdi { get; set; }
        public string SiparisDurumu { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public int TaksitMiktari { get; set; }
        public decimal TaksitliGenelToplami { get; set; }
        public string KartSahibi { get; set; }
        public string KartNo { get; set; }
        public string OnayKodu { get; set; }
        public string ReferansNo { get; set; }
        public string BankaAdi { get; set; }


        public KrediKartOdeme()
        { 
        
        }

    }
}
