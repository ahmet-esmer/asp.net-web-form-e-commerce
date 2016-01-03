using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.Messages
{
    public class UrunIndirimResponse
    {
        public string Adet { get; set; }
        public string Islem { get; set; }
        public string Indirim { get; set; }
        public string Toplam { get; set; }
    }
}
