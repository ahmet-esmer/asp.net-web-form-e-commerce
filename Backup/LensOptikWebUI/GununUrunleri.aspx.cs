using System;
using System.Collections.Generic;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;

public partial class gunun_urunleri: System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        SayfaMetaTag();
        GelecekGunler();
    }

    private void SayfaMetaTag()
    {
        try
        {
            Icerik Icerik = IcerikDB.IcerikGetir("Günün Ürünleri");
            Page.Title = Icerik.title;
            Page.MetaDescription = Icerik.description;
            Page.MetaKeywords = Icerik.keywords;
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Gelecek günlere ait ürünler metatag:", hata);
        }
    }

    private void GelecekGunler()
    {
        try
        {
            List<GununUrunu> gununUrunleri = GunuUrunuDB.Listele("web");

            if (gununUrunleri == null)
                Mesaj.Alert("Gelecek tarihlere ait kampanyalı ürün bulunamadı.");
         

            rptGunuUrunleri.DataSource = gununUrunleri;
            rptGunuUrunleri.DataBind();

            //-SAHMET-AHMET -Usa -P123456 -i"D:\SQLRun.sql"
        }
        catch (Exception hata)
        {
            Mesaj.ErrorSis("Listeleme Hatası.", hata.ToString());
        }
    }

}