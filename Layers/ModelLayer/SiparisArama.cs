using System;

namespace ModelLayer
{
    public class SiparisArama
    {

        public int Bitis { get; set; }
        public int Baslangic { get; set; }
        public DateTime BaslangicTarih { get; set; }
        public DateTime BitisTarih { get; set; }
        public int SayfaToplam { get; set; }
        public decimal GenelToplami { get; set; }
        public string SiparisDurumu { get; set; }

        public SiparisArama()
        {

        }
    }
}
