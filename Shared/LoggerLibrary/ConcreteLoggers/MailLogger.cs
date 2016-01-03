using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using ConfigLibrary;
using LoggerLibrary.Abstract;

namespace LoggerLibrary.ConcreteLoggers
{
    class MailLogger : LoggerBase
    {
        private Boolean hataDurumu;

        protected override void WriteCore(string baslik, string mesaj)
        {
            base.Baslik = baslik;
            base.Mesaj = mesaj;
            Core();

            if (hataDurumu)
                Send();
        }

        protected override void WriteCore(Exception mesaj)
        {
            base.Baslik = mesaj.Message.ToString();
            base.Mesaj = mesaj.ToString();
            Core();

            if (hataDurumu)
                Send();
        }

        protected override void WriteCore(string baslik, Exception mesaj)
        {
            base.Baslik = baslik;
            base.Mesaj = mesaj.ToString();
            Core();

            if (hataDurumu)
                Send();
        }

        protected override void Core()
        {
            using (SqlConnection baglan = new SqlConnection(ConfigHelper.GetConnectionString()))
            {
                baglan.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglan;
                cmd.CommandText = "hataMesajiEkle";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@hataAdi", SqlDbType.NVarChar);
                cmd.Parameters["@hataAdi"].Value = base.Baslik;
                cmd.Parameters.Add("@hataMesaji", SqlDbType.NVarChar);
                cmd.Parameters["@hataMesaji"].Value = base.Mesaj;
                cmd.Parameters.Add("@deger_dondur", SqlDbType.Bit);
                cmd.Parameters["@deger_dondur"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                hataDurumu = Convert.ToBoolean(cmd.Parameters["@deger_dondur"].Value);
            }
        }

        private void Send()
        {
            string mail = ConfigHelper.GetValueAppSetting<string>("logMail");
            string username = ConfigHelper.GetValueAppSetting<string>("logUsername");
            string password = ConfigHelper.GetValueAppSetting<string>("logPassword");
            string smtpClient = ConfigHelper.GetValueAppSetting<string>("smtpClient");
            int SmtpPort = ConfigHelper.GetValueAppSetting<int>("smtpPort");
            
            MailMessage mesaj = new MailMessage(new MailAddress(mail, "Site Hata Mesajı"), new MailAddress(mail));
            NetworkCredential cr = new NetworkCredential(username,password);
            mesaj.Subject = base.Baslik; 
            mesaj.IsBodyHtml = true;
            mesaj.Body = base.Mesaj;
            SmtpClient smtp = new SmtpClient(smtpClient.ToString());
            smtp.Credentials = cr;
            smtp.Port = SmtpPort;
            smtp.EnableSsl = false;
            smtp.Send(mesaj);
        }
    }
}
