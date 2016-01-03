using System;

namespace ModelLayer
{
   public  class GununUrunu
    {
        public string Doviz { get; set; }
        public string ResimAdi { get; set; }
        public string UrunAdi { get; set; }
        public string KategoriAdi { get; set; }
        public int Id { get; set; }
        public Boolean Durum { get; set; }
        public int UrunStok { get; set; }
        public int KiritikStok { get; set; }
        public int KDV { get; set; }
        public string StokCins { get; set; }
        public decimal UrunFiyat { get; set; }
        public decimal UIndirimFiyat { get; set; }
        public DateTime Tarih { get; set; }
        public int GunId { get; set; }
        public decimal SatisFiyat { get; set; }
        public string Aciklama { get; set; }
        public int IndirimYuzde { get; set; }
        public string Link  { get; set; }


        public GununUrunu()
        {

        }
          
    }
}
