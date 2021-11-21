using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Wordz.DB
{
    public static class DbHelper
    {
        public static T GetValueOrDefault<T>(this DbDataReader reader, string name)
        {
            if (Convert.IsDBNull(reader[name]))
            {
                return default(T);
            }
            
            return (T)reader[name];
        }

        public static string AsDbParam(this string source)
        {
            return "@" + source;
        }
    }
}
