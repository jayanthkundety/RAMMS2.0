using System;
using System.Collections.Generic;

namespace RAMMS.MobileApps
{
    public class LocalDataSetup
    {
        public static void CreateTables(ISQLiteFactory iSQLiteFactory)
        {
            var conn = iSQLiteFactory.GetConnectionWithLock();
            try
            {
             //   conn.CreateTable<ICollection<RmUserGroup>>();
                conn.CreateTable<RmUsers>();


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}