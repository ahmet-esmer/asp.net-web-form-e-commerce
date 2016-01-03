using System;
using System.IO;
using ConfigLibrary;
using LoggerLibrary.Abstract;

namespace LoggerLibrary.ConcreteLoggers
{
    public class EventLogger :LoggerBase
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
        
            FileStream fs = new FileStream(ConfigHelper.GetPhysicalPath(@"Logs\EventLog.txt"), 
                                           FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            streamWriter.WriteLine();
            streamWriter.Write(DateTime.Now.ToString("dd MMMM yyyy dddd HH:mm "));
            streamWriter.Write(base.Baslik);
            streamWriter.Write(" Mesaj : " + base.Mesaj);
            streamWriter.Flush();
            streamWriter.Close();
        }

    }
}
