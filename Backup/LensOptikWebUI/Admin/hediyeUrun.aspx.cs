using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BusinessLayer.BasePage;
using DataAccessLayer;
using ImageLibrary;
using ModelLayer;

public partial class admin_Hediye_Urun : BasePageAdmin
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["islem"] == "detaySil")
            {
                UrunDetaySil();
            }

            if (Request.QueryString["islem"] == "baslikSil")
            {
                UrunBaslikSil();
            }

            HediyeListe();
        }
    }


    private void UrunBaslikSil()
    {
        try
        {
            int baslikId = Convert.ToInt32(Request.QueryString["id"]);

            IList<UrunHediye> hediyeUrunler = HediyeUrunDB.GiftList(baslikId);
            HediyeUrunDB.TitleDelete(baslikId);

            foreach (UrunHediye hediye in hediyeUrunler)
            {
                Images.SmallImage.Delete(hediye.Resim);
                Images.BigImage.Delete(hediye.Resim);
            }

        }
        catch (Exception ex)
        {
            mesajGosterSis("Hata:", ex);
        }
    }

    private void UrunDetaySil()
    {
        try
        {
            HediyeUrunDB.GiftDelete(Request.QueryString["detayId"]);

            Images.SmallImage.Delete(Request.QueryString["resim"]);
            Images.BigImage.Delete(Request.QueryString["resim"]);
        }
        catch (Exception ex)
        {
            mesajGosterSis("Hata:", ex);
        }
    }

    private void HediyeListe()
    {
        try
        {
            rptHediyeUrun.DataSource = HediyeUrunDB.TitleList("admin");
            rptHediyeUrun.DataBind();
        }
        catch (Exception ex)
        {
            mesajGosterSis("Hata: ", ex);
        }
    }

    protected void rptHediyeUrun_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptHediyeUrunDetay = (Repeater)e.Item.FindControl("rptHediyeUrunDetay");
                HiddenField hdfTitleId = (HiddenField)e.Item.FindControl("hdfTitleId");

                IList<UrunHediye> hediye = HediyeUrunDB.GiftList(Convert.ToInt32(hdfTitleId.Value));

                rptHediyeUrunDetay.DataSource = hediye;
                rptHediyeUrunDetay.DataBind();

                if (hediye == null)
                {
                    rptHediyeUrunDetay.Visible = false;
                }   
            }
        }
        catch (Exception hata)
        {
            mesajGosterSis("Hata: ", hata);
        }
    }
}
