using ConfigLibrary;

namespace BusinessLayer
{
    public class ConnectionString
    {
        public static string Get = ConfigHelper.GetConnectionString();
    }
}
