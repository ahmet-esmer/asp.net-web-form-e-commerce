using System;

namespace ModelLayer
{
   public class ZiyaretciDefteri
    {
       public int id  {get; set;}
       public string adSoyad { get; set; }
       public string ePosta { get; set; }
       public string yorum { get; set; }
       public DateTime eklenmeTarihi { get; set; }
       public Boolean durum { get; set; }
       public string sehirAd { get; set; }
       public string ilceAd { get; set; }
       public string yorumCevap { get; set; }

       

        public ZiyaretciDefteri(int id,string adSoyad, string ePosta, string yorum, DateTime eklenmeTarihi, Boolean durum, string sehirAd, string ilceAd, string yorumCevap)
        {
            this.id = id;
            this.adSoyad = adSoyad;
            this.ePosta = ePosta;
            this.yorum = yorum;
            this.eklenmeTarihi = eklenmeTarihi;
            this.durum = durum;
            this.sehirAd = sehirAd;
            this.ilceAd = ilceAd;
            this.yorumCevap = yorumCevap;
        
        }


        public ZiyaretciDefteri(int id, string adSoyad, string yorum, DateTime eklenmeTarihi, string sehirAd, string ilceAd, string yorumCevap)
        {
            this.id = id;
            this.adSoyad = adSoyad;  
            this.yorum = yorum;
            this.eklenmeTarihi = eklenmeTarihi;
            this.sehirAd = sehirAd;
            this.ilceAd = ilceAd;
            this.yorumCevap = yorumCevap;

        }


        public ZiyaretciDefteri(int id,string yorum)
        {
            this.id = id;
            this.yorum = yorum;
        }

        public ZiyaretciDefteri()
        {
            // TODO: Complete member initialization
        }

        
    }
}
