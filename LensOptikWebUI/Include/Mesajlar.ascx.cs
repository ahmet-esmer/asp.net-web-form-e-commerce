using System;

public partial class include_mesajlar : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Successful(string str)
    {
        Mesaj_Ok.Visible = true;
        Mesaj_yaz_ok.Text = str.ToString();
    }

    public void SuccessfulHide()
    {
        Mesaj_Ok.Visible = false;
    }

    public void Alert(string str)
    {
        Mesaj_No.Visible = true;
        Mesaj_yaz_no.Text = str.ToString();
    }

    public void AlertHide()
    {
        Mesaj_No.Visible = false;
    }

    public void Info(string str)
    {
        Mesaj_Info.Visible = true;
        Mesaj_yaz_Info.Text = str.ToString();

    }

    public void InfoHide()
    {
        Mesaj_Info.Visible = false;

    }

    public void ErrorSis(string str)
    {
        Mesaj_Sis.Visible = true;
        Mesaj_yaz_sis.Text = str.ToString();

    }

    public void ErrorSis(string str, string hata)
    {
        Mesaj_Sis.Visible = true;
        Mesaj_yaz_sis.Text = str.ToString() + hata.ToString();

    }
}