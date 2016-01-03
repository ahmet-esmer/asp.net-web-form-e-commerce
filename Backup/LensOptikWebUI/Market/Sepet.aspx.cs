using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;
using ServiceLayer.ExtensionMethods;
using ServiceLayer.Messages.Sepet;
using BusinessLayer.Security;

public partial class Market_Sepet : System.Web.UI.Page
{
    private int uyeId = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        LoginKontrol();

        if (!IsPostBack)
        {
            if (Request.Params["kayit"] != null)
            {
                Mesaj.Successful("Üyelik İşleminiz Başarı ile Gerçekleşti.");
            }

            if (Request.Form["urunId"] != null)
            {
                sepetUrunekle();
            }

            SepetListeleme(uyeId);
        }
    }

    private void LoginKontrol()
    {
        if (KullaniciOperasyon.LoginKontrol())
        {
            uyeId = KullaniciOperasyon.GetId();
        }
        else
        {
            if (Request.QueryString["user"] == "ok")
                Response.Redirect("~/Default.aspx?user=ok");
            else
                Response.Redirect("~/Default.aspx");
        }
    }


    #region SepetListele İşlemi
    private void SepetListeleme(int uyeId)
    {
        try
        {
            SepetResponse response = SepetDB.GetListFor(uyeId).ConvertToSepetResponse();

            gvwSepet.DataSource = response.SepetGride;
            gvwSepet.DataBind();

            if (response.Indirim > 0)
            {
                trIndirim.Visible = true;
                lblIndirim.Text = "- " + response.Indirim.ToString("c");
            }

            lblKargoToplam.Text = response.KargoFiyat.ToString("c");
            lblBirimToplam.Text = response.BirimFiyatToplam.ToString("c");
            lblKdvToplam.Text = response.KDVToplam.ToString("c");
            lblToplam.Text = response.FiyatToplam.ToString("c");

            if (response.FiyatToplam == 0)
            {
                tblSepet.Visible = false;
                sepetBos.Visible = true;
                imBtnSatinAl.Enabled = false;
                imBtnSatinAl.ToolTip = "Sepetinz Boş";
                Mesaj.Info("Sepetinizde ürün bulunmamakatadır.");
            }
        }
        catch (Exception ex)
        {
            Mesaj.ErrorSis("Sepet Listeleme hatası..", ex.ToString());
            LogManager.SqlDB.Write("Sepet Listeleme hatası", ex);
        }
    }
    #endregion

    #region Sepete Ürün Ekleme
    private void sepetUrunekle()
    {
        try
        {
            SqlParameter[] prm = new SqlParameter[9];
            prm[0] = new SqlParameter("@uyeId", uyeId);
            prm[1] = new SqlParameter("@urunId", Request.Form["urunId"]);
            prm[2] = new SqlParameter("@sagAdet", Request.Form["sagAdet"]);
            prm[3] = new SqlParameter("@solAdet", Request.Form["solAdet"]);
            prm[4] = new SqlParameter("@sagBilgi", Request.Form["sagBilgi"]);
            prm[5] = new SqlParameter("@solBilgi", Request.Form["solBilgi"]);
            prm[6] = new SqlParameter("@hediyeId", Request.Form["hediyeId"]);
            prm[7] = new SqlParameter("@hediyeBilgi", Request.Form["hediyeBilgi"]);
      
            prm[8] = new SqlParameter("@deger_dondur", SqlDbType.Int);
            prm[8].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery("Sepet_UrunEkle", prm);


            int geriDonus = (int)prm[8].Value;

            if (geriDonus == -1)
            {
                Mesaj.Successful("Ürün sepetinize  eklendi.");
            }
            else if (geriDonus >= 0)
            {
                Mesaj.Alert("Talep etiginiz ürün stoklarımızda <b> " + geriDonus.ToString() + " </b> adet bulunmaktadır");
            }
        }
        catch (Exception ex)
        {
            Mesaj.ErrorSis("İşlem hata ile Sonuçlandı.."+ ex.ToString());
            LogManager.SqlDB.Write("Sepete Ürün Eklerken Hata Oluştu", ex);
        }
    }
    #endregion

    //#region Sepet Ürün Miktar Düzenleme İşlemi
    //protected void ureunMiktarArtitma(object sender, EventArgs e)
    //{

    //    DropDownList list = (DropDownList)sender;

    //    int urunMiktar = 0;
    //    int sepetId = 0;

    //    foreach (GridViewRow item in grvSepet.Rows)
    //    {
    //        HiddenField hdId = (HiddenField)item.FindControl("hdfSepetId");
    //        HiddenField hdStok = (HiddenField)item.FindControl("hdfUrunAdet");
    //        HiddenField hdMiktar = (HiddenField)item.FindControl("hdfUrunMiktar");

    //        if (list.ToolTip == hdId.Value)
    //        {
    //            urunMiktar = Convert.ToInt32(list.SelectedValue);

    //            if (urunMiktar > Convert.ToInt32(hdStok.Value))
    //            {

    //                HataMesajlari1.mesajGosterNo("Talep etiginiz ürün stoklarımızda <b>" + hdStok.Value + "</b> Adet Bulunmaktadır..");

    //                list.SelectedValue = hdMiktar.Value.ToString();

    //            }
    //            // Kontrolu geçtikten sonra sepetId geri atanacak
    //            else
    //            {
    //                HataMesajlari1.mesajGizleInfo();
    //                sepetId = Convert.ToInt32(list.ToolTip);
    //            }
    //        }
    //    }


    //    if (!(sepetId == 0 && urunMiktar == 0))
    //    {
    //        try
    //        {
    //            SqlParameter[] parametre = new SqlParameter[] 
    //            { 
    //                new SqlParameter("@sepetId",  sepetId),
    //                 new SqlParameter("@miktar", urunMiktar),
    //            };

    //            SqlHelper.ExecuteNonQuery(ConnectionString.Get.cumle, CommandType.StoredProcedure, "Sepet_UrunMiktarGuncelle", parametre);

    //            HataMesajlari1.mesajGizleOk();
    //            sepetUrunleriListele(uyeId);
    //        }
    //        catch (Exception hata)
    //        {
    //            HataMesajlari1.mesajGosterSis(" Ürün miktarı düzenlenirken hata oluştu.");
    //            mailGonder sepeteMiktarHataMail = new mailGonder();
    //            sepeteMiktarHataMail.hataMesajiGonder("Ürün miktarı düzenlenirken hata oluştu.", hata.ToString());
    //        }

    //    }
    //}
    //#endregion

    #region GridVeiw Sepet ürün silme
    protected void gvwSepet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "sepetSil")
        {
            try
            {
                SqlParameter parametre = new SqlParameter("@sepetId", Convert.ToInt32(e.CommandArgument.ToString()));
                SqlHelper.ExecuteNonQuery("Sepet_UrunSil", parametre);

                SepetListeleme(uyeId);
            }
            catch (Exception ex)
            {
                Mesaj.ErrorSis("Sepetten ürün silinirken hata oluştu.", ex.ToString());
                LogManager.Mail.Write("Sepetten ürün silinirken hata oluştu.", ex);
            }
        }
    }
    #endregion
}