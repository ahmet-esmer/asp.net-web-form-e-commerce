using System;
using System.Collections.Generic;

namespace ModelLayer
{
    public class SSorulan
    {
  
        public int id { get; set; }
        public string soru { get; set; }
        public DateTime eklenmeTarihi { get; set; }
        public bool durum { get; set; }
        public int sira { get; set; }
        public string cevap { get; set; }

        public SSorulan()
        { 
        
        }

    }

    public class SSorulanList : List<SSorulan>
    {

    }
}
