using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BusinessLayer.Cashing;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;

public partial class Include_LeftColumn_Kategoriler : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlHead headTag = (HtmlHead)this.Page.Header;

        HtmlGenericControl jquery = new HtmlGenericControl();
        jquery.TagName = "script";
        jquery.Attributes.Add("src", ResolveUrl("~/Scripts/kategoriMenu.js"));
        jquery.Attributes.Add("type", "text/javascript");

        headTag.Controls.Add(jquery);

        if (!IsPostBack)
        {
            KategoriKayitListele();
        }
    }


    private void KategoriKayitListele()
    {
        try
        {

            if (CacheStorage.Exists(CacheKeys.Category))
            {
                lock (new object())
                {
                    if (CacheStorage.Exists(CacheKeys.Category))
                    {
                        CacheStorage.Store(CacheKeys.Category, KategoriDB.Liste(), "tbl_kategori");
                    }
                }
            }

            rptAnakategoriler.DataSource = Cache[CacheKeys.Category] as List<Kategori>;
            rptAnakategoriler.DataBind();
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write(" Sol Panel kategori ", ex);
        }
    }


    #region Altkategori Listeleme İşlemi
    protected void rptAnakategoriler_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater rptAltKategori = (Repeater)e.Item.FindControl("rptAltKategori");
            Label lblKat = (Label)e.Item.FindControl("lblKategori");
            HiddenField serial = (HiddenField)e.Item.FindControl("hdfSerial");

            try
            {
                string sSerial = serial.Value.ToString();
                string sCachName = string.Format("altKategori_{0}", sSerial);


                if (CacheStorage.Exists(sCachName))
                {
                    lock (new object())
                    {
                        if (CacheStorage.Exists(sCachName))
                        {
                            CacheStorage.Store(sCachName, KategoriDB.AltLKategoriler(sSerial), "tbl_kategori");
                        }
                    }
                }

                List<Kategori> lstKategori = Cache[sCachName] as List<Kategori>;
                rptAltKategori.DataSource = lstKategori;
                rptAltKategori.Visible = lstKategori.Count > 0;
                rptAltKategori.DataBind();

            
            }
            catch (Exception hata)
            {
                LogManager.SqlDB.Write("Sol Panel Alt kategori ", hata);
            }
        }
    }
    #endregion


}