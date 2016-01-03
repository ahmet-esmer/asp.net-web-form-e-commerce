using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using BusinessLayer;
using BusinessLayer.BasePage;
using BusinessLayer.PagingLink;

public partial class bankaMesajlar : BasePageAdmin
{
  
    private int sayfaNo, Baslangic, Bitis;
    private int sayfaGosterim = 20;

    protected void Page_Load(object sender, EventArgs e)
    {
     
        if (!IsPostBack)
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


            ltlSayfalama.Text = PagingLink.GetHtmlCode(Request.QueryString, sayfaGosterim, BankaMesajlariDB.ItemCount());
            Mesajlar();
        }
    }


    #region İçerik Başlıklari Listeleme
    private void Mesajlar()
    {
        try
        {
            rptHatalar.DataSource = BankaDB.GeriBildirimMesajlari(Baslangic, Bitis);
            rptHatalar.DataBind();
        }
        catch (Exception ex)
        {
            mesajGosterSis("Mesaj Listeleme Hatası: ", ex);
        }
    }
    #endregion


    #region Hata Mesajları Sil
    protected void btnHataMesajSil_Click(object sender, EventArgs e)
    {
        SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM tbl_bankaMesajlari");
    }
    #endregion

}