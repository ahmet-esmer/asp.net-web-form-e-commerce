using System;

namespace ModelLayer
{
    public class KullaniciPuan
    {

        public int Id { get; set; }
        public int UyeId { get; set; }
        public int GenelId { get; set; }
        public string PuanKod { get; set; }
        public string Aciklama { get; set; }
        public decimal PuanTL { get; set; }
        public DateTime KazanmaTarih { get; set; }
        public DateTime KulanimTarih { get; set; }
        public Boolean  Durum { get; set; }


        public KullaniciPuan()
        {

        }

    }
}

