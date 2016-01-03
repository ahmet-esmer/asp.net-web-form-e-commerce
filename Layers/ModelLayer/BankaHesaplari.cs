using System;

namespace ModelLayer
{
    public class BankaHesaplari
    {
        private int id;
        private string bankaAdi;
        private string sube;
        private string subeKod;
        private string hesapNo;
        private string iban;
        private string hesapAdi;
        private string hesapTipi;
        private Boolean durum;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string BankaAdi
        {
            get { return bankaAdi; }
            set { bankaAdi = value; }
        }
        public string Sube
        {
            get { return sube; }
            set { sube = value; }
        }
        public string SubeKod
        {
            get { return subeKod; }
            set { subeKod = value; }
        }

        public string HesapNo
        {
            get { return hesapNo; }
            set { hesapNo = value; }
        }

        public string Iban
        {
            get { return iban; }
            set { iban = value; }
        }

        public string HesapAdi
        {
            get { return hesapAdi; }
            set { hesapAdi = value; }
        }
        public string HesapTipi
        {
            get { return hesapTipi; }
            set { hesapTipi = value; }
        }
        public Boolean Durum
        {
            get { return durum; }
            set { durum = value; }
        }

        public BankaHesaplari(int id, string bankaAdi, string sube, string subeKod, string hesapNo, string iban, string hesapAdi, string hesapTipi, Boolean durum)
        {

            this.id = id;
            this.bankaAdi = bankaAdi;
            this.sube = sube;
            this.subeKod = subeKod;
            this.hesapNo = hesapNo;
            this.iban = iban;
            this.hesapAdi = hesapAdi;
            this.hesapTipi = hesapAdi;
            this.durum = durum;
        }

        public BankaHesaplari(int id, string bankaAdi, string sube, string subeKod, string hesapNo, string iban, string hesapAdi, string hesapTipi)
        {

            this.id = id;
            this.bankaAdi = bankaAdi;
            this.sube = sube;
            this.subeKod = subeKod;
            this.hesapNo = hesapNo;
            this.iban = iban;
            this.hesapAdi = hesapAdi;
            this.hesapTipi = hesapTipi;
        }


        public BankaHesaplari( string bankaAdi, int id)
        {
            this.bankaAdi = bankaAdi;
            this.id = id;
        }

        public BankaHesaplari()
        {
            // TODO: Complete member initialization
        }
    }
}
