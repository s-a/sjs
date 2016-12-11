using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sjs
{
    public static class JSON
    {
        public static string stringifyDataTable(System.Data.DataTable dt)
        {
            var lst = dt.AsEnumerable().Select(r => r.Table.Columns.Cast<DataColumn>()
                .Select(c => new KeyValuePair<string, object>(c.ColumnName, r[c.Ordinal]))
                .ToDictionary(z => z.Key, z => z.Value))
                .ToList();
            //now serialize it
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(lst);
        }
        public static string stringify(System.Object dt)
        { 
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(dt);
        }
    }
}
