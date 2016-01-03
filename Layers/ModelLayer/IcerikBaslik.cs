using System;

namespace ModelLayer
{
   public class IcerikBaslik
    {

        public int id   { get; set; }
        public int anaId { get; set; }
        public string kategoriadi { get; set; }
        public Boolean durum { get; set; }
        public string serial { get; set; }
        public int sira { get; set; }
        public string gosterimAlani { get; set; }
        public string link { get; set; }
     



        public IcerikBaslik(int id, int anaId, string kategoriadi, Boolean durum, string serial, int sira, string gosterimAlani, string link)
        {
            this.id = id;
            this.anaId = anaId;
            this.kategoriadi = kategoriadi;
            this.durum = durum;
            this.serial = serial;
            this.sira = sira;
            this.gosterimAlani = gosterimAlani;
            this.link = link;

        }



        public IcerikBaslik(int id, string kategoriadi, string link )
        {
            this.id = id;
            this.kategoriadi = kategoriadi;
            this.link = link;
        }

        public IcerikBaslik()
        { 
        
        }
    }
}
