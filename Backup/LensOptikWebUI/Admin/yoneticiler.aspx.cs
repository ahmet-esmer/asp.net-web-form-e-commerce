using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.BasePage;
using DataAccessLayer;
using ModelLayer;

public partial class adminYoneticiler : BasePageAdmin
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            kayitListele();
            Sehirler();
            Gun();
            yil();

            if (Request.Params["islem"] == "sil")
            {
               KayitSil(Convert.ToInt32(Request.Params["id"]));
            }

            if (Request.Params["islem"] == "duzenle")
            {
                btnYoneticiGuncelle.Visible = true;
                btnYoneticiEkle.Visible = false;

                yoneticiBilgiGetir();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "yonetici", "tabAc()", true);
            }
        }
    }

    #region Yönetici Kayit Listele İşlemi
    private void kayitListele()
    {
        try
        {
            GridView1.DataSource = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "kullaniciAdmin_KayitListele");
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            mesajGosterSis("<b>HATA OLUŞTU:</b>" , ex);
        }
    }
    #endregion

    #region Güncelleme İçin Yönetici Bilgi Getirme
    private void yoneticiBilgiGetir()
    {
        rowPosta.Visible = true;
        
        Kullanici k = KullaniciDB.Getir(Convert.ToInt32(Request.Params["id"]));

        txtAdSoyad.Text = k.AdiSoyadi;
        txtEposta.Text = k.EPosta;
        txtSifre.Attributes.Add("value", k.Sifre );
        txtTelefon.Text = k.Gsm;
        ddlSehirler.SelectedValue = k.Sehir;
        ckbEposta.Checked = k.PostaGonder;
        ckbSms.Checked = k.SMSGonder;

        for (int i = 0; i < 2; i++)
        {
            if (rdbCinsiyet.Items[i].Value == k.Cinsiyet.ToString())
            {
                rdbCinsiyet.Items[i].Selected = true;
            }
        }

        ddlGun.ClearSelection();
        string gun = null;
        if (k.DogumTarihi.Month < 10)
        {
            gun = "0" + k.DogumTarihi.Day.ToString();
        }
        else
        {
            gun = k.DogumTarihi.Day.ToString();
        }

        for (int i = 0; i < ddlGun.Items.Count; i++)
        {
            if (ddlGun.Items[i].Value == gun)
            {
                ddlGun.Items[i].Selected = true;
            }
        }


        ddlAy.ClearSelection();
        string ay = null;
        if (k.DogumTarihi.Month < 10)
        {
            ay = "0" + k.DogumTarihi.Month.ToString();
        }
        else
        {
            ay = k.DogumTarihi.Month.ToString();
        }

        for (int i = 0; i < ddlAy.Items.Count; i++)
        {
            if (ddlAy.Items[i].Value == ay)
            {
                ddlAy.Items[i].Selected = true;

            }
        }

        ddlYil.ClearSelection();
        for (int i = 0; i < ddlYil.Items.Count; i++)
        {
            if (ddlYil.Items[i].Value == k.DogumTarihi.Year.ToString())
            {
                ddlYil.Items[i].Selected = true;
            }
        }

    }
    #endregion

    #region Yönetici Güncelleme İşlemi
    protected void btnYoneticiGuncelle_Click(object sender, EventArgs e)
    {

        Kullanici kulanici = new Kullanici();
        kulanici.Id = Convert.ToInt32(Request.Params["id"]);
        kulanici.AdiSoyadi = txtAdSoyad.Text;
        kulanici.Gsm = txtTelefon.Text;
        kulanici.Cinsiyet = rdbCinsiyet.SelectedValue;
        kulanici.Sehir = ddlSehirler.SelectedValue;
        kulanici.EPosta = txtEposta.Text.ToLower();
        kulanici.Sifre = txtSifre.Text;
        kulanici.DogumTarihi = Convert.ToDateTime(string.Format("{0}.{1}.{2}", ddlGun.SelectedValue, ddlAy.SelectedValue, ddlYil.SelectedValue));
        kulanici.PostaGonder = ckbEposta.Checked;
        kulanici.SMSGonder = ckbSms.Checked;

        int donendeger = KullaniciDB.kaydet(kulanici);

        if (donendeger == 0)
        {
            mesajGosterOk(" Kayıt Başarı ile Yapıldı");

            if (Request.Cookies["yonteciGirisBilgi"] != null)
            {
                HttpCookie yonetici1 = new HttpCookie("yonteciGirisBilgi");
                yonetici1.Values.Add("yoneticiId", Request.Params["id"].ToString());
                yonetici1.Values.Add("yoneticiIsim", txtAdSoyad.Text.ToString());
                yonetici1.Values.Add("yoneticiPosta", txtEposta.Text.ToString());
                yonetici1.Values.Add("yoneticiSifre", txtSifre.Text.ToString());
                yonetici1.Expires = DateTime.Now.AddDays(120);
                Response.Cookies.Add(yonetici1);
            }
        }
        else
        {
            mesajGosterNo("Bu E-posta adresi daha önce kayıt Yapımıştır.");
        }

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "yonetici", "tabAc()", true); 
    }
    #endregion

    #region Yönetici Kayıt Sil
    private void KayitSil(int id)
    {
        try
        {
            SqlParameter prm = new SqlParameter("@id",id);
            SqlHelper.ExecuteNonQuery("kullanici_KayitSil",prm);
        }
        catch (Exception ex)
        {
            mesajGosterSis("<b>HATA OLUŞTU:</b>", ex);
        }
    }
    #endregion

    #region Yönetici Eklme İşlemi
    protected void btnYoneticiEkle_Click(object sender, EventArgs e)
    {
        Kullanici kulanici = new Kullanici();
        kulanici.AdiSoyadi = txtAdSoyad.Text;
        kulanici.Gsm = txtTelefon.Text;
        kulanici.Cinsiyet = rdbCinsiyet.SelectedValue;
        kulanici.Sehir = ddlSehirler.SelectedValue;
        kulanici.EPosta = txtEposta.Text.ToLower();
        kulanici.Sifre = txtSifre.Text;
        kulanici.DogumTarihi = Convert.ToDateTime(string.Format("{0}.{1}.{2}", ddlGun.SelectedValue, ddlAy.SelectedValue, ddlYil.SelectedValue));
        kulanici.KullaniciTipi = "admin";

        int donendeger = KullaniciDB.kaydet(kulanici);

        if (donendeger != 0)
        {
            txtAdSoyad.Text = "";
            txtEposta.Text = "";
            txtSifre.Text = "";
            txtSifreTekrari.Text = "";
            mesajGosterOk(" Kayıt Başarı ile Yapıldı");
            kayitListele();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "yonetici", "tabAc()", true);
            mesajGosterNo("Bu E-posta adresi daha önce kayıt Yapımıştır.");
        }
    }
    #endregion

    #region Doğum Tarihi Gün Listeleme
    protected void Gun()
    {
        ddlGun.ClearSelection();
        ListItem item = new ListItem();
        item.Text = "Gün";
        item.Value = "0";
        item.Selected = true;
        ddlGun.Items.Add(item);

        for (int i = 31; i >= 1; i--)
        {
            if (i < 10)
            {
                ddlGun.Items.Insert(1, new ListItem("0" + i.ToString(), "0" + i.ToString()));
            }
            else
            {
                ddlGun.Items.Insert(1, new ListItem(i.ToString(), i.ToString()));
            }
        }
    }
    #endregion

    #region Doğum Tarihi Yıl Listeleme
    protected void yil()
    {
        ddlYil.ClearSelection();
        ListItem item = new ListItem();
        item.Text = "Yıl";
        item.Value = "0";
        item.Selected = true;
        ddlYil.Items.Add(item);

        for (int i = 1930; i <= 1992; i++)
        {
            ddlYil.Items.Insert(1, new ListItem(i.ToString(), i.ToString()));
        }
    }
    #endregion

    public void Sehirler()
    {
        DataTable dt = SehirDB.Sehirler();

        foreach (DataRow dataRow in dt.Rows)
        {
            ddlSehirler.Items.Add(new ListItem(dataRow["Ad"].ToString(), dataRow["IlID"].ToString()));
        }

        ListItem item = new ListItem();
        item.Text = "-- Şehir Seçiniz --";
        item.Value = "0";
        item.Selected = true;
        ddlSehirler.Items.Add(item);
    }

}
