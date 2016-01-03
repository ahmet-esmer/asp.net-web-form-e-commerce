using System;
using LoggerLibrary;

namespace MailLibrary
{
    public class MailManager
    {
        private Mail instanceMail;
        private static MailManager instance;
        private delegate void MailSendUserHandler(string toMail, string title, string content);
        private delegate void MailSendAdminHandler(string title, string content);

        private MailManager()
        {
            instanceMail = new Mail();
        }

        public static MailManager Admin
        {
            get
            {
                if (instance == null)
                {
                    lock (new object())
                    {
                        if (instance == null)
                            instance = new MailManager();
                    }
                }
                return instance;
            }
        }
        public static MailManager User
        {
            get
            {
                if (instance == null)
                {
                    lock (new object())
                    {
                        if (instance == null)
                            instance = new MailManager();
                    }
                }
                return instance;
            }
        }

        public bool Bulk(string toMail, string title, string content)
        {
            bool retrieve = true;
            try
            {
                if (instanceMail != null)
                {
                    instanceMail.Send(toMail, title, content);
                }
            }
            catch (Exception)
            {
                retrieve = false;
            }

            return retrieve;
        }

        public void Send(string title, string content, ProcessType type = ProcessType.Normal)
        {
            if (type == ProcessType.Normal)
            {
                instanceMail.Send(title, content);
            }
            else
            {
                try
                {
                    MailSendAdminHandler sendAdmin = new MailSendAdminHandler(instanceMail.Send);
                    sendAdmin.BeginInvoke(title, content, null, null);
                }
                catch (Exception ex)
                {
                    LogManager.SqlDB.Write("Asecron Mail Gönderim", ex);
                }
            }
        }

        public void Send(string toMail, string title, string content, ProcessType type = ProcessType.Normal)
        {
            if (instanceMail != null)
            {
                if (type == ProcessType.Normal)
                {
                    instanceMail.Send(toMail, title, content);
                }
                else
                {
                    try
                    {
                        MailSendUserHandler sendUser = new MailSendUserHandler(instanceMail.Send);
                        sendUser.BeginInvoke(toMail, title, content, null, null);
                    }
                    catch (Exception ex)
                    {
                        LogManager.SqlDB.Write("Asecron Mail Gönderim", ex);
                    }
                }
            }
        }

    }
}
