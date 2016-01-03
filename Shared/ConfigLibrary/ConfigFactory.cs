using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigLibrary.Readers;

namespace ConfigLibrary
{
    internal class ConfigFactory
    {
        static ConfigReaderBase _reader;

        public static ConfigReaderBase GetReader()
        {
            if (_reader == null)
            {
                _reader = GetConfigReader();
            }
            return _reader;
        }

        private static ConfigReaderBase GetConfigReader()
        {
            Type readerType = Type.GetType("ConfigLibrary.Readers.XmlConfigReader");
            return (ConfigReaderBase)Activator.CreateInstance(readerType);
        }
    }
}
