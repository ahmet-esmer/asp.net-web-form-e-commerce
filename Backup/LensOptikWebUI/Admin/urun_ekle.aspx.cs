using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using BusinessLayer;
using BusinessLayer.BasePage;
using DataAccessLayer;
using ImageLibrary;
using MailLibrary;
using ModelLayer;
using SecureCookie;


public partial class AdminUrunEkle : BasePageAdmin
{
    string ozellikler = string.Empty;
    string secenekler = string.Empty;
    string fjkVarsayılan = "<table  width='100%'; border='0' cellpadding='0' cellspacing='0' ><tr class='fjkTabloTr' ><td class='fjkTabloOzelik' colspan='2' > &nbsp; </td> </tr><tr class='fjkTabloTr' ><td class='fjkTabloOzelik'> &nbsp; </td><td class='fjkTabloAciklama' >&nbsp;</td> </tr><tr class='fjkTabloTr' ><td class='fjkTabloOzelik'> &nbsp; </td><td class='fjkTabloAciklama' >&nbsp;</td> </tr><tr class='fjkTabloTr' ><td class='fjkTabloOzelik'> &nbsp; </td><td class='fjkTabloAciklama' >&nbsp;</td> </tr><tr class='fjkTabloTr' ><td class='fjkTabloOzelik'> &nbsp; </td><td class='fjkTabloAciklama' >&nbsp;</td> </tr><tr class='fjkTabloTr' ><td class='fjkTabloOzelik'> &nbsp; </td><td class='fjkTabloAciklama' >&nbsp;</td> </tr><tr class='fjkTabloTr' ><td class='fjkTabloOzelik'> &nbsp; </td><td class='fjkTabloAciklama' >&nbsp;</td> </tr><tr class='fjkTabloTr' ><td class='fjkTabloOzelik'> &nbsp; </td><td class='fjkTabloAciklama' >&nbsp;</td> </tr><tr class='fjkTabloTr' ><td class='fjkTabloOzelik'> &nbsp; </td><td class='fjkTabloAciklama' >&nbsp;</td> </tr> </table>";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            kategoriListele();
            markaListele();

            if (Request.Params["islem"] == "duzenle")
            {
                btnUrunSil.Visible = true;
                txtResimEk.Visible = false;
                pnlResim_1.Visible = false;
                btnUrunEkle.Visible = false;
                pnlHediyeKampanya.Visible = true;

                btnUrunTavsiye.Visible = true;
                btnUrunGuncelle.Visible = true;
                pnlUrunResim.Visible = true;
                pnlTavsiye.Visible = true;
                pnlKampanya.Visible = true;
                UrunBilgiGetir();
                urunTavsiyeListele();
                urunKampanyaListele();

                HediyeUrunKampanyaList();
                HediyeKampanyaListe();
                UrunIndirimleriListe();
                KampanyaBilgiListe();
                
                if (Request.Params["kayit"] == "ok")
                {
                    mesajGosterOk("Kaydınız Başarıyla Gerçekleşti.");
                }
            }
            else 
            {
                fck_urunAciklama.Value = fjkVarsayılan;
            }

