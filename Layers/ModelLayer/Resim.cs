
namespace ModelLayer
{
     public class Resim
    {
         public int id { get; set; }
         public string resimAdi { get; set; }
         public string resimBaslik { get; set; }
         public int sira { get; set; }
         public string parametre { get; set; }

         public Resim()
         {

         }

       public Resim(int id,string resimAdi,int sira,  string resimBaslik)
      {
          this.id = id;
          this.resimAdi = resimAdi;
          this.sira = sira;
          this.resimBaslik = resimBaslik;
      }
    }
}
