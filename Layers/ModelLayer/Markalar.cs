using System;
using System.Collections.Generic;

namespace ModelLayer
{
  public class Markalar
    {
      public string markaUrunSayisi { get; set; }
      public int id { get; set; }
      public string marka_adi { get; set; }
      public bool durum { get; set; }
      public bool disbrutor { get; set; }
      public int sira { get; set; }



      public Markalar(string markaUrunSayisi, int id, string marka_adi, Boolean durum, Boolean disbrutor, int sira)
      {
            this.markaUrunSayisi = markaUrunSayisi;
            this.id = id;
            this.marka_adi = marka_adi;
            this.durum = durum;
            this.disbrutor = disbrutor;
            this.sira = sira;
      }

        public Markalar( int id, string marka_adi)
        {
            this.id = id;
            this.marka_adi = marka_adi;
        }
    }

  public class MarkaLink
  {
      public int Id { get; set; }
      public string MarkaAdi { get; set; }
      public string Link { get; set; }
      public int Adet { get; set; }
      //public string Serial  { get; set; }
      //public string Kategori { get; set; }
      //public string Title { get; set; }
  }

  public class MarkaList : List<Markalar>
  { 
  
  
  }
}