            secenek_ismi.Attributes.Add("ondblclick","moveOver_deger();");
            secenek_ismi2.Attributes.Add("ondblclick", "removeMe_deger();");

        }
    }

    #region DropDouwn Kategori Listeleme
    private void kategoriListele()
    {

        ddlkategoriler.DataSource = KategoriDB.dropDownListele();
        ddlkategoriler.DataTextField = "kategoriadi";
        ddlkategoriler.DataValueField = "id";
        ddlkategoriler.DataBind();

        ListItem item = new ListItem();
        item.Text = "-- Kategori Seçiniz --";
        item.Value = "0";
        item.Selected = true;
        ddlkategoriler.Items.Add(item);
    }
    #endregion

    #region DropDouwn Marka Listeleme
    private void markaListele()
    {
        try
        {
            ddlMarkalar.DataSource = MarkaDB.dropDownListele();
            ddlMarkalar.DataTextField = "marka_adi";
            ddlMarkalar.DataValueField = "id";
            ddlMarkalar.DataBind();

            ListItem item = new ListItem();
            item.Text = "-- Marka Seçiniz --";
            item.Value = "0";
            item.Selected = true;
            ddlMarkalar.Items.Add(item);
        }
        catch (Exception ex)
        {
            mesajGosterSis("Marka Listeleme Hatası: ", ex);
        }
    }
    #endregion

    #region Urun Ekleme ve Resim Upload Kismi 
    protected void btnUrunEkle_Click(object sender, EventArgs e)
    {
        string resimAdi = string.Empty;
        string Description = string.Empty;
        string Keyword = string.Empty;
        string Title = string.Empty;

      

        try
        {
            if (txtResimEk.PostedFile.FileName != "" || txtResimEk.PostedFile.ContentLength > 0)
            {
                resimAdi = Images.GetImageName(txtResimEk.PostedFile.FileName);
                txtResimEk.PostedFile.SaveAs(Images.GetPath(resimAdi));

                Images.SmallImage.Save(resimAdi, 220);
                Images.LittleImage.Save(resimAdi, 150);
                Images.BigImage.Save(resimAdi, 750, true);
            }

            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                Description = txt_urunAdi.Text;
                Keyword = txt_urunAdi.Text;
                Title = txt_urunAdi.Text;
            }
            else
            {
                Description = txtDescription.Text;
                Keyword = txtKeyword.Text;
                Title =txtTitle.Text;     
            }

            urunSecenekleri();// listOzellik  gerialınıyor

            Urun urun = new Urun
            {
                title = Title,
                description = Description,
                keywords = Keyword,
                kategoriId = Convert.ToInt32(ddlkategoriler.SelectedItem.Value),
                markaId =  Convert.ToInt32(ddlMarkalar.SelectedItem.Value),
                urunKodu = txt_urunKodu.Text,
                urunAdi = txt_urunAdi.Text,
                kisaAciklama = txtAciklama.Text,
                urunFiyat = Convert.ToDecimal(txt_urunFiyat.Text),
                uIndirimFiyat = Convert.ToDecimal(txt_urunIndirimFiyat.Text),
                doviz = ddlDoviz.SelectedItem.Value.ToString(),
                kdv = Convert.ToInt16(txtKdv.Text),
                havaleIndirim = Convert.ToInt16(txtHavale.Text),
                desiMiktari = Convert.ToInt16(txtDesi.Text),
                urunStok =  Convert.ToInt16(txtStok.Text),
                kiritikStok = Convert.ToInt16( txtKiritikStok.Text),
                stokCins = ddlStokCinsi.SelectedItem.Value.ToString(),
                urunOzellik = fck_urunAciklama.Value,
                resimAdi = resimAdi,
                listOzellik = ozellikler,
                durum = Convert.ToBoolean(ckbDurum.Checked)

            };

            int degerdondur = UrunDB.kaydet(urun);

            if (degerdondur > 1)
            {

                if (!string.IsNullOrWhiteSpace(hdfSecenekler.Value))
                {
                    UrunDB.secenekKaydet(degerdondur, hdfSecenekler.Value.ToString()); 
                }
               
                ddlkategoriler.ClearSelection();
                ddlMarkalar.SelectedValue = "0";
                txt_urunKodu.Text = "";
                txt_urunAdi.Text = "";
                txt_urunFiyat.Text = "";
                fck_urunAciklama.Value = "";

                mesajGosterOk("Ürün Kaydı Başarı ile yapıldı..");
            }
        }
        catch (Exception ex)
        {
                Images.BigImage.Delete(resimAdi);
                Images.LittleImage.Delete(resimAdi);
                Images.SmallImage.Delete(resimAdi);

                mesajGosterSis("Hata Oluştu :" , ex);
        }
    }
