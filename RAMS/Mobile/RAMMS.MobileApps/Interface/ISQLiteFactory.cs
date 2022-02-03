namespace RAMMS.MobileApps
{
    public interface ISQLiteFactory
    {
        SQLite.SQLiteConnectionWithLock GetConnectionWithLock();

        string GetSqliteDbPath();
    }
}