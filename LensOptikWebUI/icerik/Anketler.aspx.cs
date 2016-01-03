using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using LoggerLibrary;
using ModelLayer;
using DataAccessLayer;
using BusinessLayer;

public partial class Icerik_Anketler : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.Params["AnketDetay"] != null)
            {
                if (GenelFonksiyonlar.IsNumber(Request.Params["AnketDetay"]))
                {
                    AnketDetayGoster(Convert.ToInt32(Request.Params["AnketDetay"]));
                }
            }
            else
            {
                Response.Redirect("~/");
            }

            AnketBaslikiGoster();
        }
    }

    #region Anket Gösterim İşlemi
    private void AnketDetayGoster(int id)
    {
        try
        {
            SqlParameter parametre = new SqlParameter("@id", id);
            using (SqlDataReader dr = SqlHelper.ExecuteReader("anket_KayitGetir", parametre))
            {
                List<Anket> anketGoster = new List<Anket>();
                while (dr.Read())
                {
                    if (dr.GetString(dr.GetOrdinal("anketBaslik")) != null)
                    {
                        lblAnketSonucBaslik.Text = dr.GetString(dr.GetOrdinal("anketBaslik"));
                    }

                    if (dr.GetInt32(dr.GetOrdinal("toplamOy")) != 0)
                    {
                        int toplam = dr.GetInt32(dr.GetOrdinal("toplamOy"));
                        lblToplamOy.Text = "Anket Toplam Oylanma: " + Convert.ToString(toplam);

                        DateTime tarih = dr.GetDateTime(dr.GetOrdinal("tarih"));
                        lblTarih.Text = DateFormat.Tarih(tarih.ToString()).ToString();
                    }
                    
                    
                    Anket info = new Anket(
                        dr.GetInt32(dr.GetOrdinal("soruId")),
                        dr.GetInt32(dr.GetOrdinal("anketOy")),
                        dr.GetInt32(dr.GetOrdinal("toplamOy")),
                        dr.GetString(dr.GetOrdinal("anketBaslik")),
                        dr.GetString(dr.GetOrdinal("soru")),
                        dr.GetDateTime(dr.GetOrdinal("tarih")));

                    anketGoster.Add(info);
                }
                rptAnketGoster.DataSource = anketGoster;
                rptAnketGoster.DataBind();
            }
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Anket İşlemi", hata);
        }
    }
    #endregion

    #region Anket Gösterim İşlemi
    private void AnketBaslikiGoster()
    {
        try
        {
            SqlParameter parametre = new SqlParameter("@parametre", "admin");
            using (SqlDataReader dr = SqlHelper.ExecuteReader("anket_KayitListele", parametre))
            {
                List<Anket> anketGenelGoster = new List<Anket>();
                while (dr.Read())
                {
                    Anket info = new Anket(
                        dr.GetInt32(dr.GetOrdinal("id")),
                        dr.GetString(dr.GetOrdinal("anketBaslik")),
                        dr.GetDateTime(dr.GetOrdinal("tarih")));

                    anketGenelGoster.Add(info);
                }

                rptGenelAnketler.DataSource = anketGenelGoster;
                rptGenelAnketler.DataBind();
            }
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Anket İşlemi", hata);
        }
    }
    #endregion

    #region Anket Detay Gösterme
    protected void rptGenelAnketler_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "btnAnketBaslik")
        {
            string index = e.CommandArgument.ToString();

            AnketDetayGoster(Convert.ToInt32(index));
        }
    }
    #endregion
}