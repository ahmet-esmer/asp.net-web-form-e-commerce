using System;
using System.Xml;
using BusinessLayer.BasePage;
using DataAccessLayer;
using ImageLibrary;
using ModelLayer;

public partial class admin_Hediye_Urun_Detay : BasePageAdmin
{

    protected void Page_Load(object sender, EventArgs e)
    {
        secenek_ismi.Attributes.Add("ondblclick", "moveOver_deger();");
        secenek_ismi2.Attributes.Add("ondblclick", "removeMe_deger();");

        if (!IsPostBack)
        {
            if (Request.QueryString["detayId"] != null )
            {
                HediyeGetir();
            }
        }
    }


    private void HediyeGetir()
    {

        try
        {
            btnUrunGuncelle.Visible = true;
            btnUrun.Visible = false;
            rfvResim.Enabled = false;

            UrunHediye hediye = HediyeUrunDB.GetGift(Request.QueryString["detayId"]);
            txtHediyeBaslık.Text = hediye.UrunAdi;
            txtUrunAdet.Text = hediye.Adet.ToString();
            ckbDurum.Checked = hediye.Durum;
            txtMarka.Text = hediye.Marka;

            imgUrun.Visible = true;
            imgUrun.ImageUrl = "../Products/Small/" + hediye.Resim;
            ViewState["resim"] = hediye.Resim;

            string[] secenekler = null;

            if (hediye.Secenek != null)
            {
                secenekler = hediye.Secenek.Split('|');
            }

            secenek_ismi2.Items.Clear();

            if (secenekler != null)
            {
                foreach (var item in secenekler)
                {
                    secenek_ismi2.Items.Insert(0, item);
                } 
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("Hata: ", ex);
        }
    }

    protected void btnUrun_Click(object sender, EventArgs e)
    {
        try
        {
            UrunHediye hediye = new UrunHediye();

            if (fluUrunResim.PostedFile.FileName != "" || fluUrunResim.PostedFile.ContentLength > 0)
            {
                hediye.Resim = Images.GetImageName(fluUrunResim.PostedFile.FileName);
                fluUrunResim.PostedFile.SaveAs(Images.GetPath(hediye.Resim));

                Images.BigImage.Save(hediye.Resim);
                Images.SmallImage.Save(hediye.Resim, 130, true);
            }

            hediye.UrunAdi = txtHediyeBaslık.Text;
            hediye.BaslikId = Convert.ToInt32(Request.QueryString["baslikId"]);
            hediye.Adet = Convert.ToInt32(txtUrunAdet.Text);
            hediye.Durum = ckbDurum.Checked;
            hediye.Marka = txtMarka.Text;

            hediye.Secenek = hdfSecenekler.Value;

            HediyeUrunDB.GiftSave(hediye);

            Response.Redirect("hediyeUrun.aspx");

        }
        catch (Exception ex)
        {
            mesajGosterSis("Hata: ", ex);
        }

    }

    protected void ddlSecenekSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            secenek_ismi.Items.Clear();

            string secim = ddlSecenekler.SelectedValue.ToString();

            if (secim == "null")
               return;
            

            using (XmlTextReader reader = new XmlTextReader(Server.MapPath("~/App_Data/" + secim + ".xml")))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "secenek")
                    {
                        secenek_ismi.Items.Insert(0, secim + ":" + reader.ReadString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            mesajGosterSis("Secenek Hata:", ex);
        }
    }

    protected void btnUrunGuncelle_Click(object sender, EventArgs e)
    {
        try
        {
            UrunHediye hediye = new UrunHediye();

            hediye.Resim = ViewState["resim"].ToString();

            if (fluUrunResim.PostedFile.FileName != "" || fluUrunResim.PostedFile.ContentLength > 0)
            {
                Images.SmallImage.Delete(hediye.Resim);
                Images.BigImage.Delete(hediye.Resim);

                hediye.Resim = Images.GetImageName(fluUrunResim.PostedFile.FileName);
                fluUrunResim.PostedFile.SaveAs(Images.GetPath(hediye.Resim));

                Images.BigImage.Save(hediye.Resim);
                Images.SmallImage.Save(hediye.Resim, 130,true);
            }


            hediye.Id = Convert.ToInt32(Request.QueryString["detayId"]);
            hediye.UrunAdi = txtHediyeBaslık.Text;
            hediye.Adet = Convert.ToInt32(txtUrunAdet.Text);
            hediye.Durum = ckbDurum.Checked;
            hediye.Marka = txtMarka.Text;
            hediye.Secenek = hdfSecenekler.Value;

            HediyeUrunDB.GiftUpdate(hediye);

            Response.Redirect("hediyeUrun.aspx");

        }
        catch (Exception ex)
        {
            mesajGosterSis("Hata: ", ex);
        }
    }


}
