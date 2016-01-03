using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Xml;
using BusinessLayer.BasePage;
using DataAccessLayer.XML;
using ModelLayer;

public partial class admin_secenekler : BasePageAdmin
{

    SecenekService service;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void gvwSecenek_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "SecenekKaydet")
        {
            string index = e.CommandArgument.ToString();
            string secenekYeni = "";

            foreach (GridViewRow item in gvwSecenek.Rows)
            {
                if (item.RowType == DataControlRowType.DataRow)
                {
                    if (gvwSecenek.DataKeys[item.RowIndex].Value.ToString() == index)
                    {
                        secenekYeni = (item.FindControl("txtSecenekDuzenle") as TextBox).Text;
                        break;
                    }
                }
            }

            service = new SecenekService(ddlSecenekler.SelectedValue);

            service.Update(new Secenek
            {
                Value = secenekYeni,
                Name = index
            });
        }
        else if (e.CommandName == "SecenekSil")
        {
            string index = e.CommandArgument.ToString();
            service = new SecenekService(ddlSecenekler.SelectedValue);
            service.Delete(index);

            SecenkListele(ddlSecenekler.SelectedValue);
        }
    }

    protected void ddlSecenekSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            trKaydet.Visible = true;
            SecenkListele(ddlSecenekler.SelectedValue);
        }
        catch (Exception ex)
        {
            mesajGosterSis("Secenek Hata:", ex);
        }
    }

    private void SecenkListele( string xmlFileName)
    {
        List<Secenek> secenekler = new List<Secenek>();

        using (XmlTextReader reader = new XmlTextReader(Server.MapPath("~/App_Data/" + xmlFileName + ".xml")))
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "secenek")
                {
                    secenekler.Add(new Secenek 
                    {
                        Name = xmlFileName,
                        Value = reader.ReadString() 
                    });
                }
            }

            gvwSecenek.DataSource = secenekler;
            gvwSecenek.DataBind();
        }
    }

    protected void btnSecenekEkle_Click(object sender, EventArgs e)
    {
        try
        {
            service  = new SecenekService(ddlSecenekler.SelectedValue);
            service.Add(txtSecenek.Text);

            SecenkListele(ddlSecenekler.SelectedValue);
        }
        catch (Exception ex)
        {
            mesajGosterSis("Hata:", ex);
        }
    }
}
