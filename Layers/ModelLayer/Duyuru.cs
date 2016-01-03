using System;

namespace ModelLayer
{
  public  class Duyuru
    {

        private int id;
        private string duyuru_adi;
        private string duyuru_icerik;
        private DateTime eklenme_tarihi;
        private Boolean durum;
        private string resimAdi;


        public string ResimAdi
        {
            get { return resimAdi; }
            set { resimAdi = value; }
        }



        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Duyuru_adi
        {
            get { return duyuru_adi; }
            set { duyuru_adi = value; }
        }
        public string Duyuru_icerik
        {
            get { return duyuru_icerik; }
            set { duyuru_icerik = value; }
        }
        public DateTime Eklenme_tarihi
        {
            get { return eklenme_tarihi; }
            set { eklenme_tarihi = value; }
        }
        public Boolean Durum
        {
            get { return durum; }
            set { durum = value; }
        }


        public Duyuru(int id, string duyuru_adi, string duyuru_icerik, DateTime eklenme_tarihi, Boolean durum)
        {
            this.id = id;
            this.duyuru_adi = duyuru_adi;
            this.duyuru_icerik = duyuru_icerik;
            this.eklenme_tarihi = eklenme_tarihi;
            this.durum = durum;
        
        }


        public Duyuru(int id, string duyuru_adi, string duyuru_icerik, DateTime eklenme_tarihi, string resimAdi)
        {
            this.id = id;
            this.duyuru_adi = duyuru_adi;
            this.duyuru_icerik = duyuru_icerik;
            this.eklenme_tarihi = eklenme_tarihi;
            this.resimAdi = resimAdi;

        }
        public Duyuru(int id, string duyuru_adi )
        {
            this.id = id;
            this.duyuru_adi = duyuru_adi;

        }

    }
}
