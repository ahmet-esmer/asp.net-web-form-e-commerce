using System;
using System.Collections.Generic;

namespace ModelLayer
{
    public class UrunIndirim
    {

       public int Id { get; set; }
       public int UrunId { get; set; }
       public int Oran { get; set; }
       public int Adet { get; set; }
       public decimal Fiyat { get; set; }
       public string StokCinsi { get; set; }
       public int KDV { get; set; }
  
    }


    
}
