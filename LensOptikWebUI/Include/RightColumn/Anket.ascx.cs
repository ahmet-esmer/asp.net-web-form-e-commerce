using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using LoggerLibrary;
using ModelLayer;
using DataAccessLayer;
using BusinessLayer;

public partial class Include_RightColumn_Anket : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            anketListele();

            if (Request.Cookies["AnketOylama"] != null)
            {
               hlAnketOyla.Visible = false;
            }
        }
    }

    #region Anket Listeleme İşlemi
    private void anketListele()
    {
        try
        {
            SqlParameter parametre = new SqlParameter("@parametre", "web");

            using (SqlDataReader dr = SqlHelper.ExecuteReader("anket_KayitListele", parametre))
            {
                List<Anket> anket = new List<Anket>();
                while (dr.Read())
                {
                    if (dr.GetString(dr.GetOrdinal("anketBaslik")) != null)
                    {
                        lblAnketBaslik.Text = dr.GetString(dr.GetOrdinal("anketBaslik"));
                        hlAnketGoster.NavigateUrl = "~/Icerik/Anketler.aspx?AnketDetay=" + dr.GetInt32(dr.GetOrdinal("id")).ToString();
                    }

                    anket.Add(new Anket(dr.GetInt32(dr.GetOrdinal("soruId")), dr.GetString(dr.GetOrdinal("soru"))));
                }

                if (!dr.HasRows)
                {
                    pnlAnket.Visible = false;
                }

                rptAnketSorulari.DataSource = anket;
                rptAnketSorulari.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Sağ Panel Anket Hatası", ex);
        }
    }
    #endregion

}