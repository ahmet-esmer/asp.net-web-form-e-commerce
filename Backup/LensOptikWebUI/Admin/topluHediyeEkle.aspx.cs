using System;
using System.Web.UI.WebControls;
using BusinessLayer.BasePage;
using DataAccessLayer;

public partial class admin_toplu_hediye_ekle : BasePageAdmin
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

    protected void btnUrunHediye_Click(object sender, EventArgs e)
    {
        try
        {
            string serial = ddlkategoriler.SelectedValue;
            int kamUrunId = Convert.ToInt32(txtUrunId.Text);

            int urunVarmi = UrunDB.UrunVarmi(kamUrunId);

            if (urunVarmi == 0 && kamUrunId > 0)
	        {
                mesajGizleOk();
                mesajGosterNo(string.Format("Girmiş olduğunuz <b>{0}</b> nolu ürün Id Hatalı.", kamUrunId));
                return;
	        }

            int urunSayisi = UrunKampanyaDB.TopluKaydet(serial, kamUrunId);

            if (kamUrunId > 0)
            {
                mesajGizleNo();
                mesajGosterOk(string.Format("Toplam <b>{0}</b> adet ürüne hediye eklendi.", urunSayisi ));  
            }
            else
            {
                mesajGizleNo();
                mesajGosterOk("Toplam <b>" + urunSayisi.ToString() + "</b> adet ürüne hediye Silindi."); 
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("Hata: ", ex);
        }
    }

}
