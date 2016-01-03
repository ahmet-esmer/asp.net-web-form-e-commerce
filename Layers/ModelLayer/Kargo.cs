using System;

namespace ModelLayer
{
   public class Kargo
    {

        private int id;
        private string kargoAdi;
        private decimal desi_1_3;
        private decimal desi_4_10;
        private decimal desi_11_20;
        private decimal desi_21_30;
        private decimal desi_31_40;
        private decimal desi_41_50;
        private decimal desi_50;
        private Boolean kapidaOdeme;
        private decimal kapidaOdemeFark;


        public decimal KapidaOdemeFark
        {
            get { return kapidaOdemeFark; }
            set { kapidaOdemeFark = value; }
        }

        public Boolean KapidaOdeme
        {
            get { return kapidaOdeme; }
            set { kapidaOdeme = value; }
        }

        private Boolean durum;

        private decimal kargoFiyat;

        public decimal KargoFiyat
        {
            get { return kargoFiyat; }
            set { kargoFiyat = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string KargoAdi
        {
            get { return kargoAdi; }
            set { kargoAdi = value; }
        }
        public decimal Desi_1_3
        {
            get { return desi_1_3; }
            set { desi_1_3 = value; }
        }
        public decimal Desi_11_20
        {
            get { return desi_11_20; }
            set { desi_11_20 = value; }
        }
        public decimal Desi_4_10
        {
            get { return desi_4_10; }
            set { desi_4_10 = value; }
        }
        public decimal Desi_21_30
        {
            get { return desi_21_30; }
            set { desi_21_30 = value; }
        }
        public decimal Desi_31_40
        {
            get { return desi_31_40; }
            set { desi_31_40 = value; }
        }
        public decimal Desi_41_50
        {
            get { return desi_41_50; }
            set { desi_41_50 = value; }
        }
        public decimal Desi_50
        {
            get { return desi_50; }
            set { desi_50 = value; }
        }
        public Boolean Durum
        {
            get { return durum; }
            set { durum = value; }
        }


       public Kargo(int id,string kargoAdi, decimal desi_1_3, decimal desi_4_10, decimal desi_11_20,decimal desi_21_30,  decimal desi_31_40, decimal desi_41_50, decimal desi_50,Boolean durum)
       {
           this.id = id;
           this.kargoAdi = kargoAdi;
           this.desi_1_3 = desi_1_3;
           this.desi_4_10 = desi_4_10;
           this.desi_11_20 = desi_11_20;
           this.desi_21_30 = desi_21_30;
           this.desi_31_40 = desi_31_40;
           this.desi_41_50 = desi_41_50;
           this.desi_50 = desi_50;
           this.durum = durum;
       }

       public Kargo(int id, string kargoAdi, decimal desi_1_3, decimal desi_4_10, decimal desi_11_20, decimal desi_21_30, decimal desi_31_40, decimal desi_41_50, decimal desi_50, Boolean durum, Boolean kapidaOdeme, decimal kapidaOdemeFarki)
       {
           this.id = id;
           this.kargoAdi = kargoAdi;
           this.desi_1_3 = desi_1_3;
           this.desi_4_10 = desi_4_10;
           this.desi_11_20 = desi_11_20;
           this.desi_21_30 = desi_21_30;
           this.desi_31_40 = desi_31_40;
           this.desi_41_50 = desi_41_50;
           this.desi_50 = desi_50;
           this.durum = durum;
           this.kapidaOdeme = kapidaOdeme;
           this.kapidaOdemeFark = kapidaOdemeFarki;
       }

       public Kargo(string kargoAdi, decimal kargoFiyat, int id)
       {
           this.kargoAdi = kargoAdi;
           this.kargoFiyat = kargoFiyat;
           this.id = id;
       }

    }
}
