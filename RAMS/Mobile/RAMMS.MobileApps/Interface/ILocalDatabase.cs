using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RAMMS.MobileApps
{
    public interface ILocalDatabase
    {
        Task<List<T>> GetAll<T>() where T : new();

        Task CreateTable<T>() where T : new();

        Task CreateTables(Type[] types);

        Task<T> QuerySingle<T>(Expression<Func<T, bool>> query) where T : new();

        Task<List<T>> QueryList<T>(Expression<Func<T, bool>> query) where T : new();

        Task<List<T>> QueryList<T>(string query, params object[] args) where T : new();

        Task<int> InsertOrReplaceAllAsync(IEnumerable items);

        Task<int> InsertOrReplaceAsync(object item);

        Task<int> Insert(object item);

        Task<int> Update(object item);

        Task<int> Delete<T>(object primaryKey);

        void ClearTable<T>() where T : new();
    }
}