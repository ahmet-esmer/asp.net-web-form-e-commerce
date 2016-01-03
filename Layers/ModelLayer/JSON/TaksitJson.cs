using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelLayer;

namespace ModelLayer.JSON
{
    public class TaksitJson
    {
        public string Resim { get; set; }
        public List<Taksit> Taksit { get; set; }

        public TaksitJson()
	    {
            this.Taksit = new List<Taksit>();
	    }
    }

    

    public class Taksit
    {
        public int Id { get; set; }
        public int Ay { get; set; }
        public string AylikFiyat { get; set; }
        public double AylikVade { get; set; }
        public string ToplamFiyat { get; set; }
    }
}
