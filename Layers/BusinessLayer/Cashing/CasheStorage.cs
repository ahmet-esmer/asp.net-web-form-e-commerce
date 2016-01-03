using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace BusinessLayer.Cashing
{
    public static class CacheStorage
    {
        public static void Remove(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }
        public static bool Exists(string key)
        {
            if (HttpContext.Current.Cache[key] == null)
                return true;
            else
                return false;
        }
        public static void Store(string key, object data)
        {
            HttpContext.Current.Cache.Insert(key, data, null, DateTime.Now.AddMinutes(30),
                        System.Web.Caching.Cache.NoSlidingExpiration);
        }
        public static void Store(string key, object data, string tableName)
        {
            SqlCacheDependency sqlCacheDependency = new SqlCacheDependency("LensOptikDB", tableName);
            HttpContext.Current.Cache.Insert(key, data, sqlCacheDependency);
        }
        public static T Retrieve<T>(string key)
        {

            T itemStored = (T)HttpContext.Current.Cache.Get(key);
            if (itemStored == null)
                itemStored = default(T);

            return itemStored;
        }
    }
}
