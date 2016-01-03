using System;
using BusinessLayer.BasePage;
using DataAccessLayer;
using ModelLayer;
using ImageLibrary;


public partial class admin_Hediye_Urun_Kayit : BasePageAdmin
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                KampanyaBaslikGetir();
            }
        }
    }

    private void KampanyaBaslikGetir()
    {
        try
        {
            btnHediyeBaslik.Visible = false;
            btnHediyeBaslikGuncele.Visible = true;
            
            UrunHediyeBaslik baslik = HediyeUrunDB.GetTitle(Convert.ToInt32(Request.QueryString["id"]));
            txtHediyeBaslık.Text = baslik.Title;
            ddlLimit.SelectedValue = baslik.Limit.ToString();
            ckbDurum.Checked = baslik.Durum;
            imgKampanya.ImageUrl = "~/Products/Small/"+ baslik.Resim;

            ViewState["resim"] = baslik.Resim;

        }
        catch (Exception ex)
        {
            mesajGosterSis("Hata:", ex);
        }
    }

    protected void btnHediyeBaslikGuncele_Click(object sender, EventArgs e)
    {
        try
        {
            UrunHediyeBaslik baslik = new UrunHediyeBaslik();

            baslik.Resim = ViewState["resim"].ToString();
            baslik.Id = Convert.ToInt32(Request.QueryString["id"]);
            baslik.Durum = ckbDurum.Checked;
            baslik.Limit = Convert.ToInt32(ddlLimit.SelectedValue);
            baslik.Title = txtHediyeBaslık.Text;


            if (fluKampanya.PostedFile.FileName != "" || fluKampanya.PostedFile.ContentLength > 0)
            {
                Images.SmallImage.Delete(baslik.Resim);
                Images.BigImage.Delete(baslik.Resim);

                baslik.Resim = Images.GetImageName(fluKampanya.PostedFile.FileName);
                fluKampanya.PostedFile.SaveAs(Images.GetPath(baslik.Resim));

                Images.BigImage.Save(baslik.Resim);
                Images.SmallImage.Save(baslik.Resim, 130,true);
            }


            HediyeUrunDB.TitleUpdate(baslik);

            Response.Redirect("hediyeUrun.aspx");
        }
        catch (Exception ex)
        {
            mesajGosterSis("Hata: ", ex);
        }
    }

    protected void btnHediyeBaslik_Click(object sender, EventArgs e)
    {
        try
        {
            UrunHediyeBaslik hediye = new UrunHediyeBaslik
            {
                Title = txtHediyeBaslık.Text,
                Limit = Convert.ToInt32(ddlLimit.SelectedValue),
            };


            if (fluKampanya.PostedFile.FileName != "" || fluKampanya.PostedFile.ContentLength > 0)
            {
                hediye.Resim = Images.GetImageName(fluKampanya.PostedFile.FileName);
                fluKampanya.PostedFile.SaveAs(Images.GetPath(hediye.Resim));

                Images.BigImage.Save(hediye.Resim);
                Images.SmallImage.Save(hediye.Resim, 130, true);
            }


            int baslikId = HediyeUrunDB.TitleSave(hediye);

            Response.Redirect("hediyeUrunDetay.aspx?baslikId=" + baslikId.ToString());
        }
        catch (Exception ex)
        {
            mesajGosterSis("Hata:", ex);
        }
    }

}
