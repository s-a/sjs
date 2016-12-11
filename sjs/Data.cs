using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sjs
{
    public static class Data
    {
        public static System.Data.DataTable SelectPagedDataRows(System.Data.DataTable dt, int page = 1, int pageSize = 10)
        {
            var total = dt.Rows.Count; 
            var skip = pageSize * (page - 1);
            var end = skip + pageSize;
            var canPage = skip < total;

            if (!canPage) // do what you wish if you can page no further
                return null;

            if (end > dt.Rows.Count)
            {
                end = dt.Rows.Count;
            }
            System.Data.DataTable dtn = dt.Clone();
            for (int i = skip; i < end; i++)
            {
                dtn.ImportRow(dt.Rows[i]);
            }

            return dtn;
        }

        public static string dataTableColsToJsonMeta(System.Data.DataColumnCollection cols)
        {
            var result = new List<Dictionary<string, object>>();

            foreach(System.Data.DataColumn col in cols)
            {
                var dict = new Dictionary<string, object>();
                dict.Add("key", col.ColumnName);
                dict.Add("pk", false);

                result.Add(dict);
            }
            //now serialize it
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string res = serializer.Serialize(result);
            return res;
        }

        public static string toJSON(System.Data.DataTable dt, int currentPage, int pageSize, int totalRowCount)
        {
            string res = "{";
                res += "\"cols\" : " + sjs.Data.dataTableColsToJsonMeta(dt.Columns) + ",";
                res += "\"rows\" : " + sjs.JSON.stringifyDataTable(dt) + ",";
                res += "\"rowCount\" : " + sjs.JSON.stringify(totalRowCount) + ",";
                res += "\"page\" : " + sjs.JSON.stringify(currentPage) + ",";
                res += "\"pageSize\" : " + sjs.JSON.stringify(pageSize) + "";
            res += "}";
            return res;
        }
    }
}
