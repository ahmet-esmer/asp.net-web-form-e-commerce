using System;
using System.IO;
using ConfigLibrary;
using LoggerLibrary.Abstract;

namespace LoggerLibrary.ConcreteLoggers
{
    class TextLogger: LoggerBase
    {
        protected override void WriteCore(string baslik, string mesaj)
        {
            base.Baslik = baslik;
            base.Mesaj = mesaj;
            Core();
        }
        protected override void WriteCore(Exception mesaj)
        {
            base.Baslik = mesaj.Message.ToString();
            base.Mesaj = mesaj.ToString();
            Core();
        }
        protected override void WriteCore(string baslik, Exception mesaj)
        {
            base.Baslik = baslik;
            base.Mesaj = mesaj.ToString();
            Core();
        }

        protected override void Core()
        {
            FileStream fs = new FileStream(ConfigHelper.GetPhysicalPath(@"Logs\SiteLog.txt"),
                                       FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            streamWriter.WriteLine(" ");
            streamWriter.WriteLine(base.Baslik);
            streamWriter.WriteLine("İşlem Tarihi: " + DateTime.Now.ToString("dd MMMM yyyy dddd HH:mm"));
            streamWriter.WriteLine("Hata Mesaj  : " + base.Mesaj);
            streamWriter.WriteLine("-------------------------------------------------------------------------");
            streamWriter.Flush();
            streamWriter.Close();
        }
    }
}
