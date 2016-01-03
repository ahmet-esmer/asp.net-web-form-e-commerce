using System;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;
using BusinessLayer.PagingLink;

public partial class Icerik_SSorulanSorular : System.Web.UI.Page
{
    private int baslangic, bitis;
    private int sayfaGosterim = 8;
    private int sayfaNo = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SorulanSorularSayfaNo();
            SorulanSorularListele();
            PageMetaTag();
        }
    }

    private void SorulanSorularListele()
    {
        try
        {
            rptSSorulari.DataSource = SSorularDB.Liste(baslangic, bitis, "web");
            rptSSorulari.DataBind();
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Sık Sorulan Sorular Listeleme", hata);
        }
    }

    private void SorulanSorularSayfaNo()
    {
        try
        {
            if (Request.QueryString["Sayfa"] != null)
                sayfaNo = Convert.ToInt32(Request.QueryString["Sayfa"]);

            baslangic = (sayfaNo * sayfaGosterim) + 1;
            bitis = baslangic + sayfaGosterim - 1;

            ltlSayfalama.Text = PagingLinkWeb.GetHtmlCode(Request.QueryString, sayfaGosterim, SSorularDB.ListItemCount("web"));
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Sık Sorulan Sorular Listeleme", ex);
        }
    }

    public void PageMetaTag()
    {
        try
        {
            if (Request.QueryString["sayfaId"] != null)
            {
                MetaTag metaTag = IcerikDB.MetaTag(Convert.ToInt32(Request.QueryString["sayfaId"]));
                Page.Title = metaTag.Title;
                Page.MetaDescription = metaTag.Description;
                Page.MetaKeywords = metaTag.Keywords;
            }
        }
        catch (Exception hata)
        {
            LogManager.SqlDB.Write("Kategori Metatag:", hata);
        }
    }

}