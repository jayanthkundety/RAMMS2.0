using Acr.UserDialogs;
using SQLite;
using System;
using System.IO;

namespace RAMMS.MobileApps.Droid
{
    public class SQLiteFactoryDroid : ISQLiteFactory
    {
        public SQLiteConnectionWithLock GetConnectionWithLock()
        {
            try
            {
                return new SQLiteConnectionWithLock(
                    new SQLiteConnectionString(GetSqliteDbPath(), true, null)
                    );
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message);
            }
            return null;
        }

        public string GetSqliteDbPath()
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return path = Path.Combine(path, "ramms.db3");
        }
    }
}