using ConfigLibrary;

namespace DataAccessLayer
{
    public class BaseDB
    {
        public static string ConnectionString = ConfigHelper.GetConnectionString();
    }
}