#endregion

    #region Ürün Güncelleme İçin Bilgi Getirme
    private void UrunBilgiGetir()
    {
        try
        {
            urunResimleriGetir();
            Urun urun = UrunDB.getir(Convert.ToInt32(Request.Params["id"]), "admin");

            txt_urunKodu.Text = urun.urunKodu;
            txt_urunAdi.Text = urun.urunAdi;
            txtAciklama.Text = urun.kisaAciklama;
            txt_urunFiyat.Text = urun.urunFiyat.ToString("N");
            txt_urunIndirimFiyat.Text = urun.uIndirimFiyat.ToString("N");
            txtKdv.Text = urun.kdv.ToString();
            txtDesi.Text = urun.desiMiktari.ToString();
            txtStok.Text = urun.urunStok.ToString();
            txtHavale.Text = urun.havaleIndirim.ToString();
            txtKeyword.Text = urun.keywords;
            txtTitle.Text = urun.title;
            txtDescription.Text = urun.description;
            txtHit.Text = urun.hit.ToString();
            txtKiritikStok.Text = urun.kiritikStok.ToString();
            fck_urunAciklama.Value = urun.urunOzellik;
            ckbDurum.Checked = urun.durum;

            ddlkategoriler.ClearSelection();
            ddlkategoriler.SelectedValue = urun.kategoriId.ToString();

            ddlDoviz.ClearSelection();
            ddlDoviz.SelectedValue = urun.doviz;

            ddlStokCinsi.ClearSelection();
            ddlStokCinsi.SelectedValue = urun.stokCins;

            ddlMarkalar.ClearSelection();
            ddlMarkalar.SelectedValue = urun.markaId.ToString();


            List<string> ozelikler = UrunDB.urunOzellikleri(urun.id);
            foreach (object ozellik in ozelikler)
	        {
                foreach (ListItem item in ckbOzellikler.Items)
                {
                    if (item.Value == ozellik.ToString())
                    {
                        item.Selected = true;
                    }
                }
	        }

            string[] secenekler = UrunDB.urunSecenekGetir(Convert.ToInt32(Request.Params["id"]));

            if (secenekler != null)
            {
                secenek_ismi2.Items.Clear();
                for (int i = secenekler.Length - 1; i >= 0 ; i--)
                {
                     secenek_ismi2.Items.Insert(0, secenekler[i]);
                }
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("ÜRÜN LİSTELEME GÜNCELLEME HATASI:" , ex);
        }
    }
    #endregion

    #region Ürün Güncelleme İşlemi
    protected void btnUrunGuncelle_Click(object sender, EventArgs e)
    {
        try
        {
            urunSecenekleri();// listOzellik  gerialınıyor
         
            Urun urun = new Urun
            {
                id = Convert.ToInt32(Request.Params["id"]),
                title = txtTitle.Text.ToString(),
                description = txtDescription.Text,
                keywords = txtKeyword.Text,
                kategoriId = Convert.ToInt32(ddlkategoriler.SelectedItem.Value),
                markaId = Convert.ToInt32(ddlMarkalar.SelectedItem.Value),
                urunKodu = txt_urunKodu.Text,
                urunAdi = txt_urunAdi.Text,
                kisaAciklama = txtAciklama.Text,
                urunFiyat = Convert.ToDecimal(txt_urunFiyat.Text),
                uIndirimFiyat = Convert.ToDecimal(txt_urunIndirimFiyat.Text),
                doviz = ddlDoviz.SelectedItem.Value.ToString(),
                kdv = Convert.ToInt16(txtKdv.Text),
                havaleIndirim = Convert.ToInt16(txtHavale.Text),
                desiMiktari = Convert.ToInt16(txtDesi.Text),
                urunStok = Convert.ToInt16(txtStok.Text),
                kiritikStok = Convert.ToInt16(txtKiritikStok.Text),
                stokCins = ddlStokCinsi.SelectedItem.Value.ToString(),
                urunOzellik = fck_urunAciklama.Value,
                listOzellik = ozellikler,
                hit = Convert.ToInt32(txtHit.Text),
                durum = Convert.ToBoolean(ckbDurum.Checked)
            };


           int degerdondur = UrunDB.kaydet(urun);

           // Seçenek Kaydetme

           UrunDB.secenekKaydet(Convert.ToInt32(Request.Params["id"]), hdfSecenekler.Value.ToString());

           if (urun.uIndirimFiyat != 0)
           {
               urunIndirimBirdir(urun.id, urun.uIndirimFiyat, urun.urunAdi);
           }

           if (degerdondur == 0)
           {
               mesajGosterOk("Kaydınız başarıyla güncellendi.");
               UrunBilgiGetir();
           }
        }
        catch (Exception ex)
        {
            mesajGosterSis("Hata Oluştu :", ex);
        }
    }
    #endregion

    // Ürün Resimleri

    #region Ürün Resimleri Getirme
    private void urunResimleriGetir()
    {
        dlResimleri.DataSource = ResimDB.ResimListele(Convert.ToInt32(Request.Params["id"]), "urun");
        dlResimleri.DataBind();
    }
    #endregion

    #region  Ürün Resim Ekleme İşlemi
    protected void btnUrunResimEkle_Click(object sender, EventArgs e)
    {
        if (txtresim.PostedFile.FileName != "" || txtresim.PostedFile.ContentLength > 0)
        {
            string resimAdi = Images.GetImageName(txtresim.PostedFile.FileName);
            txtresim.PostedFile.SaveAs(Images.GetPath(resimAdi));

            Images.SmallImage.Save(resimAdi, 250);
            Images.LittleImage.Save(resimAdi, 180);
            Images.BigImage.Save(resimAdi, 1260, true);

            ResimDB.Kaydet(Convert.ToInt32(Request.Params["id"]), resimAdi, "urun");
         
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "tabResim", "tabResim()", true);
            urunResimleriGetir();
        }   
    }
    #endregion

    #region Resim Sıra Kaydetme
    protected void Urun_Resim_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txtSira = (TextBox)sender;
            ResimDB.Duzenle(Convert.ToInt32(txtSira.ToolTip), txtSira.Text, "sira");
        }
        catch (Exception hata)
        {
            lblResimHata.Visible = true;
            lblResimHata.Text = " Resim Sıra Güncelleme Hatası: " + hata.ToString();
        }
       
        urunResimleriGetir();
    }
    #endregion

    #region Resim Başlık Düzenleme
    protected void GunResimBaslik_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txtBaslik = (TextBox)sender;
            ResimDB.Duzenle(Convert.ToInt32(txtBaslik.ToolTip), txtBaslik.Text.ToString(), "baslik");
        }
        catch (Exception ex)
        {
            lblResimHata.Visible = true;
            lblResimHata.Text = " Resim Başlık Güncelleme Hatası: " + ex.ToString();
            mesajGosterSis("Resim Başlık Güncelleme Hatası: ", ex);
        }

        urunResimleriGetir();
    }
    #endregion

    #region Resim Silme
    protected void dlResimleri_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "btnSil")
        {
            string[] kayitDizi = e.CommandArgument.ToString().Split(',');
            string resimAdiSil = kayitDizi[0];

            try
            {
                Images.BigImage.Delete(resimAdiSil);
                Images.LittleImage.Delete(resimAdiSil);
                Images.SmallImage.Delete(resimAdiSil);

                ResimDB.ResimSil(Convert.ToInt32(kayitDizi[1]));
                urunResimleriGetir();
            }
            catch (Exception ex)
            {
              mesajGosterSis("HATA OLUŞTU : " , ex);
            }
        }
    }
    #endregion


    // ÜRÜN TAVSİYE ALANI

    #region  Ürün Tavsiye Ekleme İşlemi
    protected void btnUrunTavsiye_Click(object sender, EventArgs e)
    {
        if (Request.Params["id"] == txtTUrunId.Text.Trim())
        {

            lblMesaj.Text = " ! İki Üründe Aynı";
            lblMesaj.CssClass = "input-notification information";
        }
        else
        {
            try
            {

                SqlParameter[] parameter = new SqlParameter[3];
                parameter[0] = new SqlParameter("@tavAnaUrunId", SqlDbType.Int);
                parameter[0].Value = Convert.ToInt32(Request.Params["id"].ToString());
                parameter[1] = new SqlParameter("@tavUrunId", SqlDbType.Int);
                parameter[1].Value = Convert.ToInt32(txtTUrunId.Text);
                parameter[2] = new SqlParameter("@deger_dondur", SqlDbType.Int);
                parameter[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery("urunTavsiye_Ekle", parameter);


                int degerdondur = Convert.ToInt32(parameter[2].Value);

                if (degerdondur == 0)
                {
                    urunTavsiyeListele();
                    lblMesaj.Text = "Kayıt Yapıldı.";
                    lblMesaj.CssClass = "input-notification success";
                }
                else  if (degerdondur == 2)
                {
                    lblMesaj.Text = " ! Bu ID ye sahip bir ürün bulunamadı";
                    lblMesaj.CssClass = "input-notification information";
                }
                else
                {
                    lblMesaj.Text = " ! Bu Ürün Kaydı Daha Önce Yapılmıştır!";
                    lblMesaj.CssClass = "input-notification information";
                }

            }
            catch (Exception hata)
            {
                lblMesaj.CssClass = "input-notification information";
                lblMesaj.Text = " Hata:" + hata.ToString();
            }

        }
    }
    #endregion

    #region Ürün Tavsiye Listeleme
    private void urunTavsiyeListele()
    {
        try
        {
            SqlParameter parametre = new SqlParameter("@id", Convert.ToInt32(Request.Params["id"])); 
  
            using(SqlDataReader dr = SqlHelper.ExecuteReader("urunTavsiye_Listele", parametre))
	        {
                List<UrunTavsiye> urunTavsiyeTablo = new List<UrunTavsiye>();
                while (dr.Read())
                {
                    UrunTavsiye info = new UrunTavsiye(
                        dr.GetInt32(dr.GetOrdinal("id")),
                        dr.GetInt32(dr.GetOrdinal("tavAnaUrunId")),
                        dr.GetInt32(dr.GetOrdinal("tavUrunId")),
                        dr.GetInt32(dr.GetOrdinal("tavSatisMiktari")),
                        dr.GetDecimal(dr.GetOrdinal("urunFiyat")),
                        dr.GetDecimal(dr.GetOrdinal("uIndirimFiyat")),
                        dr.GetString(dr.GetOrdinal("urunAdi")),
                        dr.GetString(dr.GetOrdinal("doviz")));
                    urunTavsiyeTablo.Add(info);

                }

                GridView1.DataSource = urunTavsiyeTablo;
                GridView1.DataBind();
	        }
        }
        catch (Exception ex)
        {
            mesajGosterSis("Ürün Tavsiye Listeleme Hatası", ex);
        }
    }
    #endregion

    #region Ürün Tavsiye Silme İşlemi
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "urunTavsiyeSil")
        {
            try
            {
                SqlParameter parametre = new SqlParameter("@id", Convert.ToInt32(e.CommandArgument));

                SqlHelper.ExecuteNonQuery("urunTavsiye_Sil", parametre);

                urunTavsiyeListele();
            }
            catch (Exception hata)
            {
                lblMesaj.Visible = true;
                lblMesaj.Text = "Ürün Tavsiye Silme İşlemi" + hata.ToString();
            }
        }
    }
      #endregion

    #region Ürün Fiyatı Düşünce E-Posta İle Bildirme İşlemi
    private void urunIndirimBirdir(int Iid , decimal Ifiyat, string urunAdi)
    {
        try
        {
            SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@urunId", Iid),
                    new SqlParameter("@fiyat", Ifiyat)
                };

            using (SqlDataReader dr = SqlHelper.ExecuteReader("kullanici_IndirimGetir", parametre))
            {
                int count = 0;
                while (dr.Read())
                {
                    string adiSoyad = dr.GetString(dr.GetOrdinal("adiSoyadi"));
                    decimal fiyat = dr.GetDecimal(dr.GetOrdinal("fiyat"));
                    int urunId = dr.GetInt32(dr.GetOrdinal("id"));
                    string ePosta = dr.GetString(dr.GetOrdinal("ePosta"));

                    string adres = "http://www.lensoptik.com.tr/Urun/"+ Iid.ToString() +"/Indirim.aspx";

                    StringBuilder sb = new StringBuilder();

                    sb.Append("<table cellpadding='0' cellspacing='0' style='width:100%;border:1px solid #d9dada;background-color:#fafafa;margin:20px 0px; font-family:Verdana;' >");

                    sb.Append("<tr>");
                    sb.Append("<td style='vertical-align:middle;font-size:12px;color:#292929;height:170px;border-bottom:1px solid #fff;padding:15px;' >");

                    sb.Append("<h1> lensoptik.com</h1> <br/>");
                    sb.Append("<b>Sayın  : </b>" + adiSoyad + "<br/> <br/>");
                    sb.Append("Fiyat İndirim takip talebinde bulunduğunuz <b>\"" + urunAdi + "\"</b> adlı ürünün fiyatı ");
                    sb.Append("<b>" + fiyat.ToString("C") + "</b> den <b>" + Ifiyat.ToString("C") + "</b>ye düşmüştür.<br/>");
                    sb.Append(" Aşağdaki linkten ürün bilgilerine ulaşabilirsiniz.<br/> <br/>");
                    sb.Append("<a href='" + adres + "' target='_blank' style='font-size:12px;color:#069;'  >" + adres + "</a>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td style='vertical-align:middle;font-size:12px;color:#069;height:60px;padding-left:15px;text-decoration:underline;' >");
                    sb.Append("<a style='font-size:12px;color:#069;'  href='http://www.lensoptik.com.tr' target='_blank'> www.lensoptik.com.tr </a>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("</table>");

                    MailManager.User.Send(ePosta, "Fiyat İndirimi", sb.ToString());

                    gonderilenPostaSil(urunId);
                    count = count + 1;
                }

                if (count != 0)
                {
                    mesajGosterInfo("Ürünün İndirim Fiyatı <b>" + count.ToString() + "</b> Üyeye E-Posta Olarak Gönderildi.");
                }
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("Kulanıcı Ürün İndirim E-Posta Gönderimi", ex);
        }
    }
    #endregion

    #region Fiyatı Düşünce Haberver Tablosunda mail gönderme sonrası silme işlemi
    private void gonderilenPostaSil(int Iid)
    {
        try
        {
            SqlParameter parametre = new SqlParameter("@id", Iid);
            SqlHelper.ExecuteNonQuery("kullanici_IndirimSil", parametre); 
        }
        catch (Exception ex)
        {
            mesajGosterSis("Kulanıcı İndirim Silme İşlemi", ex);
        }
    }
    #endregion

    #region Ürün Kayıt Silme İşlemi
    protected void btnUrunSil_Click(object sender, EventArgs e)
    {
        try
        {
            int id = Convert.ToInt32(Request.Params["id"]);
        
            foreach (var image in ResimDB.ResimListele(id, "urun"))
            {
                Images.BigImage.Delete(image.resimAdi);
                Images.LittleImage.Delete(image.resimAdi);
                Images.SmallImage.Delete(image.resimAdi);
            }

            UrunDB.Delete(id);

            Response.Redirect("urunler.aspx?islem=genelUrun", false);
        }
        catch (Exception ex)
        {
            mesajGosterSis("<b>Ürün Ve Resimlerini Silme Hatası:</b> ", ex);
        }
    }
    #endregion

    // Ürün Seçenek İşlemi
    private void urunSecenekleri()
    {
        foreach (ListItem item in ckbOzellikler.Items)
        {
            if (item.Selected)
            {
                ozellikler += item.Value + ",";
            }
        }

        if (ozellikler.Length > 0)
        {
            ozellikler = ozellikler.Remove(ozellikler.Length - 1);
        }
    }

    protected void ddlSecenekSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            secenek_ismi.Items.Clear();
            string secim =  ddlSecenekler.SelectedValue.ToString();
            using (XmlTextReader reader = new XmlTextReader(Server.MapPath("~/App_Data/"+ secim +".xml")))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "secenek")
                    {
                        secenek_ismi.Items.Insert(0, secim +":"+ reader.ReadString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("Secenek Hata:", ex);
        }
    }

    // Hediye Ürün
    #region  Ürün Kampanya Ekleme İşlemi
    protected void btnKampanya_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.Params["id"].Trim() == txtKampanya.Text.Trim())
            {
                lblMesaj.Text = " ! İki Üründe Aynı";
                lblMesaj.CssClass = "input-notification error";
            }
            else
            {
                int degerdondur = UrunKampanyaDB.Kaydet(Request.Params["id"], txtKampanya.Text);

                if (degerdondur == 0)
                {
                    lblMesaj.Text = "Kayıt Yapıldı.";
                    lblMesaj.CssClass = "input-notification success";

                    urunKampanyaListele();
                }
                else if (degerdondur == 2)
                {
                    lblMesaj.Text = " ! Bu ID ye sahip bir ürün bulunamadı";
                    lblMesaj.CssClass = "input-notification information";
                }
                else
                {
                    lblMesaj.Text = " ! Bu Ürün Kaydı Daha Önce Yapılmıştır!";
                    lblMesaj.CssClass = "input-notification information";
                }
            }
        }
        catch (Exception hata)
        {
            lblMesaj.Text = " Hata:" + hata.ToString();
            lblMesaj.CssClass = "input-notification error";
        }
    }
    #endregion

    #region Ürün Kampanya Listeleme
    protected void urunKampanyaListele()
    {
        try
        {
           gvwKampanya.DataSource = UrunKampanyaDB.Liste(Convert.ToInt32(Request.Params["id"]));
           gvwKampanya.DataBind();
        }
        catch (Exception ex)
        {
            mesajGosterSis("Ürün Kampanya Listeleme Hatası", ex);
        }

    }
    #endregion

    #region Ürün Kampanya Silme İşlemi
    protected void gvwKampanya_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "urunKampanyaSil")
        {
            try
            {
                SqlParameter parametre = new SqlParameter("@id", Convert.ToInt32(e.CommandArgument));

                SqlHelper.ExecuteNonQuery("urunKampanya_Sil", parametre);

                urunKampanyaListele();
            }
            catch (Exception hata)
            {
                lblMesaj.Visible = true;
                lblMesaj.Text = "Ürün Kampanya Silme İşlemi" + hata.ToString();
            }
        }
    }
    #endregion


    // Hediye Ürün Kampanya
    #region Hediye Ürün Kampanya Ekleme İşlemi
    protected void btnHediyeKampanya_Click(object sender, EventArgs e)
    {
        try
        {

         UrunKampanyaDB.HediyeKampanyaKaydet(Request.Params["id"], ddlHediyeKampanya.SelectedValue);

         HediyeKampanyaListe();

        }
        catch (Exception hata)
        {
            lblMesaj.Text = " Hata:" + hata.ToString();
            lblMesaj.CssClass = "input-notification error";
        }
    }
    #endregion

    #region Hediye Ürün Kampanya Listeleme
    protected void HediyeKampanyaListe()
    {
        try
        {
            grwHediyeKampanya.DataSource = HediyeUrunDB.TitleListFor(Request.Params["id"]);
            grwHediyeKampanya.DataBind();
        }
        catch (Exception hata)
        {
            lblMesaj.Text = " Hata:" + hata.ToString();
            lblMesaj.CssClass = "input-notification error";
        }
    }
    #endregion

    #region Hediye Ürün Kampanya Silme İşlemi
    protected void grwHediyeKampanya_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "urunHediyeKampanyaSil")
            {
                int urunId = Convert.ToInt32(Request.QueryString["id"]);

                HediyeUrunDB.TitleDeleteFor(urunId, Convert.ToInt32(e.CommandArgument));
                HediyeKampanyaListe();
            } 
        }
        catch (Exception hata)
        {
            lblMesaj.Visible = true;
            lblMesaj.Text = "Ürün Kampanya Silme İşlemi" + hata.ToString();
        }
    }
    #endregion

    #region Hediye Ürün Kampanya Ekleme İşlemi
    private void HediyeUrunKampanyaList()
    {
        try
        {
            ddlHediyeKampanya.DataSource = HediyeUrunDB.TitleList("web");
            ddlHediyeKampanya.DataTextField = "title";
            ddlHediyeKampanya.DataValueField = "id";
            ddlHediyeKampanya.DataBind();

            ListItem item = new ListItem();
            item.Text = "-- Hediye Kampanyası Seçiniz --";
            item.Value = "0";
            item.Selected = true;
            ddlHediyeKampanya.Items.Add(item);
        }
        catch (Exception hata)
        {
            mesajGosterSis("Hata:", hata);
        }
    }
    #endregion

    // Ürün Yorum ekleme
    #region Ürün Yorum Ekleme
    protected void btnYorumKaydet_Click(object sender, EventArgs e)
    {
        SqlConnection baglan = new SqlConnection(ConnectionString.Get);
        try
        {
            HttpCookie cookie = Request.Cookies["LensOptikAdminGiris"];
            cookie = HttpSecureCookie.Decode(cookie);

            SqlCommand komutver;

            baglan.Open();
            komutver = new SqlCommand();
            komutver.Connection = baglan;
            komutver.CommandText = "urun_YorumEkle";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@uye_Id", SqlDbType.Int);
            komutver.Parameters["@uye_Id"].Value = Convert.ToInt32(cookie["yoneticiId"]);
            komutver.Parameters.Add("@uyeAdi", SqlDbType.NVarChar);
            komutver.Parameters["@uyeAdi"].Value = txtAdSoyad.Text;

            komutver.Parameters.Add("@urun_Id", SqlDbType.Int);
            komutver.Parameters["@urun_Id"].Value = Convert.ToInt32(Request.QueryString["id"]);
            komutver.Parameters.Add("@degerKiriteri", SqlDbType.Int);
            komutver.Parameters["@degerKiriteri"].Value = Convert.ToInt32(ddlDegerlendirme.SelectedItem.Value);
            komutver.Parameters.Add("@yorum", SqlDbType.NVarChar);
            komutver.Parameters["@yorum"].Value = txtUrunYorum.Text.ToString();
            komutver.Parameters.Add("@deger_dondur", SqlDbType.Int);
            komutver.Parameters["@deger_dondur"].Direction = ParameterDirection.Output;
            komutver.ExecuteNonQuery();
            int donendeger = Convert.ToInt32(komutver.Parameters["@deger_dondur"].Value);


            if (donendeger == 0)
            {
                txtUrunYorum.Text = "";
                txtAdSoyad.Text = "";
                mesajGosterOk("Yorum eklendi, görüntülene bilmesi için onay vermeniz gerekmekte.");
            }
            else
            {
                mesajGosterNo("Bu Yorumu Daha Önce Gönderdiniz");
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("Lütfen Daha Sonra Tekrar Deneyiniz..", ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion


    // Adet Bazlı Ürün İndirim İşlemleri
    #region Ürün indirim Kaydetme
    protected void btnUrunIndirimKampaya_Click(object sender, EventArgs e)
    {
        try
        {
            UrunIndirim indirim = new UrunIndirim
            {
                Adet = int.Parse(ddlUrunIndirimAdet.SelectedValue),
                UrunId = int.Parse(Request.QueryString["id"]),
                Oran = int.Parse(txtIndirimOran.Text)
            };

            UrunIndirimDB.Kaydet(indirim);

            UrunIndirimleriListe();
        }
        catch (Exception hata)
        {
            lblMesaj.Text = " Hata:" + hata.ToString();
            lblMesaj.CssClass = "input-notification error";
        }
    }
    #endregion

    #region Indirim Listeleme
    protected void UrunIndirimleriListe()
    {
        try
        {
            gvwUrunIndirim.DataSource = UrunIndirimDB.Liste(Request.Params["id"]);
            gvwUrunIndirim.DataBind();
        }
        catch (Exception hata)
        {
            lblMesaj.Text = " Hata:" + hata.ToString();
            lblMesaj.CssClass = "input-notification error";
        }
    }
    #endregion

    #region Ürün İndirim Silme İşlemi
    protected void gvwUrunIndirim_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "urunUrunIndirimSil")
            {
                int urunId = Convert.ToInt32(Request.QueryString["id"]);

                UrunIndirimDB.Delete(urunId, Convert.ToInt32(e.CommandArgument));

                UrunIndirimleriListe();
            }
        }
        catch (Exception hata)
        {
            lblMesaj.Visible = true;
            lblMesaj.Text = "Ürün Kampanya Silme İşlemi" + hata.ToString();
        }
    }
    #endregion

    // Kampanya Bilgi
    #region Kampanya Bilgi Kaydetme
    protected void btnKampanyaBilgi_Click(object sender, EventArgs e)
    {
        try
        {
            UrunKampanyaBilgi kampanya = new UrunKampanyaBilgi
            {
               Sira  = int.Parse(ddlKampanyaSira.SelectedValue),
               UrunId = int.Parse(Request.QueryString["id"]),
               Bilgi = txtKampanyaBilgi.Text
            };

            UrunKampanyaDB.UrunBilgiKaydet(kampanya);

            KampanyaBilgiListe();
        }
        catch (Exception hata)
        {
            lblMesaj.Text = " Hata:" + hata.ToString();
            lblMesaj.CssClass = "input-notification error";
        }
    }
    #endregion

    #region Kampanya bilgi Listeleme
    protected void KampanyaBilgiListe()
    {
        try
        {
            gvwKampanyaBilgi.DataSource = UrunKampanyaDB.BilgiListe(Convert.ToInt32(Request.Params["id"]));
            gvwKampanyaBilgi.DataBind();
        }
        catch (Exception hata)
        {
            lblMesaj.Text = " Hata:" + hata.ToString();
            lblMesaj.CssClass = "input-notification error";
        }
    }
    #endregion

    #region Kampanya Bilgi Silme İşlemi
    protected void gvwKampanyaBilgi_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "kampanyaBilgiSil")
            {
                UrunKampanyaDB.UrunBilgiSil(Convert.ToInt32(e.CommandArgument));
                KampanyaBilgiListe();
            }
        }
        catch (Exception hata)
        {
            lblMesaj.Visible = true;
            lblMesaj.Text = "Ürün Kampanya Silme İşlemi" + hata.ToString();
        }
    }
    #endregion
}