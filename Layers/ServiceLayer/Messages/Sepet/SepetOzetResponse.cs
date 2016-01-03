using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.Messages.Sepet
{
    public class SepetOzetResponse
    {

        public decimal FiyatToplam { get; set; }
        public int Adet { get; set; }


        public SepetOzetResponse()
        {

        }
    }
}
