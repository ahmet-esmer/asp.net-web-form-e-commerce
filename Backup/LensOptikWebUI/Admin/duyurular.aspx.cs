using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataAccessLayer;
using ModelLayer;
using BusinessLayer.BasePage;

public partial class AdminDuyurular : BasePageAdmin
{

    SqlConnection baglan;
    SqlCommand komutver;
    DataSet ds;
   
    protected void Page_Load(object sender, EventArgs e)
    {
       
        baglan = new SqlConnection(ConnectionString.Get);
        if (!IsPostBack)
        {
            if (Request.Params["islem"] == "sil")//Silme İşlemi 
            {
                KayitSil(Convert.ToInt32(Request.Params["id"]));
            }

            if (Request.Params["islem"] == "durum")
            {
                aktifPasif();
            }

            if (Request.Params["msg"] == "ok")
            {
                mesajGosterOk("Güncelleme İşlemi Başarı İle Yapıldı.");  
            }
            if (Request.Params["msg"] == "no")
            {
               mesajGosterNo("Güncelleme İşlemi Gelçekleşmedi");
            }

            duyuruListele();
        }
    }


    #region Duyuru Aktif Pasif Yapma İşlemi
    private void aktifPasif()
    {
        try
        {
            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "duyuru_Durum";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = Convert.ToInt32(Request.Params["id"]);
            komutver.Parameters.Add("@durum", SqlDbType.Int);
            komutver.Parameters["@durum"].Value = Convert.ToInt32(Request.Params["durum"]);
            komutver.ExecuteNonQuery();


        }
        catch (Exception ex)
        {
            mesajGosterSis("<b>Durum Degiştirme Hatası: </b>" , ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion

    #region Duyuru Listele
    private void duyuruListele()
    {
        try
        {
            SqlParameter parametre = new SqlParameter("@paremetre","admin");

            SqlDataReader dr = SqlHelper.ExecuteReader("duyuru_Listele", parametre);
            List<Duyuru> duyuruListe = new List<Duyuru>();
            while(dr.Read())
            {
                Duyuru info = new Duyuru(
                    dr.GetInt32(dr.GetOrdinal("id")),
                    dr.GetString(dr.GetOrdinal("duyuru_adi")),
                    dr.GetString(dr.GetOrdinal("duyuru_icerik")),
                    dr.GetDateTime(dr.GetOrdinal("eklenme_tarihi")),
                    dr.GetBoolean(dr.GetOrdinal("durum")));

                duyuruListe.Add(info);
            }

            dr.Close();
            GridView1.DataSource = duyuruListe;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            mesajGosterSis("Duyuru Listeleme Hatası" , ex);
        }
    }
    #endregion

    #region Duyuru Silme İşlemi
    private void KayitSil(int id)
    {
        try
        {
            ds = new DataSet();
            ds = DuyuruDB.ResimAdiListele(id);

            int resimkayitsay = Convert.ToInt32(ds.Tables[0].Rows.Count);
            for (int i = 0; i < resimkayitsay; i++)
            {
                if (File.Exists(resim.BigImagePath.ToString() + ds.Tables[0].Rows[i]["resim_adi"].ToString()))
                {
                    File.Delete(resim.BigImagePath.ToString() + ds.Tables[0].Rows[i]["resim_adi"].ToString());
                }
            }

            komutver = new SqlCommand();
            baglan.Open();
            komutver.Connection = baglan;
            komutver.CommandText = "duyuru_Sil";
            komutver.CommandType = CommandType.StoredProcedure;
            komutver.Parameters.Add("@id", SqlDbType.Int);
            komutver.Parameters["@id"].Value = Convert.ToInt32(id);
            komutver.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            mesajGosterSis("Duyuru  Kayit Silme Hatası" , ex);
        }
        finally
        {
            baglan.Close();
        }
    }
    #endregion

    protected void GridView1_Paging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;

    }

    protected void btnYoonetiEkle_Click(object sender, EventArgs e)
    {
        Response.Redirect("duyuru_ekle.aspx");
    }
}
