using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.Messages.Sepet
{
    public class SepetResponse
    {

        public decimal FiyatToplam { get; set; }
        public decimal KargoFiyat { get; set; }
        public decimal BirimFiyatToplam { get; set; }
        public decimal Indirim { get; set; }
        public decimal HavaleIndirim { get; set; }
        public decimal KDVToplam { get; set; }

        public List<SepetGridResponse> SepetGride { get; set; }

        public SepetResponse()
        {

        }
    }
}
