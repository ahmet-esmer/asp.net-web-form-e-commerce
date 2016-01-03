using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using DataAccessLayer;
using ImageLibrary;
using ModelLayer;
using BusinessLayer.BasePage;
using BusinessLayer.PagingLink;

public partial class Admin_Markalar : BasePageAdmin
{

    SqlDataReader dr;
    private int Baslangic, Bitis;
    private int sayfaGosterim = 14;
    private int sayfaNo = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request.QueryString["Sayfa"] != null)
            {
                sayfaNo = Convert.ToInt32(Request.QueryString["Sayfa"]);
            }


            Baslangic = (sayfaNo * sayfaGosterim) + 1;
            Bitis = Baslangic + sayfaGosterim - 1;

            if (Request.Params["islem"] == "sil")
            {
                markaSil(Convert.ToInt32(Request.Params["id"]));
            }

            if (Request.Params["myLetter"] != null)
            {

                SqlParameter[] sqlParametre = new SqlParameter[]
                { 
                    new SqlParameter("@letter ", Request.Params["myLetter"]),
                    new SqlParameter("@parametre", "Letter")
                };

                dr = SqlHelper.ExecuteReader("marka_aramaAdmin", sqlParametre);
                markaListele(dr);
            }

            else if (Request.Params["islem"] == "genel")
            {

                SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@Baslangic", Baslangic),
                    new SqlParameter("@Bitis", Bitis)
                };

                dr = SqlHelper.ExecuteReader("marka_KayitListeleAdmin", parametre);

                ltlSayfalama.Text = PagingLink.GetHtmlCode(Request.QueryString, sayfaGosterim, MarkaDB.ItemCount());
                markaListele(dr);
            }
            if (Request.Params["islem"] == "durum")
            {
                try
                {
                    MarkaDB.Durum(Convert.ToInt32(Request.Params["id"]), Request.Params["durum"]);
                    Response.Redirect(Request.ServerVariables["HTTP_REFERER"].ToString());
                }
                catch (Exception ex)
                {
                    mesajGosterSis("<b>Durum Degiştirme Hatası: </b>", ex);
                }
            }

            CreateLink();
        }
      
       
    }

    #region Yeni Marka Ekleme
    protected void btnMarkaEkle_Click1(object sender, EventArgs e)
    {
        try
        {
            int donenDeger = MarkaDB.Kaydet(txt_markaAd.Text, Convert.ToBoolean(ckb_durum.Checked), Convert.ToBoolean(ckbDisbrutor.Checked));
            if (donenDeger == 0)
            {
                mesajGizleNo();
                mesajGosterOk("Kayıt Başarı ile Yapıldı");
                txt_markaAd.Text = "";
            }
            else
            {
                mesajGizleOk();
                mesajGosterNo("Bu Marka  daha önce kaydı Yapılmıştır");
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("Marka Ekleme Hatası:", ex);
        }
    }
    #endregion

    #region Marka Güncelleme İşlemi
    protected void btnMarkaGuncelle_Click(object sender, EventArgs e)
    {
        try
        {
            MarkaDB.Guncelle(Convert.ToInt32(hdfMarkaId.Value), txt_markaAd.Text.ToString(), Convert.ToBoolean(ckb_durum.Checked), Convert.ToBoolean(ckbDisbrutor.Checked));
            Response.Redirect(Request.ServerVariables["HTTP_REFERER"].ToString());

        }
        catch (Exception ex)
        {
            mesajGosterSis("GÜNCELLEME HATASI:", ex);
        }
    }
    #endregion

    #region Marka Alfa Listeleme
    protected void CreateLink()
    {
        string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "V", "Y", "Z" };
        for (int i = 0; i < letters.Length; i++)
        {
            HyperLink myLetter = new HyperLink();
            myLetter.ID = myLetter + i.ToString();
            myLetter.NavigateUrl = "?myLetter=" + letters[i].ToString();
            myLetter.CssClass = "letters";
            myLetter.Text = letters[i].ToString() + " ";
            this.PagingPanel.Controls.Add(myLetter);
        }
    }
    #endregion
 
    #region Marka Listeleme
    private void markaListele(SqlDataReader dr)
    {
        try
        {
            List<Markalar> markaTablo = new List<Markalar>();
            int count = 0;
            while (dr.Read())
            {
                count += +1;
                Markalar info = new Markalar(
                    dr.GetString(dr.GetOrdinal("markaUrunSayisi")),
                    dr.GetInt32(dr.GetOrdinal("id")),
                    dr.GetString(dr.GetOrdinal("marka_adi")),
                    dr.GetBoolean(dr.GetOrdinal("durum")),
                    dr.GetBoolean(dr.GetOrdinal("disbrutor")),
                    dr.GetInt32(dr.GetOrdinal("sira")));
                markaTablo.Add(info);
            }

          
            if (count == 0)
            {
                mesajGosterNo("Arama Sonucu: " + count.ToString());
            }
            else
            {
                mesajGizleNo();
            }

            GridView1.DataSource = markaTablo;
            GridView1.DataBind();
        }

        catch (Exception ex)
        {
            mesajGosterSis("Sayfa Listeleme Hatası", ex);
        }
        finally
        {
            dr.Close();
        }
    }
    #endregion

    #region Marka Silme İşlemi
    private void markaSil(int id)
    {
        try
        {
            foreach (var item in MarkaDB.ResimAdiListeleSilme(id))
            {
                Images.BigImage.Delete(item.resimAdi);
                Images.SmallImage.Delete(item.resimAdi);
                Images.LittleImage.Delete(item.resimAdi);
            }

            MarkaDB.Delete(id);
            Response.Redirect(Request.ServerVariables["HTTP_REFERER"].ToString(), false);

        }
        catch (Exception ex)
        {
            mesajGosterSis("Marka Silme Hatası:" , ex);
        }
    }
    #endregion

    #region Marka Sıralama İşlemi
    protected void MarkaSiraGuncelleme(object sender, EventArgs e)
    {
        try
        {
            TextBox txt = (TextBox)sender;
            int katId = Convert.ToInt32(txt.ToolTip);
            string katSira = txt.Text.ToString();

            if (katSira == "")
                return;

            IcerikDB.GenelSiralama(katId, katSira, "marka");
        }
        catch (Exception ex)
        {
            mesajGosterSis("Marka Sira Güncelleme", ex);
        }
    }
    #endregion

    protected void btnMarkaAra_Click(object sender, EventArgs e)
    {
        SqlParameter[] sqlParametre = new SqlParameter[]
        {
          new SqlParameter("@letter ", txtMarkaAra.Text.ToString()),
          new SqlParameter("@parametre", "adArama") 
        };

        dr = SqlHelper.ExecuteReader("marka_aramaAdmin", sqlParametre);

        CreateLink();
        ltlSayfalama.Visible = false;
        markaListele(dr);
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "btnGuncell")
        {
            ModalPopupExtender.Show();
            btnMarkaEkle.Visible = false;
            btnMarkaGuncelle.Visible = true;

            string[] kayitDizi = e.CommandArgument.ToString().Split(',');

            hdfMarkaId.Value = kayitDizi[0];
            ckb_durum.Checked = Convert.ToBoolean(kayitDizi[1]);
            txt_markaAd.Text = kayitDizi[2];
        }  
    }
}
