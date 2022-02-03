using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;

namespace RAMMS.Repository
{
    public static class Utilities
    {

        public static List<T> ToObject<T>(this DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                       .Select(c => c.ColumnName)
                       .ToList();

            var properties = typeof(T).GetProperties();

            List<T> returnList = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    try
                    {
                        if (columnNames.Contains(pro.Name) && row[pro.Name] != DBNull.Value)
                        {
                            pro.SetValue(objT, Convert.ChangeType(row[pro.Name], pro.PropertyType));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException(string.Format("Error on: {0}", pro.Name), ex);
                    }
                }

                returnList.Add(objT);
            }

            return returnList;
        }


    }
}
