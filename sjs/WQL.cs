using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sjs
{
    public static class WQL
    {

        private readonly static System.Collections.Generic.IDictionary<System.Management.CimType, Type> Cim2TypeTable = new System.Collections.Generic.Dictionary<System.Management.CimType, Type>
        {
            {System.Management.CimType.Boolean, typeof (bool)},
            {System.Management.CimType.Char16, typeof (string)},
            {System.Management.CimType.DateTime, typeof (DateTime)},
            {System.Management.CimType.Object, typeof (object)},
            {System.Management.CimType.Real32, typeof (decimal)},
            {System.Management.CimType.Real64, typeof (decimal)},
            {System.Management.CimType.Reference, typeof (object)},
            {System.Management.CimType.SInt16, typeof (short)},
            {System.Management.CimType.SInt32, typeof (int)},
            {System.Management.CimType.SInt8, typeof (sbyte)},
            {System.Management.CimType.String, typeof (string)},
            {System.Management.CimType.UInt8, typeof (byte)},
            {System.Management.CimType.UInt16, typeof (ushort)},
            {System.Management.CimType.UInt32, typeof (uint)},
            {System.Management.CimType.UInt64, typeof (ulong)}
        };

        public static Type Cim2SystemType(this System.Management.PropertyData data)
        {
            Type type = Cim2TypeTable[data.Type];
            if (data.IsArray)
                type = type.MakeArrayType();
            return type;
        }

        public static object Cim2SystemValue(this System.Management.PropertyData data)
        {
            Type type = Cim2SystemType(data);
            if (data.Type == System.Management.CimType.DateTime)
                return DateTime.ParseExact(data.Value.ToString(), "yyyyMMddHHmmss.ffffff-000", System.Globalization.CultureInfo.InvariantCulture);
            return Convert.ChangeType(data.Value, type);
        }


        public static System.Data.DataTable Select(string sql)
        {
            System.Management.WqlObjectQuery wqlQuery = new System.Management.WqlObjectQuery(sql);
            System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher(wqlQuery);
            System.Data.DataTable result = new System.Data.DataTable();

            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();



            foreach (System.Management.ManagementObject mo in searcher.Get())
            {
                System.Data.DataRow row = result.NewRow();
                foreach (System.Management.PropertyData prop in mo.Properties)
                {

                    if (!result.Columns.Contains(prop.Name))
                    {
                        result.Columns.Add(new System.Data.DataColumn(prop.Name, Cim2SystemType(prop)));
                    }

                    if (prop.Value == null)
                    {
                        row[prop.Name] = System.DBNull.Value;
                    }
                    else
                    {
                        row[prop.Name] = prop.Value;
                    }
                }
                result.Rows.Add(row);
            }
            // System.Diagnostics.Debug.WriteLine(js.Serialize(result));
            return result;
        }
    }
}
