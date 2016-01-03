using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Cashing;
using DataAccessLayer;
using LoggerLibrary;
using ModelLayer;

public partial class ErrorPage_404State : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        UrunListele();


        StringBuilder sb = new StringBuilder();

        sb.Append("<span class='title'>HTTP 404 </span>");
        sb.Append("<div class='appText'>");
        sb.Append("<b>*</b> ");
        sb.Append("Ulaşmaya çalıştığınız sayfa bulunamadı;sayfa kaldırılmış veya taşınmış olabilir.");
        sb.Append("</br>");
        sb.Append("<b>*</b> ");
        sb.Append("Eğer web adresini kendiniz girdiyseniz, lütfen kontrol edip, doğru girdiğinizden emin olunuz.");
        sb.Append("</br>");
        sb.Append("<b>*</b> ");
        sb.Append("Eğer bir linke tıklayarak bu sayfaya ulaştıysanız, link hatalı olabilir.");
        sb.Append("</br>");
        sb.Append("<b>*</b> ");
        sb.Append("Aradığınız web sayfası silinmiş, adresi değiştirilmiş olabilir.");
        sb.Append("</br>");
        sb.Append("</div>");

       ucMesaj.Alert(sb.ToString()); 
    }

    private void UrunListele()
    {
        try
        {
            if (CacheStorage.Exists(CacheKeys.MainProducts))
            {
                lock (new object())
                {
                    if (CacheStorage.Exists(CacheKeys.MainProducts))
                        CacheStorage.Store(CacheKeys.MainProducts, UrunDB.Urunler("anaSayfa"), "tbl_urunler");
                }
            }

            ucProductList.ProductsToDisplay = Cache[CacheKeys.MainProducts] as List<Urun>;
        }
        catch (Exception ex)
        {
            LogManager.SqlDB.Write("Anasayfa ürün listeleme hatası", ex);
        }
    }

}