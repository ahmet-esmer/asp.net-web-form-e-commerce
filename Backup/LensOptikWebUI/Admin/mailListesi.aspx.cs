using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using BusinessLayer;
using BusinessLayer.BasePage;
using BusinessLayer.PagingLink;

public partial class AdminUyeler : BasePageAdmin
{

    private int sayfaNo, Baslangic, Bitis;
    private int sayfaGosterim = 12;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["islem"] == "sil")
            {
                uyeSil(Request.Params["id"]);
            }

            if (Request.QueryString["Sayfa"] != null)
                sayfaNo = Convert.ToInt32(Request.QueryString["Sayfa"]);
            else
                sayfaNo = 0;


            Baslangic = (sayfaNo * sayfaGosterim) + 1;
            Bitis = Baslangic + sayfaGosterim - 1;

            ltlSayfalama.Text = PagingLink.GetHtmlCode(Request.QueryString, sayfaGosterim, MailDB.ItemCount());
            mailList(Baslangic, Bitis);

        }
    }

    #region Mail Listesi Listeleme
    private void mailList(int Baslangic, int Bitis )
    {
        SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@Baslangic", Baslangic),
                    new SqlParameter("@Bitis", Bitis)
                };

        DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "mailAdres_KayitListele", parametre);

        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    #endregion

    #region Mail Silme
    private void uyeSil(string id)
    {
        try
        {
            SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM tbl_mail_adresleri WHERE id="+ id.ToString());

            Response.Redirect(Request.ServerVariables["HTTP_REFERER"].ToString());
        }
        catch (Exception ex)
        {
            mesajGosterSis("Üye Silme Hatası:" , ex);
        }
    }
    #endregion

}
