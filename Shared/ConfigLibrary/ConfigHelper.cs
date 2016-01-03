using System.Configuration;
using ConfigLibrary.Readers;
using System.IO;
using System.Reflection;

namespace ConfigLibrary
{
    public class ConfigHelper
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["sqlConnectionStr"].ConnectionString;
        }

        public static T GetConfigValue<T>(string key)
        {
            ConfigReaderBase configReader = ConfigFactory.GetReader();
            return ConvertHelper.ConvertValue<T>(configReader.GetConfigValue(key));
        }

        public static T GetValueAppSetting<T>(string key)
        {
            return  ConvertHelper.ConvertValue<T>(ConfigurationManager.AppSettings[key]);
        }

        public static string GetPhysicalPath(string fileName)
        {
            string path = Path.GetDirectoryName(Assembly.GetAssembly(typeof(ConfigHelper)).CodeBase);

            path = path.Replace("file:\\", "");
            path = path.Replace("\\bin", "");

            return string.Format("{0}\\{1}", path, fileName);
        }

    }
}
