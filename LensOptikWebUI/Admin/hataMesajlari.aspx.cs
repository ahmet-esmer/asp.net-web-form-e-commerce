using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using BusinessLayer;
using BusinessLayer.BasePage;
using BusinessLayer.PagingLink;

public partial class adminReklamlar : BasePageAdmin
{
  
    private int sayfaNo, Baslangic, Bitis;
    private int sayfaGosterim = 20;

    protected void Page_Load(object sender, EventArgs e)
    {
     
        if (!IsPostBack)
        {
            lblSayfaBaslik.Text = "Hata Mesajları";
            
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

            ltlSayfalama.Text = PagingLink.GetHtmlCode(Request.QueryString, sayfaGosterim, HataMesajlariDB.ItemCount());
            hataMesajlariListele();
        }
    }

    #region İçerik Başlıklari Listeleme
    private void hataMesajlariListele()
    {
        try
        {
            SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@Baslangic", Baslangic),
                    new SqlParameter("@Bitis", Bitis)
                };

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "hataMesajlariListele", parametre);

            rptHatalar.DataSource = ds;
            rptHatalar.DataBind();
        }
        catch (Exception ex)
        {
            mesajGosterSis("Hata Mesajları Listeleme ", ex);
        }
    }
    #endregion

    #region Hata Mesajları Sil
    protected void btnHataMesajSil_Click(object sender, EventArgs e)
    {
        SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM tbl_hataMesajlari");
        hataMesajlariListele();
    }
    #endregion
}