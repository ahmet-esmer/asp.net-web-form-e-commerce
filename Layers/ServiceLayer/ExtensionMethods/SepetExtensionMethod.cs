using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceLayer.Messages.Sepet;
using BusinessLayer;
using ModelLayer;
using DataAccessLayer;

namespace ServiceLayer.ExtensionMethods
{
    public static class SepetExtensionMethod
    {
        public static SepetResponse ConvertToSepetResponse(this List<Sepet> sepet)
        {
            SepetResponse response;

            List<SepetGridResponse> sepetGridResponse = new List<SepetGridResponse>();
            List<SepetToplamIslem> sepetToplamResponse = new List<SepetToplamIslem>();
            UrunHediyeTek hediyeTek = new UrunHediyeTek();

            foreach (Sepet spt in sepet)
            {
                SepetGridResponse res = new SepetGridResponse();

                res.Id = spt.SepetId.ToString();
                res.Resim = spt.ResimAdi;
                res.StokCins = spt.StokCins;
                res.Urun = spt.UrunAdi;
                res.UrunLink = LinkBulding.Urun("sepet", spt.UrunId, spt.UrunAdi);
                res.KDV = AritmetikIslemler.sepetKDV(spt.UrunKDV);
                res.Miktar = spt.Miktar.ToString();
                res.Fiyat = SepetOperasyon.FiyatVeParaBirimi(spt.UrunFiyat, spt.Doviz);
                res.Birim = SepetToplamIslem.SepetBirimToplam(spt.UrunFiyat, spt.Miktar, spt.Doviz);
                res.SagAdet = spt.SagAdet.ToString();
                res.SolAdet = spt.SolAdet.ToString();
                res.SolBilgi = SepetOperasyon.SolBilgiHtml(spt.SolBilgi);
                res.SagBilgi = SepetOperasyon.SagBilgiHtml(spt.SagBilgi);
                res.HediyeHTML = HediyeHtml(spt.ObjHediye);
               
                if (hediyeTek.UrunAdi == null)
                {
                    hediyeTek = UrunDB.HediyeUrun(spt.UrunId);
                    res.HediyeUrunTekHTML = HediyeUrunTekHtml(hediyeTek);
                }

                sepetGridResponse.Add(res);

                // Sepet Toplam Fiyat İşlemi
                SepetToplamIslem islem = sepetToplamResponse.Find(item => item.UrunId == spt.UrunId);
              
                if (islem != null )
                {
                    sepetToplamResponse.Remove(islem);
                    islem.Miktar += spt.Miktar;
                    if (spt.ObjHediye.Id > 0)
                    {
                        islem.Hediye = spt.ObjHediye.Id;  
                    }
                    
                    sepetToplamResponse.Add(islem);
                }
                else
                {
                    sepetToplamResponse.Add(new SepetToplamIslem
                    {
                        UrunId = spt.UrunId,
                        Fiyat = spt.UrunFiyat,
                        Doviz = spt.Doviz,
                        Miktar = spt.Miktar,
                        KDV = spt.UrunKDV,
                        Hediye = spt.ObjHediye.Id
                    });
                }
            }

            response = SepetToplamIslem.SepetToplamIslemleri(sepetToplamResponse);

            response.SepetGride = sepetGridResponse;

            return response;
        }

        public static SepetOzetResponse ConvertToSepetOzetResponse(this List<Sepet> sepet)
        {
            SepetOzetResponse response;
            List<SepetToplamIslem> sepetToplamResponse = new List<SepetToplamIslem>();

            foreach (Sepet spt in sepet)
            {
                SepetToplamIslem islem = sepetToplamResponse.Find(item => item.UrunId == spt.UrunId);

                if (islem != null)
                {
                    sepetToplamResponse.Remove(islem);
                    islem.Miktar += spt.Miktar;
                    if (spt.ObjHediye.Id > 0)
                    {
                        islem.Hediye = spt.ObjHediye.Id;
                    }
                    sepetToplamResponse.Add(islem);
                }
                else
                {
                    sepetToplamResponse.Add(new SepetToplamIslem
                    {
                        UrunId = spt.UrunId,
                        Fiyat = spt.UrunFiyat,
                        Doviz = spt.Doviz,
                        Miktar = spt.Miktar,
                        KDV = spt.UrunKDV,
                        Hediye = spt.ObjHediye.Id
                    });
                }
            }

            response = SepetToplamIslem.SepetToplamOzet(sepetToplamResponse);

            return response;
        }

