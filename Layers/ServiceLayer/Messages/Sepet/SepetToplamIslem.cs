using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using ModelLayer;
using BusinessLayer;

namespace ServiceLayer.Messages.Sepet
{
    public class SepetToplamIslem
    {
        public int UrunId { get; set; }
        public int Miktar { get; set; }
        public int Kampanya { get; set; }
        public decimal Fiyat { get; set; }
        public decimal BirimFiyat { get; set; }
        public int KDV { get; set; }
        public string Doviz { get; set; }
        public string Indirim { get; set; }
        public int HavaleIndirim { get; set; }
        public int Hediye { get; set; }


        public SepetToplamIslem()
        {
        }

        // Nolmal
        internal static SepetResponse SepetToplamIslemleri(List<SepetToplamIslem> sepetToplam)
        {
            SepetResponse response = new SepetResponse();

            foreach (SepetToplamIslem spt in sepetToplam)
            {
                decimal kdv = 0;
                decimal dovizKontlorFiyat = 0;
                decimal birim = 0;
                decimal indirim = 0;
                UrunIndirim urunIndirim;

                dovizKontlorFiyat = DovizKurFiyati(spt.Fiyat, spt.Doviz);

                if (spt.Hediye == 0)
                {
                    //İndirim İşlemi
                    urunIndirim = UrunIndirimDB.UrunKampanyaGetir(spt.UrunId, spt.Miktar);
                    indirim = (dovizKontlorFiyat * urunIndirim.Adet) * urunIndirim.Oran / 100;
                    response.Indirim += indirim;
                }

                // Birim Toplam İşlem 
                birim = dovizKontlorFiyat * spt.Miktar;
                response.BirimFiyatToplam += birim;
                birim -= indirim;

                // Kdv işlem 
                kdv = birim * spt.KDV / 100;
                response.KDVToplam += kdv;

                // Fiyat Toplam
                response.FiyatToplam += birim + kdv;
            }

            response.KargoFiyat = KargoDB.KargoFiyat(KullaniciOperasyon.GetId());

            response.FiyatToplam += response.KargoFiyat;
            return response;
        }

        internal static SepetOzetResponse SepetToplamOzet(List<SepetToplamIslem> sepetToplam)
        {
            SepetOzetResponse response = new SepetOzetResponse();

            foreach (SepetToplamIslem spt in sepetToplam)
            {
                decimal kdv = 0;
                decimal dovizKontlorFiyat = 0;
                decimal birim = 0;
                decimal indirim = 0;
                UrunIndirim urunIndirim;

                dovizKontlorFiyat = DovizKurFiyati(spt.Fiyat, spt.Doviz);

                if (spt.Hediye == 0)
                {
                    //İndirim İşlemi
                    urunIndirim = UrunIndirimDB.UrunKampanyaGetir(spt.UrunId, spt.Miktar);
                    indirim = (dovizKontlorFiyat * urunIndirim.Adet) * urunIndirim.Oran / 100;
                }

                // Birim Toplam İşlem 
                birim = dovizKontlorFiyat * spt.Miktar;
                birim -= indirim;

                // Kdv işlem 
                kdv = birim * spt.KDV / 100;

                // Fiyat Toplam
                response.FiyatToplam += birim + kdv;
                response.Adet += spt.Miktar;
            }

            return response;
        }

