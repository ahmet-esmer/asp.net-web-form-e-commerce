using System;

namespace ModelLayer
{
   public class Urun
    {
       public int id { get; set; }
       public int eskiId { get; set; }
       public int kategoriId { get; set; }
       public int markaId { get; set; }
       public string resimAdi { get; set; }
       public string kategoriadi { get; set; }
       public string markaAdi { get; set; }
       public string urunAdi { get; set; }
       public string urunKodu { get; set; }
       public string kisaAciklama { get; set; }
       public string urunOzellik { get; set; }
       public Boolean durum { get; set; }
       public int sira { get; set; }
       public string stokCins { get; set; }
       public decimal urunFiyat { get; set; }
       public decimal uIndirimFiyat { get; set; }
       public int urunStok { get; set; }
       public int kiritikStok { get; set; }
       public int hit { get; set; }
       public DateTime tarih { get; set; }
       public string doviz { get; set; }
       public int kdv { get; set; }
       public int havaleIndirim { get; set; }
       public int desiMiktari { get; set; }
       public string title {get; set;}
       public string description {get; set;}
       public string keywords {get; set;}
       public string listOzellik { get; set; }
       public string link { get; set; }
       

        public Urun(string resimAdi, int id, string urunAdi,  decimal urunFiyat, decimal uIndirimFiyat,  string doviz, int kdv, string link)
        {
            this.resimAdi = resimAdi;
            this.id = id;
            this.urunAdi = urunAdi;
            this.urunFiyat = urunFiyat;
            this.uIndirimFiyat = uIndirimFiyat;
            this.doviz = doviz;
            this.kdv = kdv;
            this.link = link;
        }


        public Urun(string resimAdi, string kategoriadi, string markaAdi, string urunAdi, int id, Boolean durum, int sira, int urunStok, string stokCins, decimal urunFiyat, decimal uIndirimFiyat, int kiritikStok, string doviz )
        {

            this.resimAdi = resimAdi;
            this.kategoriadi = kategoriadi;
            this.markaAdi = markaAdi;
            this.urunAdi = urunAdi;
            this.id = id;
            this.durum = durum;
            this.sira = sira;
            this.urunStok = urunStok;
            this.stokCins = stokCins;
            this.urunFiyat = urunFiyat;
            this.uIndirimFiyat = uIndirimFiyat;
            this.kiritikStok = kiritikStok;
            this.doviz = doviz;
        
        }

        public Urun()
        {
            // TODO: Complete member initialization
        }
    }
}
