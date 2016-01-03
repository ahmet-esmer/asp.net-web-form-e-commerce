using System;
using LoggerLibrary.Abstract;
using LoggerLibrary.ConcreteLoggers;

namespace LoggerLibrary
{
    internal class LogFactory
    {
        public static LoggerBase GetInstens(string location)
        {
            if (location == "DbLogger")
                return new DbLogger();
            else if (location == "EventLogger")
                return new EventLogger();
            else if (location ==  "MailLogger")
                return new MailLogger();
            else if (location == "TextLogger")
                return new TextLogger();
            else
                throw new Exception("istenen lokasyon desteklenmiyor");
        
        }
    }
}
