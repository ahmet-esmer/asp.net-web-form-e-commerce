using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using ModelLayer;
using DataAccessLayer;
using BusinessLayer;
using BusinessLayer.BasePage;
using BusinessLayer.PagingLink;

public partial class AdminSSSorular:BasePageAdmin
{
    private int sayfaNo, Baslangic, Bitis;
    private int sayfaGosterim = 5;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (! IsPostBack)
        {
            if (Request.QueryString["Sayfa"] != null)
            {
                sayfaNo = Convert.ToInt32(Request.QueryString["Sayfa"]);
            }
            else
            {
                sayfaNo = 0;
            }

            Baslangic = (sayfaNo * sayfaGosterim) + 1;
            Bitis = Baslangic + sayfaGosterim - 1;

            if (Request.Params["islem"] == "sil")
            {
                kayitSil(Convert.ToInt32(Request.Params["id"]));
                Response.Redirect(Request.ServerVariables["HTTP_REFERER"].ToString());
            }

            if (Request.Params["islem"] == "durum")
            {
                aktifPasif();
                Response.Redirect(Request.ServerVariables["HTTP_REFERER"].ToString());
            }

            ltlSayfalama.Text = PagingLink.GetHtmlCode(Request.QueryString, sayfaGosterim, SSorularDB.ItemCount());
            SSSorulanlarListele();
        }
    }

    #region Soru Güncelleme İşlemi İçin Getirme
    private void SSSoruGetir( int id)
    {
        using (SqlConnection baglan = new SqlConnection(ConnectionString.Get))
        {
            baglan.Open();
            using (SqlCommand komutver = new SqlCommand())
            {
                komutver.Connection = baglan;
                komutver.CommandText = "SSSorlansular_Getir";
                komutver.CommandType = CommandType.StoredProcedure;
                komutver.Parameters.Add("@id", SqlDbType.Int);
                komutver.Parameters["@id"].Value = id ;

                SqlDataAdapter adp = new SqlDataAdapter(komutver);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                hdfId.Value = id.ToString();

                txtCevap.Text = GenelFonksiyonlar.BRDonusturEnter( ds.Tables[0].Rows[0]["cevap"].ToString());

                txtZiyaretciYorum.Text = GenelFonksiyonlar.BRDonusturEnter(ds.Tables[0].Rows[0]["soru"].ToString());

                ckbDurum.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["durum"]);
                
            }
        }
    }
    #endregion

    #region Sik sorulan sorular Durum İşlemi
    private void aktifPasif()
    {
        try
        {
            string sqr = string.Format("UPDATE tbl_SSSorulanlar  SET durum='{0}' WHERE id ='{1}'", Request.Params["durum"], Request.Params["id"]);

            SqlHelper.ExecuteNonQuery(CommandType.Text, sqr);
        }
        catch (Exception ex)
        {
            mesajGosterSis("<b>Durum Degiştirme Hatası: </b>", ex);
        }
    }
    #endregion

    #region Soru Silme İşlemi
    private void kayitSil(int id)
    {
        try
        {
            string sqr = string.Format("DELETE tbl_SSSorulanlar  WHERE id ='{0}'", id.ToString());
            SqlHelper.ExecuteNonQuery(CommandType.Text, sqr);
        }
        catch (Exception ex)
        {
            mesajGosterSis("<b>Silme Hatası:</b>  ", ex);
        }
    }

    #endregion

    #region Sık sorulan sorular Listeleme İşlemi
    private void SSSorulanlarListele()
    {
        try
        {
            SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@Baslangic", Baslangic),
                    new SqlParameter("@Bitis", Bitis),
                    new SqlParameter("@parametre", "admin")
                };


                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "SSSorulanlar_Listele", parametre);

                GridView1.DataSource = ds;
                GridView1.DataBind();

        }
        catch (Exception ex)
        {
            mesajGosterSis("Listeleme Hatası", ex);
        }
    }
    #endregion

    #region Sık sorulan soru güncelleme işlemi
    protected void btnSSSoruGuncelle_Click(object sender, EventArgs e)
    {
        try
        {
            using(SqlConnection baglan = new SqlConnection(ConnectionString.Get))
	        {
                baglan.Open();
                using (SqlCommand komutver = new SqlCommand())
                {
                    komutver.Connection = baglan;
                    komutver.CommandText = "SSSorular_Guncelle";
                    komutver.CommandType = CommandType.StoredProcedure;
                    komutver.Parameters.Add("@id", SqlDbType.Int);
                    komutver.Parameters["@id"].Value = Convert.ToInt32(hdfId.Value);
                    komutver.Parameters.Add("@soru", SqlDbType.NVarChar);
                    komutver.Parameters["@soru"].Value = GenelFonksiyonlar.EnterDonusturBr(txtZiyaretciYorum.Text.ToString());

                    komutver.Parameters.Add("@cevap", SqlDbType.NVarChar);
                    komutver.Parameters["@cevap"].Value = GenelFonksiyonlar.EnterDonusturBr(txtCevap.Text.ToString());
                    komutver.Parameters.Add("@durum", SqlDbType.Bit);
                    komutver.Parameters["@durum"].Value = Convert.ToBoolean(ckbDurum.Checked);

                    komutver.ExecuteNonQuery();

                    txtZiyaretciYorum.Text = "";
                    txtCevap.Text = "";
                    btnSSSoruGuncelle.Visible = false;
                    btnSSSoruKaydet.Visible = true;

                    mesajGosterOk("Kayıt Başarı ile Güncellendi.");
                   

                }
	        }
        }
        catch (Exception ex)
        {
            mesajGosterSis("<b>YORUM GÖNDERME HATAS: </b> ", ex);
        }
    }
    #endregion

    #region Sık sorulan soru kaydetme işlemi
    protected void btnSSSoruKaydet_Click(object sender, EventArgs e)
    {
        try
        {
            using (SqlConnection baglan = new SqlConnection(ConnectionString.Get))
            {
                baglan.Open();
                using (SqlCommand komutver = new SqlCommand())
                {
                    komutver.Connection = baglan;
                    komutver.CommandText = "SSSorular_Ekle";
                    komutver.CommandType = CommandType.StoredProcedure;
                    komutver.Parameters.Add("@soru", SqlDbType.NVarChar);
                    komutver.Parameters["@soru"].Value = GenelFonksiyonlar.EnterDonusturBr(txtZiyaretciYorum.Text.ToString());
                    komutver.Parameters.Add("@cevap", SqlDbType.NVarChar);
                    komutver.Parameters["@cevap"].Value = GenelFonksiyonlar.EnterDonusturBr(txtCevap.Text.ToString());
                    komutver.Parameters.Add("@durum", SqlDbType.Bit);
                    komutver.Parameters["@durum"].Value = Convert.ToBoolean(ckbDurum.Checked);

                    komutver.ExecuteNonQuery();
                    txtZiyaretciYorum.Text = "";

                    //mesajGosterOk("Kayıt başarı ile oluşturuldu");

                    Response.Redirect(Request.ServerVariables["HTTP_REFERER"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("<b>YORUM GÖNDERME HATAS: </b> ", ex);
        }
    }
    #endregion


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "btnGuncell")
        {
            SSSoruGetir(Convert.ToInt32(e.CommandArgument));
            btnSSSoruGuncelle.Visible = true;
            btnSSSoruKaydet.Visible = false;
            ModalPopupExtender.Show();
        }
    }


    #region Sıralama İşlemi
    protected void SSSorulanlarSira(object sender, EventArgs e)
    {
        try
        {
            TextBox txt = (TextBox)sender;
            int katId = Convert.ToInt32(txt.ToolTip);
            string katSira = txt.Text.ToString();

            if (katSira == "")
                return;

            string sqr = string.Format("UPDATE tbl_SSSorulanlar  SET sira='{0}' WHERE id ='{1}'", katSira, katId );

            SqlHelper.ExecuteNonQuery(CommandType.Text, sqr);

        }
        catch (Exception ex)
        {
            mesajGosterSis("Kategori Sira Güncelleme", ex);
        }
    }
    #endregion



    protected void ckbPanel_CheckedChanged(object sender, EventArgs e)
    {

        CheckBox checkBox = (CheckBox)sender;
        string Id = checkBox.ToolTip;
        string panel = checkBox.Checked.ToString();


        string sqr = string.Format("UPDATE tbl_SSSorulanlar  SET panel='{0}' WHERE id ='{1}'", panel, Id);

        SqlHelper.ExecuteNonQuery(CommandType.Text, sqr);

    }
}
