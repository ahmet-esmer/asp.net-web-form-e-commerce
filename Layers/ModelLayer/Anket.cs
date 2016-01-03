using System;

namespace ModelLayer
{
   public  class Anket
    {

      private  int id;
      private int soruId;
      private int anketOy;
      private int toplamOy;
      private string soru;
      private string anketBaslik;
      private DateTime tarih;
      private Boolean durum;

      public Boolean Durum
      {
          get { return durum; }
          set { durum = value; }
      }

      public DateTime Tarih
      {
          get 
         {
              return tarih; 
          }
          set 
          {
              tarih = value; 
          }
      }
       


        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        

        public int SoruId
        {
            get { return soruId; }
            set { soruId = value; }
        }
        

        public string Soru
        {
            get { return soru; }
            set { soru = value; }
        }
        
       public int ToplamOy
        {
          get { return toplamOy; }
          set { toplamOy = value; }
        }

        public int AnketOy
        {
            get { return anketOy; }
            set { anketOy = value; }
        }
        

        public string AnketBaslik
        {
            get { return anketBaslik; }
            set { anketBaslik = value; }
        }

     

        public Anket(int soruId, int anketOy, int toplamOy, string anketBaslik, string soru)
        {
            this.soruId = soruId;
            this.anketOy = anketOy;
            this.toplamOy = toplamOy;
            this.anketBaslik = anketBaslik;
            this.soru = soru; 
        }

       // Anket Soru Güncelleme İşlemi için
        public Anket(int soruId, int anketOy, string soru, Boolean durum)
        {
            this.soruId = soruId;
            this.anketOy = anketOy;
            this.soru = soru;
            this.durum = durum;
        }

        public Anket(int soruId, int anketOy, int toplamOy, string anketBaslik, string soru, DateTime tarih)
        {
            this.soruId = soruId;
            this.anketOy = anketOy;
            this.toplamOy = toplamOy;
            this.anketBaslik = anketBaslik;
            this.soru = soru;       
            this.tarih = tarih;
        }

        public Anket(int id, string anketBaslik,  DateTime tarih)
        {
            this.id = id;
            this.anketBaslik = anketBaslik;
            this.tarih = tarih;
        }
        public Anket(int soruId, string soru)
        {
            this.soruId = soruId;
            this.soru = soru;
        }
    }
}
