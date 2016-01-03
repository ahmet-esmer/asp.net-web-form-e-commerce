using System;
using System.Net;
using System.Net.Mail;
using ConfigLibrary;
using LoggerLibrary;

namespace MailLibrary
{
    internal class Mail
    {
        private string MailSend { get; set; }
        private string Username { get; set; }
        private string Password { get; set; }
        private string SmtpClient { get; set; }
        private int SmtpPort { get; set; }

        internal Mail()
        {
            try
            {
                this.MailSend = ConfigHelper.GetValueAppSetting<string>("mail");
                this.Username = ConfigHelper.GetValueAppSetting<string>("username");
                this.Password = ConfigHelper.GetValueAppSetting<string>("password");
                this.SmtpClient = ConfigHelper.GetValueAppSetting<string>("smtpClient");
                this.SmtpPort = ConfigHelper.GetValueAppSetting<int>("smtpPort");
            }
            catch (Exception hata)
            {
                LogManager.SqlDB.Write("Mail Nesnesi Kurulurken Hata oluştu", hata);
            }
        }

        internal void Send(string toMail, string title, string content)
        {
            SendCore(toMail, title, content);
        }
        
        internal void Send(string title, string content)
        {
            SendCore(MailSend, title, content);
        }

        internal void SendCore(string toMail, string title, string content)
        {
            try
            {

                MailMessage mesaj = new MailMessage(new MailAddress(MailSend, "lensoptik.com.tr"),
                    new MailAddress(toMail));
                NetworkCredential cr = new NetworkCredential(Username, Password);

                mesaj.IsBodyHtml = true;
                mesaj.Subject = title;
                mesaj.SubjectEncoding = System.Text.Encoding.UTF8;
                mesaj.Body = content;
                mesaj.BodyEncoding = System.Text.Encoding.UTF8;

                SmtpClient smtp = new SmtpClient(SmtpClient);
                smtp.Credentials = cr;
                smtp.Port = SmtpPort;
                smtp.EnableSsl = false;
                smtp.Send(mesaj);

            }
            catch (Exception hata)
            { 
               LogManager.SqlDB.Write(string.Format("Başlık:{0}, Mail Adresi: {1}", title, toMail), hata);
               throw;
            }
        }
    }
}
