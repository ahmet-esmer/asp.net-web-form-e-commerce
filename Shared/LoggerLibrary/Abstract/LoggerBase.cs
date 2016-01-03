using System;

namespace LoggerLibrary.Abstract
{
    public abstract class LoggerBase
    {
        protected abstract void WriteCore(Exception mesaj);
        protected abstract void WriteCore(string baslik, string mesaj);
        protected abstract void WriteCore(string baslik, Exception mesaj);
        protected abstract void Core();

        protected virtual string Baslik { get; set; }
        protected virtual string Mesaj { get; set; } 
        public LoggerBase NextLogger { get; set; }

        public virtual void Write(string baslik, string mesaj)
        {
            try
            {
                WriteCore(baslik, mesaj);
            }
            catch (Exception)
            {
                if (NextLogger != null)
                {
                    NextLogger.Write(baslik, mesaj);
                }
            }
        }
        public virtual void Write(string baslik, Exception mesaj)
        {
            try
            {
                WriteCore(baslik, mesaj);
            }
            catch (Exception)
            {
                if (NextLogger != null)
                {
                    NextLogger.Write(baslik, mesaj);
                }
            }
        }
        public virtual void Write(Exception mesaj)
        {
            try
            {
                WriteCore(mesaj);
            }
            catch (Exception)
            {
                if (NextLogger != null)
                {
                    NextLogger.Write(mesaj);
                }
            }
        }

    }
}
