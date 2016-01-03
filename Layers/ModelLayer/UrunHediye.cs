using System;
using System.Collections.Generic;

namespace ModelLayer
{
    public class UrunHediye
    {

       public int Id { get; set; }
       public int BaslikId { get; set; }
       public string BaslikAdi { get; set; }
       public string Resim { get; set; }
       public string UrunAdi { get; set; }
       public string Marka { get; set; }
       public string Secenek  { get; set; }
       public int Adet { get; set; }
       public bool Durum { get; set; }
       public List<Secenek> Secenekler { get; set; }

    }


    
}
