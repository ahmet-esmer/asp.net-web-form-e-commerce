using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web;

namespace ServiceLayer
{
    public static class QueryString
    {
        public static T Value<T>(string parameterName) where T : IConvertible
        {
            return Value<T>(parameterName, default(T));
        }

        public static T Value<T>(string parameterName, T defaultValue) where T : IConvertible
        {
            if (HttpContext.Current == null)
                throw new InvalidOperationException("HttpContext.Current is null");

            HttpRequest request = HttpContext.Current.Request;

            string input;
            if (request.QueryString[parameterName] != null && !string.IsNullOrEmpty(request.QueryString[parameterName]))
                input = request.QueryString[parameterName];
            else
                return defaultValue;

            T value;
            try
            {
                value = (T)Convert.ChangeType(input, typeof(T));
            }
            catch (Exception)
            {
                return defaultValue;
                //throw new InvalidOperationException(string.Format("Unable to convert query string parameter named '{0}' with value of '{1}' to type {2}", parameterName, input, typeof(T).FullName), ex);
            }
            return value;
        }  

    }
}
