using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using BusinessLayer.BasePage;
using DataAccessLayer;

public partial class Admin_KampanyaFiyat : BasePageAdmin
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Listele();
        }
    }

    private void Listele()
    {
        string str = "SELECT id, aciklama, fiyat, puanKod FROM tbl_kampanyaFiyat";
        gvwKampanya.DataSource = SqlHelper.ExecuteDataset(CommandType.Text,str);
        gvwKampanya.DataBind();
    }

    protected void gvwKampanya_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Kaydet")
        {
            string index = e.CommandArgument.ToString();

            int id = 0;
            string aciklama = null;
            string fiyat = null;

            foreach (GridViewRow item in gvwKampanya.Rows)
            {
                if (item.RowType == DataControlRowType.DataRow)
                {
                    if (gvwKampanya.DataKeys[item.RowIndex].Value.ToString() == index.ToString())
                    {
                        id = Convert.ToInt32(e.CommandArgument);

                        aciklama = (item.FindControl("txtAciklama") as TextBox).Text;
                        fiyat = (item.FindControl("txtFiyat") as TextBox).Text;
                    }
                }
            }

            if (id != 0 && aciklama != null)
            {
                SqlParameter[] prm = new SqlParameter[3];
                prm[0] = new SqlParameter("@id", SqlDbType.Int);
                prm[0].Value = id;
                prm[1] = new SqlParameter("@aciklama", SqlDbType.NVarChar);
                prm[1].Value = aciklama;
                prm[2] = new SqlParameter("@fiyat", SqlDbType.Decimal);
                prm[2].Value = Convert.ToDecimal(fiyat);

                SqlHelper.ExecuteNonQuery("kampanyaFiyat_Guncelle", prm);
                mesajGosterOk( "kayıt başarı ile güncellendi.");
            }
        }

    }
}