        // havale İşlemi için
        internal static SepetResponse SepetToplamIslemleriHavale(List<SepetToplamIslem> sepetToplam)
        {
            SepetResponse response = new SepetResponse();

            foreach (SepetToplamIslem spt in sepetToplam)
            {
                decimal kdv = 0;
                decimal dovizKontlorFiyat = 0;
                decimal birim = 0;
                decimal indirim = 0;
                decimal havaleIndirim = 0;
                UrunIndirim urunIndirim;

                dovizKontlorFiyat = DovizKurFiyati(spt.Fiyat, spt.Doviz);

                if (spt.Hediye == 0)
                {
                    //İndirim İşlemi
                    urunIndirim = UrunIndirimDB.UrunKampanyaGetir(spt.UrunId, spt.Miktar);
                    indirim = (dovizKontlorFiyat * urunIndirim.Adet) * urunIndirim.Oran / 100;
                    response.Indirim += indirim;
                }

                // Birim Toplam İşlem 
                birim = dovizKontlorFiyat * spt.Miktar;
                response.BirimFiyatToplam += birim;
                birim -= indirim;

                // Havale İndirimi
                havaleIndirim = (birim * spt.HavaleIndirim) / 100;
                response.HavaleIndirim += havaleIndirim;
                birim -= havaleIndirim;

                // Kdv işlem 
                kdv = birim * spt.KDV / 100;
                response.KDVToplam += kdv;

                // Fiyat Toplam
                response.FiyatToplam += birim + kdv;
            }

            response.KargoFiyat = KargoDB.KargoFiyat(KullaniciOperasyon.GetId());
            response.FiyatToplam += response.KargoFiyat;

            return response;
        }

        // Sepet Birim Toplam
        public static string SepetBirimToplam(decimal urunFiyat, int miktar, string doviz)
        {
            decimal toplam = 0;
            decimal gelenFiyat = DovizKurFiyati(urunFiyat, doviz);

            toplam = gelenFiyat * miktar;

            return toplam.ToString("c");
        }

        public static decimal DovizKurFiyati(decimal urunFiyat, string doviz)
        {
            decimal DovizKur = 0;
            decimal donenDeger = 0;

            //SqlParameter[] dovizParam = new SqlParameter[1];

            //if (doviz == "USD")
            //{
            //    dovizParam[0] = new SqlParameter("@paremetre", "USD");
            //    DovizKur = (decimal)SqlHelper.ExecuteScalar(ConnectionString.Get, CommandType.StoredProcedure, "dovizKur_Getir", dovizParam);
            //}
            //else if (doviz == "EURO")
            //{
            //    dovizParam[0] = new SqlParameter("@paremetre", "EURO");
            //    DovizKur = (decimal)SqlHelper.ExecuteScalar(ConnectionString.Get, CommandType.StoredProcedure, "dovizKur_Getir", dovizParam);
            //}


            donenDeger = urunFiyat * DovizKur;


            if (doviz == "TL")
            {
                donenDeger = urunFiyat;
            }

            return donenDeger;
        }

        // Sepet Toplam Fiyat
        //internal static decimal SepetToplamFiyat(List<SepetToplamIslem> sepetToplam)
        //{
        //    SepetResponse response = new SepetResponse();

        //    foreach (SepetToplamIslem spt in sepetToplam)
        //    {
        //        decimal kdv = 0;
        //        decimal dovizKontlorFiyat = 0;
        //        decimal birim = 0;
        //        decimal indirim = 0;
        //        UrunIndirim urunIndirim;

        //        dovizKontlorFiyat = DovizKurFiyati(spt.Fiyat, spt.Doviz);

        //        if (spt.Hediye == 0)
        //        {
        //            //İndirim İşlemi
        //            urunIndirim = UrunIndirimDB.UrunKampanyaGetir(spt.UrunId, spt.Miktar);
        //            indirim = (dovizKontlorFiyat * urunIndirim.Adet) * urunIndirim.Oran / 100;
        //            response.Indirim += indirim;
        //        }

        //        // Birim Toplam İşlem 
        //        birim = dovizKontlorFiyat * spt.Miktar;
        //        response.BirimFiyatToplam += birim;
        //        birim -= indirim;

        //        // Kdv işlem 
        //        kdv = birim * spt.KDV / 100;
        //        response.KDVToplam += kdv;

        //        // Fiyat Toplam
        //        response.FiyatToplam += birim + kdv;
        //    }

        //    response.KargoFiyat = KargoDB.KargoFiyat(KullaniciOperasyon.GetId());

        //    return response.FiyatToplam += response.KargoFiyat;
        //}


        // Masterpage için özen bilgilendirme

       
    }
}
