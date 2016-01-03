using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using DataAccessLayer;
using BusinessLayer;
using BusinessLayer.BasePage;

public partial class Admin_Toplu_Islemler : BasePageAdmin
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            kategoriListele();
        }
    }

    #region  DropDown'a Kategori Listelem İşlemi
    private void kategoriListele()
    {

        ddlkategoriler.DataSource = KategoriDB.dropDownListele();
        ddlkategoriler.DataTextField = "kategoriadi";
        ddlkategoriler.DataValueField = "serial";
        ddlkategoriler.DataBind();

        ListItem item = new ListItem();
        item.Text = "-- Kategori Şeçiniz --";
        item.Value = "0";
        item.Selected = true;
        ddlkategoriler.Items.Add(item);
    }

    #endregion

    protected void btnFiyatIndirim_Click(object sender, EventArgs e)
    {
        string serial = ddlkategoriler.SelectedValue.ToString();

        if (serial.ToString() == "0")
        {
            mesajGosterInfo(" Kategori Seçiniz.");
            return;
        }


        SqlParameter[] parametre = new SqlParameter[3];
        parametre[0] = new SqlParameter("@serial", serial);
        parametre[1] = new SqlParameter("@indirimYuzde", Convert.ToInt32(txtFiyatYuzde.Text) );
        parametre[2] = new SqlParameter("@deger_dondur", SqlDbType.Int);
        parametre[2].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery("urun_TopluFiyatDuz", parametre);

       int donenDeger = Convert.ToInt32(parametre[2].Value);

       if (donenDeger == 0)
        {
            mesajGosterNo("İşlem yapılırken hata oluştu." + donenDeger);
            mesajGizlenfo();
        }
        else
        {
            mesajGosterOk("Toplam <b>" + donenDeger + "</b> ürün fiyatı başarı ile düzenlendi. ");
         
        }
    }

    #region Kategoriye ait toplam ürün sayısı bulma
    protected void ddlkategoriler_SelectedIndexChanged(object sender, EventArgs e)
    {
        string serial = ddlkategoriler.SelectedValue.ToString();

        if (serial.ToString() == "0")
        {
            mesajGosterInfo(" Kategori Seçiniz.");
            return;
        }


         SqlParameter[] parametre = new SqlParameter[] 
                { 
                    new SqlParameter("@serial", serial),
                    new SqlParameter("@parametre", "kategori"),
                    new SqlParameter("@markaId ",  0)
                
                };

      int  toplamKayit = (int)SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "urun_SayafaNo", parametre);

      mesajGosterInfo(ddlkategoriler.SelectedItem.ToString().Replace("-","") + " Kategorisine ait toplam<b> " + toplamKayit + "</b> ürün bulundu");
    }
    #endregion

    protected void btnFiyatGoster_Click(object sender, EventArgs e)
    {
        rowHavale.Visible = false;
        rowFiyat.Visible = true;
    }

    protected void btnHaveleGoster_Click(object sender, EventArgs e)
    {
        rowFiyat.Visible = false;
        rowHavale.Visible = true;
    }

    #region Toplu havale indirimi
    protected void btnHavaleIndirim_Click(object sender, EventArgs e)
    {
        string serial = ddlkategoriler.SelectedValue.ToString();

        if (serial.ToString() == "0")
        {
            mesajGosterInfo(" Kategori Seçiniz.");
            return;
        }

        SqlParameter[] parametre = new SqlParameter[3];
        parametre[0] = new SqlParameter("@serial", serial);
        parametre[1] = new SqlParameter("@havaleIndirim", Convert.ToInt32(txtHavale.Text));

        SqlHelper.ExecuteNonQuery("urun_TopluHavaleIndirim", parametre);

        mesajGosterOk(" ürün fiyatı başarı ile düzenlendi. ");

    }
    #endregion
}
