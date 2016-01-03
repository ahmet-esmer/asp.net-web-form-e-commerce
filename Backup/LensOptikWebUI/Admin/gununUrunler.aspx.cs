using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using DataAccessLayer;
using BusinessLayer;
using BusinessLayer.BasePage;

public partial class adminGununUrunleri : BasePageAdmin
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            gununUrunleriListele();
        }
    }


    #region Günün  Ürünleri Listele İşlemi
    private void gununUrunleriListele()
    {
        try
        {
            gvwGununUrunleri.DataSource = GunuUrunuDB.Listele("admin");
            gvwGununUrunleri.DataBind();

        }
        catch (Exception ex)
        {
            mesajGosterSis("Günün Ürünleri Listeleme Hatası", ex);
        } 
    }
    #endregion


    #region Günün Ürünü Ekleme İşlemi
    protected void btnGununUrunu_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32( hdfTarih.Value)  == 0)
        {
            lblTarih.Text = " ! Geçmiş Tarihe Ait Ürün Ekleyemezsiniz.";
        }
        else
        {
            try
            {
                DateTime indirmGunu = Convert.ToDateTime(txtGunTarih.Text);
                SqlParameter[] sqlp = new SqlParameter[6];
                sqlp[0] = new SqlParameter("@urunId", SqlDbType.Int);
                sqlp[0].Value = Convert.ToInt32(txtUrunId.Text.Trim());
                sqlp[1] = new SqlParameter("@tarih", SqlDbType.DateTime);
                sqlp[1].Value = indirmGunu;
                sqlp[2] = new SqlParameter("@satisFiyat", SqlDbType.Money );
                sqlp[2].Value = Convert.ToDecimal(txtSatisFiyat.Text);
                sqlp[3] = new SqlParameter("@aciklama", SqlDbType.NVarChar);
                sqlp[3].Value = txtAciklama.Text;
                sqlp[4] = new SqlParameter("@indirimYuzde", SqlDbType.Int);
                sqlp[4].Value = Convert.ToInt16(txtUrunYuzde.Text);
                sqlp[5] = new SqlParameter("@deger_dondur", SqlDbType.Int);
                sqlp[5].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery("gununUrunu_Ekle",sqlp);

                if (sqlp[5].Value.ToString() == "0" )
                {
                    if (indirmGunu.ToShortDateString() == DateTime.Now.ToShortDateString())
                    {
                        SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "gununUrunu_FiyatIslem");
                    }

                    Response.Redirect("gununUrunler.aspx", false);
                }
                else
                {
                    mesajGosterInfo("! Bu güne ait bir kayıt mevcut lütfen kaydı silip tekrar deneyiniz..");
                }
            }
            catch (Exception ex)
            {
                mesajGosterSis("Hata Oluştu", ex);
            }
        }
    }
    #endregion

    #region Ekleme  Tarih Kontrolu
    protected void txtGunTarih_TextChanged(object sender, EventArgs e)
    {
        DateTime tarih = Convert.ToDateTime(txtGunTarih.Text);
        DateTime tarihBugun = Convert.ToDateTime( DateTime.Now.ToString("dd.MM.yyyy"));

        if (tarihBugun > tarih)
        {
            lblTarih.Visible = true;
            hdfTarih.Value = "0";
            lblTarih.Text = "Geçmiş Tarihe Ait Ürün Ekleyemezsiniz.";
        }
        else
        {
            lblTarih.Text = " ";
            hdfTarih.Value = "1";
        }
    }
    #endregion

    #region Günün Ürünü Silme İşlemi
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "gununUrunSil")
        {
            try
            {
                SqlParameter parametre = new SqlParameter("@id", Convert.ToInt32(e.CommandArgument));

                SqlHelper.ExecuteNonQuery("gununUrunu_Sil", parametre);
                gununUrunleriListele();
            }
            catch (Exception ex)
            {
                mesajGosterSis("Günün Ürünü Silme İşlemi", ex);
            }
        }
    }
    #endregion

    protected void txtUrunId_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string formId = txtUrunId.Text.Trim();

            if (!string.IsNullOrWhiteSpace(formId))
            {
               string query = "SELECT urunFiyat FROM tbl_urunler WHERE id='" + formId + "'";
               object fiyat = SqlHelper.ExecuteScalar(CommandType.Text, query);

                if ( fiyat  != null )
                {
                    hdfFiyat.Value = fiyat.ToString();
                    lblFiyat.Text = "Ürün Fiyatı: " + Convert.ToDecimal(fiyat).ToString("N");

                    lblUrunDurum.CssClass = "input-notification success";
                    lblUrunDurum.Visible = true;
                    lblUrunDurum.Text = formId + " Ürün Bulundu.";
                }
                else
                {
                    lblUrunDurum.CssClass = "input-notification error";
                    lblUrunDurum.Visible = true;
                    lblUrunDurum.Text = formId + " Ürün Bulunumadı.";
                }
            }


        }
        catch (Exception ex)
        {
            mesajGosterSis("Günün Ürünü Silme İşlemi", ex);
        }

    }

    protected void txtUrunYuzde_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtUrunYuzde.Text))
        {
            decimal toplam = Convert.ToDecimal(hdfFiyat.Value);
            decimal indirimYuzde = Convert.ToInt16(txtUrunYuzde.Text);
            decimal indirim;

            indirim = toplam * indirimYuzde / 100;

            indirim = toplam - indirim;
            txtSatisFiyat.Text = indirim.ToString("n"); 
        }
    }
}
