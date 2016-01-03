using System;

namespace ModelLayer
{
     public class Kategori      
    {
         public int id { get; set; }
         public int anaId { get; set; }
         public string kategoriadi { get; set; }
         public Boolean durum { get; set; }
         public int sira { get; set; }
         public string serial { get; set; }
         public int count { get; set; }
         public string title { get; set; }
         public string description { get; set; }
         public string keywords { get; set; }
         public string resimAdi { get; set; }


  

        public Kategori()
        {
        }

         public Kategori(int id, int anaId,string kategoriadi, Boolean durum,int sira, string serial)
         {

             this.id = id;
             this.anaId = anaId;
             this.kategoriadi = kategoriadi;
             this.durum = durum;
             this.sira = sira;
             this.serial = serial;
         }

         public Kategori(int id, int anaId, string kategoriadi, Boolean durum, int sira, string serial, string resimAdi,string title,string description, string keywords )
         {

             this.id = id;
             this.anaId = anaId;
             this.kategoriadi = kategoriadi;
             this.durum = durum;
             this.sira = sira;
             this.serial = serial;
             this.resimAdi = resimAdi;
             this.title = title;
             this.description = description;
             this.keywords = keywords;
         }


         public Kategori(int id, string kategoriadi, string serial)
         {

             this.id = id;
             this.kategoriadi = kategoriadi;
             this.serial = serial;
         }


         public Kategori( string kategoriadi, string serial)
         {
             this.kategoriadi = kategoriadi;
             this.serial = serial;
         }

      

         public Kategori(int id, string kategoriadi, string serial, int count)
         {

             this.id = id;
             this.kategoriadi = kategoriadi;
             this.serial = serial;
             this.count = count;
         }

        }
}
