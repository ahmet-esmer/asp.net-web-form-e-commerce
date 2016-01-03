using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigLibrary
{

    public class EnumOperations
    {
        public static T GetEnumValue<T>(string value)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            catch
            {
                throw new Exception(string.Format("Can not convert {0} to enum type {1}", value, typeof(T).ToString()));
            }
        }
    }

    public  class ConvertHelper
    {
       public static T ConvertValue<T>(object value)
       {
           try
           {
               if (!(value is IConvertible))
                   return (T)value;

               if (typeof(T).BaseType == typeof(Enum))
               {
                   return EnumOperations.GetEnumValue<T>(value.ToString());
               }
               if (typeof(T) == typeof(bool))
               {
                   object returnValue = false;
                   if (
                       value.ToString().Equals("1"))
                   {
                       returnValue = true;
                       //return (T)Convert.ChangeType(Convert.ToInt32(value), typeof(T));
                   }
                   return (T)returnValue;
               }
               return (T)Convert.ChangeType(value, typeof(T), new System.Globalization.CultureInfo("tr-TR"));
           }
           catch (Exception)
           {
               Exception exp = new Exception("CONVERTIONERROR");
               exp.Data.Add("VALUE", value);
               exp.Data.Add("TYPE", typeof(T).ToString());
               throw exp;
           }
       }
    }
}
