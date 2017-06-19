using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MCS.DataAccess
{
    public static class ExtensionMethods
    {

        /// <summary>
        /// To Convert Dapper's dynamic data object to Datable
        /// </summary>        
        public static DataTable ToDataTable(IEnumerable<object> data)
        {
            DataTable dt = new DataTable();
            DataRow dr;
            foreach (IDictionary<string, object> item in data)
            {
                if (dt.Columns.Count != item.Keys.Count)
                {
                    foreach (var col in item.Keys) dt.Columns.Add(col);
                }
                dr = dt.NewRow();
                for (int i = 0; i < item.Values.Count; i++)
                {
                    dr[i] = item.Values.ElementAt(i);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
