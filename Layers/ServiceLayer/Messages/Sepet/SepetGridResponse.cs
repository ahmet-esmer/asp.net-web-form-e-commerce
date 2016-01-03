using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.Messages.Sepet
{
    public class SepetGridResponse
    {
        public string Id { get; set; }
        public string Resim { get; set; }
        public string Urun { get; set; }
        public string StokCins { get; set; }
        public string UrunLink { get; set; }
        public string KDV { get; set; }
        public string Miktar { get; set; }
        public string Fiyat { get; set; }
        public string Birim { get; set; }
        public string SagAdet { get; set; }
        public string SolAdet { get; set; }
        public string SagBilgi { get; set; }
        public string SolBilgi { get; set; }
        public string Kampanya { get; set; }
        public string HediyeHTML { get; set; }
        public string HediyeUrunTekHTML { get; set; }

        public SepetGridResponse()
        {

        }
    }
}