        public static SepetResponse ConvertToSepetResponseHavale(this List<Sepet> sepet)
        {
            SepetResponse response;

            List<SepetGridResponse> sepetGridResponse = new List<SepetGridResponse>();
            List<SepetToplamIslem> sepetToplamResponse = new List<SepetToplamIslem>();
            UrunHediyeTek hediyeTek = new UrunHediyeTek();

            foreach (Sepet spt in sepet)
            {
                SepetGridResponse res = new SepetGridResponse();

                res.Id = spt.SepetId.ToString();
                res.Resim = spt.ResimAdi;
                res.StokCins = spt.StokCins;
                res.Urun = spt.UrunAdi;
                res.UrunLink = LinkBulding.Urun("sepet", spt.UrunId, spt.UrunAdi);
                res.KDV = AritmetikIslemler.sepetKDV(spt.UrunKDV);
                res.Miktar = spt.Miktar.ToString();
                res.Fiyat = SepetOperasyon.FiyatVeParaBirimi(spt.UrunFiyat, spt.Doviz);
                res.Birim = SepetToplamIslem.SepetBirimToplam(spt.UrunFiyat, spt.Miktar, spt.Doviz);
                res.SagAdet = spt.SagAdet.ToString();
                res.SolAdet = spt.SolAdet.ToString();
                res.SolBilgi = SepetOperasyon.SolBilgiHtml(spt.SolBilgi);
                res.SagBilgi = SepetOperasyon.SagBilgiHtml(spt.SagBilgi);
                res.HediyeHTML = HediyeHtml(spt.ObjHediye);


                if (hediyeTek.UrunAdi == null)
                {
                    hediyeTek = UrunDB.HediyeUrun(spt.UrunId);
                    res.HediyeUrunTekHTML = HediyeUrunTekHtml(hediyeTek);
                }

                sepetGridResponse.Add(res);

                // Sepet Toplam Fiyat İşlemi
                SepetToplamIslem islem = sepetToplamResponse.Find(item => item.UrunId == spt.UrunId);

                if (islem != null)
                {
                    sepetToplamResponse.Remove(islem);
                    islem.Miktar += spt.Miktar;
                    if (spt.ObjHediye.Id > 0)
                    {
                        islem.Hediye = spt.ObjHediye.Id;
                    }

                    sepetToplamResponse.Add(islem);
                }
                else
                {
                    sepetToplamResponse.Add(new SepetToplamIslem
                    {
                        UrunId = spt.UrunId,
                        Fiyat = spt.UrunFiyat,
                        Doviz = spt.Doviz,
                        Miktar = spt.Miktar,
                        KDV = spt.UrunKDV,
                        HavaleIndirim = spt.HavaleIndirim,
                        Hediye = spt.ObjHediye.Id
                    });
                }
            }

            response = SepetToplamIslem.SepetToplamIslemleriHavale(sepetToplamResponse);

            //Tek Hediye Ürün Id Alma
            //response.HediyeTekId = hediyeTek.Id;

            response.SepetGride = sepetGridResponse;

            return response;
        }

        //// Toplam Fiyat Havale
        //public static decimal GetTotalPrice(this List<Sepet> sepet)
        //{
        //    List<SepetToplamIslem> sepetToplamResponse = new List<SepetToplamIslem>();

        //    foreach (Sepet spt in sepet)
        //    {
        //        // Sepet Toplam Fiyat İşlemi
        //        SepetToplamIslem islem = sepetToplamResponse.Find(item => item.UrunId == spt.UrunId);

        //        if (islem != null)
        //        {
        //            sepetToplamResponse.Remove(islem);
        //            islem.Miktar += spt.Miktar;
        //            if (spt.ObjHediye.Id > 0)
        //            {
        //                islem.Hediye = spt.ObjHediye.Id;
        //            }

        //            sepetToplamResponse.Add(islem);
        //        }
        //        else
        //        {
        //            sepetToplamResponse.Add(new SepetToplamIslem
        //            {
        //                UrunId = spt.UrunId,
        //                Fiyat = spt.UrunFiyat,
        //                Doviz = spt.Doviz,
        //                Miktar = spt.Miktar,
        //                KDV = spt.UrunKDV,
        //                Hediye = spt.ObjHediye.Id
        //            });
        //        }
        //    }

        //    return SepetToplamIslem.SepetToplamFiyat(sepetToplamResponse);
        //}

        private static string HediyeUrunTekHtml(UrunHediyeTek hediyeTek)
        {
            StringBuilder sb = new StringBuilder();

            if (hediyeTek.UrunAdi  != null)
            {
                sb.Append("<div class='sepet-hediye'>");
                sb.Append("<div class='hediyeSol'>");
                sb.Append("<img class='hediyeImg'  src='/Products/Small/" + hediyeTek.ResimAdi + "' />");
                sb.Append("</div>");

                sb.Append("<div class='hediyeSag'>");
                sb.Append("<span class='hediyeTitle' >Hediye Solüsyon</span>");
                sb.Append(hediyeTek.UrunAdi);
                sb.Append("<br/>");
                sb.Append("<span class='hediye-fiyat'>");
                sb.Append(hediyeTek.UrunFiyat.ToString("n") + hediyeTek.Doviz);
                sb.Append("</span>");
                sb.Append("</div");
                sb.Append("<div class='clear'></div>");
                sb.Append("</div>");
            }

            return sb.ToString();
         
        }

        private static string HediyeHtml(UrunHediye hediye)
        {
            StringBuilder sb = new StringBuilder();

            if (hediye.Id > 0)
            {
                sb.Append("<div class='sepet-hediye'>");
                sb.Append("<div class='hediyeSol'>");
                sb.Append("<img class='hediyeImg'  src='/Products/Small/" + hediye.Resim + "'  />");
                sb.Append("</div>");

                sb.Append("<div class='hediyeSag'>");
                sb.Append("<span class='hediyeTitle' >Hediye Ürün</span>");
                sb.Append(hediye.UrunAdi);
                sb.Append("<br/>");
                sb.Append(hediye.Secenek);
                sb.Append("</div");
                sb.Append("<div class='clear'></div>");
                sb.Append("</div>");
            }

            return sb.ToString();
        }

    }
}
