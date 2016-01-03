using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigLibrary.Readers
{
    internal abstract class ConfigReaderBase
    {

        Dictionary<string, object> _cache = new Dictionary<string, object>();
        object _dummyObject = new object();

        protected abstract object GetValue(string key);

        public virtual object GetConfigValue(string key)
        {
            if (_cache.ContainsKey(key))
                return _cache[key];

            lock (_dummyObject)
            {

                if (!_cache.ContainsKey(key))
                {
                    object value = GetValue(key);
                    _cache.Add(key, value);

                }

                return _cache[key];
            }
        }

    }
}
