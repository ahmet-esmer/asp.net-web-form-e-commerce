using System;

namespace ModelLayer
{
    public class Kullanici
    {

        public int Id { get; set; }
        public string AdiSoyadi { get; set; }
        public string Gsm { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string EPosta { get; set; }
        public DateTime KayitTarihi { get; set; }
        public int GirisSayisi { get; set; }
        public string Cinsiyet { get; set; }
        public string Sehir { get; set; }
        public string KullaniciTipi { get; set; }
        public string Sifre { get; set; }
        public Boolean Durum { get; set; }
        public Boolean PostaGonder { get; set; }
        public Boolean SMSGonder { get; set; }
        public int SiparisAdet { get; set; }



        public Kullanici(int id, string adiSoyadi, string ePosta, string kulaniciTip, int siparisAdet)
        {
            this.Id = id;
            this.AdiSoyadi = adiSoyadi;
            this.EPosta = ePosta;
            this.KullaniciTipi = kulaniciTip;
            this.SiparisAdet = siparisAdet;
        }




        public Kullanici()
        {

        }

    }
}

