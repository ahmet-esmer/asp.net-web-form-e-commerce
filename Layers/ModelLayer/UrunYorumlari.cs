using System;

namespace ModelLayer
{
   public class UrunYorumlari
    {

       public int Id { get; set; }
       public int UyeId { get; set; }
       public int UrunId { get; set; }
       public string AdiSoyadi { get; set; }
       public string UrunAdi { get; set; }
       public int DegerKiriteri { get; set; }
       public string Yorum { get; set; }
       public string Sehir { get; set; }
       public DateTime Tarih { get; set; }
       public Boolean IsimGoster { get; set; }
       public Boolean Durum { get; set; }


        public UrunYorumlari()
        {
           
        }
         
    }
}
       
