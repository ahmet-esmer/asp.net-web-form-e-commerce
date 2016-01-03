using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelLayer
{
    public  class BankaMesaj
    {
        public int Id { get; set; }
        public int SiparisId { get; set; }
        public string SiparisNo { get; set; }
        public int BankaId { get; set; }
        public int KullaniciId { get; set; }
        public string KartAd { get; set; }
        public string KartNo { get; set; }
        public string KartCV2 { get; set; }
        public string RedMesaj { get; set; }
        public string RedMesajKodu { get; set; }
        public int Taksit { get; set; }
        public int HataSayisi { get; set; }
        public decimal ToplamFiyat { get; set; }
        public DateTime Tarih { get; set; }
        public string AdiSoyadi { get; set; }
        public string BankaAdi { get; set; }

        public BankaMesaj()
        {

        }

    }
}
