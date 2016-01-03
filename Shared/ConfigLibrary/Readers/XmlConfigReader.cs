using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ConfigLibrary.Readers
{
    internal class XmlConfigReader :  ConfigReaderBase        
    {
        protected override object GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
