using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RAMMS.MobileApps
{
    public class LocalDatabase : ILocalDatabase
    {
        private readonly SQLiteAsyncConnection _asyncConn;

        private readonly SQLiteConnection _conn;

        private ISQLiteFactory _factory;

        public LocalDatabase(ISQLiteFactory factory)
        {
            SQLiteConnectionWithLock connWithLock = factory.GetConnectionWithLock();
            _asyncConn = new SQLiteAsyncConnection(factory.GetSqliteDbPath(), true);
            _factory = factory;
        }

        public async Task CreateTable<T>() where T : new()
        {
            try
            {
                await _asyncConn.CreateTableAsync<T>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public async Task CreateTables(Type[] types)
        {
            try
            {
                await _asyncConn.CreateTablesAsync(CreateFlags.None, types);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public async Task<T> QuerySingle<T>(Expression<Func<T, bool>> query) where T : new()
        {
            var item = await _asyncConn.Table<T>().Where(query).FirstOrDefaultAsync();
            TextBlobOperations.GetTextBlobs(item);
            return item;
        }

        public async Task<List<T>> QueryList<T>(Expression<Func<T, bool>> query) where T : new()
        {
            var list = await _asyncConn.Table<T>().Where(query).ToListAsync();
            foreach (var item in list)
            {
                TextBlobOperations.GetTextBlobs(item);
            }
            return list;
        }

        public async Task<List<T>> QueryList<T>(string query, params object[] args) where T : new()
        {
            var list = (await _asyncConn.QueryAsync<T>(query, args));
            foreach (var item in list)
            {
                TextBlobOperations.GetTextBlobs(item);
            }
            return list;
        }

        public async Task<List<T>> GetAll<T>() where T : new()
        {
            var list = (await _asyncConn.Table<T>().ToListAsync());
            foreach (var item in list)
            {
                TextBlobOperations.GetTextBlobs(item);
            }
            return list;
        }

        public async Task<int> InsertOrReplaceAllAsync(IEnumerable items)
        {
            foreach (var item in items)
            {
                TextBlobOperations.UpdateTextBlobs(item);
            }
            var insert = await _asyncConn.InsertOrReplaceAsync(items);
            return insert;
        }

        public async Task<int> InsertOrReplaceAsync(object item)
        {
            TextBlobOperations.UpdateTextBlobs(item);
            var insert = await _asyncConn.InsertOrReplaceAsync(item);
            return insert;
        }

        public async Task<int> Insert(object item)
        {
            TextBlobOperations.UpdateTextBlobs(item);
            var insert = await _asyncConn.InsertOrReplaceAsync(item);
            return insert;
        }

        public async Task<int> Update(object item)
        {
            TextBlobOperations.UpdateTextBlobs(item);
            var update = await _asyncConn.UpdateAsync(item);
            return update;
        }

        public async Task<int> Delete<T>(object primaryKey)
        {
            return await _asyncConn.DeleteAsync<T>(primaryKey);
        }

        public void ClearTable<T>() where T : new()
        {
            var conn = _factory.GetConnectionWithLock();
            conn.DropTable<T>();
            conn.CreateTable<T>();
        }
    }
}