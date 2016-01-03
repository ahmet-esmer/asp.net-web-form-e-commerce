using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LoggerLibrary;
using MailLibrary;
using ModelLayer;
using BusinessLayer;
using System.Text;

namespace DataAccessLayer
{
    public  class SiparisDB: BaseDB
    {
        public static string SiparisNo(int uyeId)
        {
            string siparisId = " ";
            try
            {
                using (SqlConnection baglan = new SqlConnection(ConnectionString))
                {
                    baglan.Open();
                    using (SqlCommand kmt = new SqlCommand())
                    {
                        kmt.Connection = baglan;
                        kmt.CommandText = "siparisNo_Olustur";
                        kmt.CommandType = CommandType.StoredProcedure;
                        kmt.Parameters.Add("@uyeId", SqlDbType.Int);
                        kmt.Parameters["@uyeId"].Value = uyeId;
                        kmt.Parameters.Add("@siparisNo", SqlDbType.Int);
                        kmt.Parameters["@siparisNo"].Direction = ParameterDirection.Output;
                        kmt.ExecuteNonQuery();

                        siparisId = kmt.Parameters["@siparisNo"].Value.ToString();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return siparisId;
        }

        public static int Kayit(SiparisKayit siparis)
        {
            int siparisId = 0;
            try
            {
                using (SqlConnection baglan = new SqlConnection(ConnectionString))
                {
                    baglan.Open();
                    using (SqlCommand kmt = new SqlCommand())
                    {
                        kmt.Connection = baglan;
                        kmt.CommandText = "siparis_KayitEkle";
                        kmt.CommandType = CommandType.StoredProcedure;
                        kmt.Parameters.Add("@uyeId", SqlDbType.Int);
                        kmt.Parameters["@uyeId"].Value = siparis.UyeId;
                        kmt.Parameters.Add("@siparisNo", SqlDbType.NVarChar);
                        kmt.Parameters["@siparisNo"].Value = siparis.SiparisNo;
                        kmt.Parameters.Add("@odemeTipi", SqlDbType.Int);
                        kmt.Parameters["@odemeTipi"].Value = (int)siparis.SiparisTuru;
                        kmt.Parameters.Add("@bankaAdi", SqlDbType.NVarChar);
                        kmt.Parameters["@bankaAdi"].Value = siparis.BankaAdi;
                        kmt.Parameters.Add("@siparisBirimToplami", SqlDbType.Money);
                        kmt.Parameters["@siparisBirimToplami"].Value = siparis.BirimToplam;
                        kmt.Parameters.Add("@siparisKdvTutari", SqlDbType.Money);
                        kmt.Parameters["@siparisKdvTutari"].Value = siparis.KdvTutar;
                        kmt.Parameters.Add("@siparisGenelToplami", SqlDbType.Money);
                        kmt.Parameters["@siparisGenelToplami"].Value = siparis.GenelToplam;
                        kmt.Parameters.Add("@kullanilanPara", SqlDbType.Money);
                        kmt.Parameters["@kullanilanPara"].Value = siparis.KullanilanPara;
                        kmt.Parameters.Add("@kargoId", SqlDbType.Int);
                        kmt.Parameters["@kargoId"].Value = siparis.KargoId;
                        kmt.Parameters.Add("@kargoToplam", SqlDbType.Money);
                        kmt.Parameters["@kargoToplam"].Value = siparis.KargoFiyat;
                        kmt.Parameters.Add("@taksitMiktari", SqlDbType.Int);
                        kmt.Parameters["@taksitMiktari"].Value = siparis.TaksitMiktari;
                        kmt.Parameters.Add("@aylikTaksitTutari", SqlDbType.Money);
                        kmt.Parameters["@aylikTaksitTutari"].Value = siparis.AylikTaksitTutari;
                        kmt.Parameters.Add("@TaksitliGenelToplami", SqlDbType.Money);
                        kmt.Parameters["@TaksitliGenelToplami"].Value = siparis.TaksitliGenelToplam;
                        kmt.Parameters.Add("@adresId", SqlDbType.Int);
                        kmt.Parameters["@adresId"].Value = siparis.AdresId;
                        kmt.Parameters.Add("@faturaId", SqlDbType.Int);
                        kmt.Parameters["@faturaId"].Value = siparis.FaturaId;
                        kmt.Parameters.Add("@siparisMesaj", SqlDbType.NVarChar);
                        kmt.Parameters["@siparisMesaj"].Value = siparis.Mesaj;
                        kmt.Parameters.Add("@durum", SqlDbType.Int);
                        kmt.Parameters["@durum"].Value = (int)siparis.SiparisDurum;
                        kmt.Parameters.Add("@indirim", SqlDbType.Money);
                        kmt.Parameters["@indirim"].Value = siparis.Indirim;

                        kmt.Parameters.Add("@deger_dondur", SqlDbType.Int);
                        kmt.Parameters["@deger_dondur"].Direction = ParameterDirection.Output;
                        kmt.ExecuteNonQuery();

                        siparisId = Convert.ToInt32(kmt.Parameters["@deger_dondur"].Value);
                    }
                }

                // Sipariş Kaydı Başarı ile oluştuysa sepet bilgilerini kaydı yapılacak
                if (siparisId != 0)
                {
                  SqlParameter[] sql = new SqlParameter[12];

                  SepetDB sepet = new SepetDB();

                  List<SepetSiparisKayit> sepetList = sepet.GetListForInsert(siparis.UyeId);

                  foreach (SepetSiparisKayit s in sepetList)
                    {
                        sql[0] = new SqlParameter("@siparisId", siparisId);
                        sql[1] = new SqlParameter("@sepetId", s.SepetId);
                        sql[2] = new SqlParameter("@urunId", s.UrunId);
                        sql[3] = new SqlParameter("@fiyat", s.Fiyat);
                        sql[4] = new SqlParameter("@Adet", s.Miktar);
                        sql[5] = new SqlParameter("@sagAdet", s.SagAdet);
                        sql[6] = new SqlParameter("@solAdet", s.SolAdet);
                        sql[7] = new SqlParameter("@sagBilgi", s.SagBilgi);
                        sql[8] = new SqlParameter("@solBilgi", s.SolBilgi);
                        sql[9] = new SqlParameter("@hediyeBilgi", s.HediyeBilgi);
                        sql[10] = new SqlParameter("@hediyeId", s.HediyeId);
                        sql[11] = new SqlParameter("@deger_dondur", SqlDbType.Int);
                        sql[11].Direction = ParameterDirection.Output;

                        SqlHelper.ExecuteNonQuery("siparis_DetayEkle", sql);

                        string urunId = sql[11].Value.ToString();
                        if (urunId != "0")
                        {
                            string str = string.Format("<a href='http://www.lensoptik.com.tr/Urun/{0}/Kiritik/UrunDetay.aspx'>{0}  Nolu Ürün kritik Stogun altına düştü Ürün Detayına Linketen Ulaşabilirsiniz.</a>", urunId);

                            MailManager.Admin.Send("Kırıtık Stok Bildirisi", str, ProcessType.Async);
                        }
                    }
                }
                else
                {
                    MailManager.Admin.Send(" Sipariş Kaydedilirken Hata Oluştu",
                                           "* Sipariş ödemesi yapıldı<br/>"+
                                           "* sipariş kaydı oluşmadı<br/> "+
                                           "* sipariş edilen ürünler sepetten silinmedi.<br/>"+
                                           " Üye Id: " + siparis.UyeId.ToString());
                }
            }
            catch (Exception ex)
            {
                LogManager.SqlDB.Write("Sipariş Kayıt Sırasında Hata Oluştu", siparis.UyeId.ToString() + " Nolu Üye Sipariş verilken Hata Oluştu." + ex);
                throw;
            }
            return siparisId;
        }

        public static List<Siparis> TarihListe(SiparisArama prm)
        {
            List<Siparis> sipariTablo = new List<Siparis>();

            try
            {
                SqlParameter[] parametre = new SqlParameter[5];
                parametre[0] = new SqlParameter("@siparisDurumu", SqlDbType.NVarChar);
                parametre[0].Value = prm.SiparisDurumu;
                parametre[1] = new SqlParameter("@baslangicTarih", SqlDbType.DateTime);
                parametre[1].Value = prm.BaslangicTarih;
                parametre[2] = new SqlParameter("@bitisTarih", SqlDbType.DateTime);
                parametre[2].Value = prm.BitisTarih;
                parametre[3] = new SqlParameter("@baslangic", SqlDbType.Int);
                parametre[3].Value = prm.Baslangic;
                parametre[4] = new SqlParameter("@bitis", SqlDbType.Int);
                parametre[4].Value = prm.Bitis;

                using (SqlDataReader dr = SqlHelper.ExecuteReader("siparis_TarihArama", parametre))
                {
                    while (dr.Read())
                    {
                        sipariTablo.Add(new Siparis
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("id")),
                            SiparisNo = dr.GetString(dr.GetOrdinal("siparisNo")),
                            AdiSoyadi = dr.GetString(dr.GetOrdinal("adiSoyadi")),
                            KullaniciId = dr.GetInt32(dr.GetOrdinal("kullaniciId")),
                            OdemeTipi = Fonksiyonlar.OdemeTuru(dr.GetInt32(dr.GetOrdinal("odemeTipi"))),
                            SiparisDurumu = Fonksiyonlar.SiparisDurum(dr.GetInt32(dr.GetOrdinal("siparisDurumu"))),
                            SiparisTarihi = dr.GetDateTime(dr.GetOrdinal("siparisTarihi")),
                            TaksitMiktari = dr.GetInt32(dr.GetOrdinal("taksitMiktari")),
                            TaksitliGenelToplami = dr.GetDecimal(dr.GetOrdinal("TaksitliGenelToplami"))
                        });
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

            return sipariTablo;
        }

        public static SiparisArama TarihSayfaNo(SiparisArama prm)
        {
            try
            {
                SqlParameter[] sprm = new SqlParameter[3];
                sprm[0] = new SqlParameter("@siparisDurumu", SqlDbType.NVarChar);
                sprm[0].Value = prm.SiparisDurumu;
                sprm[1] = new SqlParameter("@baslangicTarih", SqlDbType.DateTime);
                sprm[1].Value = prm.BaslangicTarih;
                sprm[2] = new SqlParameter("@bitisTarih", SqlDbType.DateTime);
                sprm[2].Value = prm.BitisTarih;

                using (SqlDataReader dr = SqlHelper.ExecuteReader("siparis_TarihAramaSayfaNo", sprm))
                {
                    while (dr.Read())
                    {
                        if (!dr.IsDBNull(dr.GetOrdinal("toplam")))
                        {
                            prm.SayfaToplam = Convert.ToInt32(dr[0]); 
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("genelToplam")))
                        {
                            prm.GenelToplami = Convert.ToDecimal(dr[1]); 
                        }
                        
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return prm;
        }

        public static List<Siparis> Listele(string value, string bolge, decimal fiyat = 0)
         {
             SqlParameter[] prm = new SqlParameter[3];
             prm[0] = new SqlParameter("@bolge", SqlDbType.NVarChar);
             prm[0].Value = bolge;
             prm[1] = new SqlParameter("@value", SqlDbType.NVarChar);
             prm[1].Value = value;
             prm[2] = new SqlParameter("@fiyat", SqlDbType.Money );
             prm[2].Value = fiyat;

             List<Siparis> info = new List<Siparis>();

             using (SqlDataReader dr = SqlHelper.ExecuteReader("siparis_KayitAra", prm))
             {
                 while (dr.Read())
                 {
                     info.Add(new Siparis
                     {
                         Id = dr.GetInt32(dr.GetOrdinal("id")),
                         SiparisNo = dr.GetString(dr.GetOrdinal("siparisNo")),
                         KullaniciId = dr.GetInt32(dr.GetOrdinal("kullaniciId")),
                         AdiSoyadi = dr.GetString(dr.GetOrdinal("adiSoyadi")),
                         OdemeTipi = Fonksiyonlar.OdemeTuru(dr.GetInt32(dr.GetOrdinal("odemeTipi"))),
                         SiparisDurumu = Fonksiyonlar.SiparisDurum(dr.GetInt32(dr.GetOrdinal("siparisDurumu"))),
                         SiparisTarihi = dr.GetDateTime(dr.GetOrdinal("siparisTarihi")),
                         TaksitMiktari = dr.GetInt32(dr.GetOrdinal("taksitMiktari")),
                         TaksitliGenelToplami = dr.GetDecimal(dr.GetOrdinal("TaksitliGenelToplami"))
                         
                     });
                 }
             }
             return info;
         }

        public static List<SiparisDetay> DetayUrunListe(int siparisId)
        {
            try
            {
                SqlParameter parametre = new SqlParameter("@siparisId", siparisId);
                List<SiparisDetay> siparisler = new List<SiparisDetay>();
                UrunHediyeTek hediyeTek = new UrunHediyeTek();

                using (SqlDataReader dr = SqlHelper.ExecuteReader("siparis_DetayGetir", parametre))
                {
                    while (dr.Read())
                    {
                        UrunHediye _hediye = new UrunHediye();

                        if (!dr.IsDBNull(dr.GetOrdinal("hediyeId")))
                        {
                            _hediye.Id = dr.GetInt32(dr.GetOrdinal("hediyeId"));
                        }

                        if (_hediye.Id > 0)
                        {
                            _hediye.Resim = dr.GetString(dr.GetOrdinal("resim"));
                            _hediye.UrunAdi = dr.GetString(dr.GetOrdinal("hediyeAdi"));
                            _hediye.Secenek = dr.GetString(dr.GetOrdinal("hediyeBilgi"));
                        }

                        SiparisDetay siparis = new SiparisDetay
                        {
                            urunId = dr.GetInt32(dr.GetOrdinal("urunId")),
                            resimAdi = dr.GetString(dr.GetOrdinal("resimAdi")),
                            urunAdi = dr.GetString(dr.GetOrdinal("urunAdi")),
                            urunKodu = dr.GetString(dr.GetOrdinal("urunKodu")),
                            urunKDV = dr.GetInt32(dr.GetOrdinal("urunKDV")),
                            adet = dr.GetInt32(dr.GetOrdinal("adet")),
                            fiyat = dr.GetDecimal(dr.GetOrdinal("fiyat")),
                            sagAdet = dr.GetInt32(dr.GetOrdinal("sagAdet")),
                            solAdet = dr.GetInt32(dr.GetOrdinal("solAdet")),
                            sagBilgi = SepetOperasyon.SagBilgiHtml(dr.GetString(dr.GetOrdinal("sagBilgi"))),
                            solBilgi = SepetOperasyon.SolBilgiHtml(dr.GetString(dr.GetOrdinal("solBilgi"))),
                            stokCins = dr.GetString(dr.GetOrdinal("stokCins")),
                            //kampanya = dr.GetString(dr.GetOrdinal("kampanya")),
                            KdvDahilFiyat = SiparisOperasyon.KDVDahilFiyat(dr.GetDecimal(dr.GetOrdinal("fiyat")),
                                                        dr.GetInt32(dr.GetOrdinal("urunKDV"))),

                            Birim = SiparisOperasyon.BirimToplam(dr.GetDecimal(dr.GetOrdinal("fiyat")),
                                                        dr.GetInt32(dr.GetOrdinal("adet"))),
                            HediyeHTML = HediyeHtml(_hediye)
                        };

                        if (hediyeTek.UrunAdi == null)
                        {
                            hediyeTek = UrunDB.HediyeUrun(siparis.urunId);
                            siparis.HediyeUrunTekHTML = HediyeUrunTekHtml(hediyeTek);
                        }

                         siparisler.Add(siparis);
                    }
                }

                return siparisler;
            }
            catch (Exception)
            {
                throw;
            }
        }
      
        public static SiparisResponse Detay(int siparisId, int uyeId)
        {
            SiparisResponse siparis = new SiparisResponse();

            SqlParameter[] parametre = new SqlParameter[] 
            { 
                  new SqlParameter("@siparisId", siparisId),
                  new SqlParameter("@uyeId", uyeId)
            };

            using (SqlDataReader dr = SqlHelper.ExecuteReader("siparis_KayitGetir", parametre))
            {
                while (dr.Read())
                {
                    siparis.SiparisNo = dr.GetString(dr.GetOrdinal("siparisNo"));
                    siparis.Tarih = DateFormat.TarihGun(dr.GetDateTime(dr.GetOrdinal("siparisTarihi")));
                    siparis.OdemeTipi = Fonksiyonlar.OdemeTuru(dr.GetInt32(dr.GetOrdinal("odemeTipi")));
                    siparis.FiyatToplam = string.Format("{0:C}", dr.GetDecimal(dr.GetOrdinal("TaksitliGenelToplami")));
                    siparis.KDVToplam = string.Format("{0:C}", dr.GetDecimal(dr.GetOrdinal("siparisKdvTutari")));
                    siparis.KargoFiyat = string.Format("{0:C}", dr.GetDecimal(dr.GetOrdinal("kargoToplam")));
                    siparis.BirimFiyat = string.Format("{0:C}", dr.GetDecimal(dr.GetOrdinal("siparisBirimToplami")));
                    siparis.Indirim =  dr.GetDecimal(dr.GetOrdinal("indirim"));
                    siparis.HavaleVeKapi = string.Format("{0:C}", dr.GetDecimal(dr.GetOrdinal("aylikTaksitTutari")));
                    siparis.Mail= dr.GetString(dr.GetOrdinal("ePosta"));
                    siparis.UyeAdi = dr.GetString(dr.GetOrdinal("adiSoyadi")); 
                    siparis.BankaAdi = dr.GetString(dr.GetOrdinal("bankaAdi"));
                    siparis.TeslimAlan = dr.GetString(dr.GetOrdinal("teslimAlan"));
                    siparis.Adres = dr.GetString(dr.GetOrdinal("adres"));
                }
            }

            return siparis;
        }

        public static SiparisResponse DetayAdmin(int siparisId)
        {
            SiparisResponse siparis = new SiparisResponse();

            SqlParameter prm = new SqlParameter("@siparisId", siparisId);

            using (SqlDataReader dr = SqlHelper.ExecuteReader("siparis_KayitGetirAdmin", prm))
            {
                while (dr.Read())
                {
                    KullaniciFatura fatura = new KullaniciFatura();
                    KullaniciAdres adres = new KullaniciAdres();

                    siparis.UyeAdi = dr.GetString(dr.GetOrdinal("adiSoyadi"));
                    siparis.Mail = dr.GetString(dr.GetOrdinal("ePosta"));
                    siparis.SiparisNo = dr.GetString(dr.GetOrdinal("siparisNo"));
                    siparis.OdemeTipi = Fonksiyonlar.OdemeTuru(dr.GetInt32(dr.GetOrdinal("odemeTipi")));
                    siparis.Durum = dr.GetInt32(dr.GetOrdinal("siparisDurumu")).ToString();
                    siparis.KullanilanPara = dr.GetDecimal(dr.GetOrdinal("kullanilanPara"));
                    siparis.FiyatToplam = string.Format("{0:C}", dr.GetDecimal(dr.GetOrdinal("TaksitliGenelToplami")));
                    siparis.Tarih = DateFormat.TarihSaatSiparis(dr.GetDateTime(dr.GetOrdinal("siparisTarihi")));
                    siparis.Mesaj = dr.GetString(dr.GetOrdinal("siparisMesaj"));
                   
                    siparis.Taksit = dr.GetInt32(dr.GetOrdinal("taksitMiktari"));
                    siparis.HavaleVeKapi = string.Format("{0:C}", dr.GetDecimal(dr.GetOrdinal("aylikTaksitTutari")));
                    siparis.BankaAdi = dr.GetString(dr.GetOrdinal("bankaAdi"));
                    siparis.BirimFiyat = string.Format("{0:C}", dr.GetDecimal(dr.GetOrdinal("siparisBirimToplami")));
                    siparis.KDVToplam = string.Format("{0:C}", dr.GetDecimal(dr.GetOrdinal("siparisKdvTutari")));
                    siparis.KargoFiyat = string.Format("{0:C}", dr.GetDecimal(dr.GetOrdinal("kargoToplam")));
                    siparis.Indirim = dr.GetDecimal(dr.GetOrdinal("indirim"));

                    adres.TeslimAlan = dr.GetString(dr.GetOrdinal("teslimAlan"));
                    adres.Adres = dr.GetString(dr.GetOrdinal("adres"));
                    adres.Telefon = dr.GetString(dr.GetOrdinal("telefon"));
                    adres.Sehir = dr.GetString(dr.GetOrdinal("sehir"));

                    fatura.FaturaCinsi = dr.GetBoolean(dr.GetOrdinal("faturaCinsi"));
                    fatura.FaturaAdresi = dr.GetString(dr.GetOrdinal("faturaAdresi"));


                    if (fatura.FaturaCinsi)
                    {
                        fatura.AdSoyad = dr.GetString(dr.GetOrdinal("adSoyad"));
                        fatura.TCNo = dr.GetString(dr.GetOrdinal("tcNo"));
                    }
                    else
                    {
                        fatura.Unvan = dr.GetString(dr.GetOrdinal("unvan"));
                        fatura.VergiNo = dr.GetString(dr.GetOrdinal("vergiNo"));
                        fatura.VergiDairesi = dr.GetString(dr.GetOrdinal("vergiDairesi"));
                    }


                    siparis.Fatura = fatura;
                    siparis.Adress = adres;
                }
            }

            return siparis;
        }

        private static string HediyeHtml(UrunHediye hediye)
        {
            StringBuilder sb = new StringBuilder();

            if (hediye.Id > 0)
            {
                sb.Append("<div class='hediyeSol'>");
                sb.Append("<a href='/Admin/hediyeUrun.aspx'>");
                sb.Append("<img class='hediyeImg' src='/Products/Small/" + hediye.Resim + "'  />");
                sb.Append("</a>");
                sb.Append("</div>");

                sb.Append("<div class='hediyeSag'>");
                sb.Append("<span class='hediyeTitle' >Hediye Ürün</span>");
                sb.Append(hediye.UrunAdi);
                sb.Append("<br/>");
                sb.Append(hediye.Secenek);
                sb.Append("</div");
                sb.Append("<div class='clear'></div>");
            }

            return sb.ToString();
        }

        private static string HediyeUrunTekHtml(UrunHediyeTek hediyeTek)
        {
            StringBuilder sb = new StringBuilder();

            if (hediyeTek.UrunAdi != null)
            {
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
            }

            return sb.ToString();

        }

    }
}
