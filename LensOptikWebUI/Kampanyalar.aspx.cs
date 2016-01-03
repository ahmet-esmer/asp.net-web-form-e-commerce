using System;
using System.Collections.Generic;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;

public partial class kampanyalar: System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        KampanyaList();
        SayfaMetaTag();
    }

    private void SayfaMetaTag()
    {
        try
        {

            //UrunHediyeBaslik titel = HediyeUrunDB.GetTitle(

            //Icerik Icerik = IcerikDB.IcerikGetir("Günün Ürünleri");
            //Page.Title = Icerik.title;
            //Page.MetaDescription = Icerik.description;
            //Page.MetaKeywords = Icerik.keywords;
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Kampanyalar :", hata);
        }
    }

    private void KampanyaList()
    {
        try
        {
            rptKampanyalar.DataSource = HediyeUrunDB.TitleList("web");
            rptKampanyalar.DataBind();
        }
        catch (Exception hata)
        {
            Mesaj.ErrorSis("Listeleme Hatası.");
            LogManager.SqlDB.Write("Kampanyalar Liste :", hata);
        }
    }

}