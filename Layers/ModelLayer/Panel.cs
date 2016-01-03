using System;

namespace ModelLayer
{
  public  class Pannel
    {
        private string resim_adi;
        private int id;
        private string resim_baslik;
        private Boolean durum;
        private string parametre;

        public string Parametre
        {
            get { return parametre; }
            set { parametre = value; }
        }

   
      public string Resim_adi
      {
          get { return resim_adi; }
          set { resim_adi = value; }
      }
      public int Id
      {
          get { return id; }
          set { id = value; }
      }

      public string Resim_baslik
      {
          get { return resim_baslik; }
          set { resim_baslik = value; }
      }
      public Boolean Durum
      {
          get { return durum; }
          set { durum = value; }
      }

      public Pannel(string resim_adi, int id, string resim_baslik, Boolean durum, string parametre)
      {
          this.resim_adi = resim_adi;
          this.id = id;
          this.resim_baslik = resim_baslik;
          this.durum = durum;
          this.parametre = parametre;
      }

      public Pannel(string resim_adi, int id, string resim_baslik)
      {
          this.resim_adi = resim_adi;
          this.id = id;
          this.resim_baslik = resim_baslik;
      }

      public Pannel(string resim_adi, string resim_baslik)
      {
          this.resim_adi = resim_adi;
          this.resim_baslik = resim_baslik;
      }

      public Pannel(string resim_adi)
      {
          this.resim_adi = resim_adi;
      }
    }
}
