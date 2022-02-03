using RAMMS.Common;
using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> Search(Expression<Func<T, bool>> exp);
        Task<T> GetByIdAsync(int id);
        T GetById(int id);
        Task<ICollection<T>> GetAllAsync();
        Task<ICollection<T>> GetAllAsyncNoTracking();
        T Find(Expression<Func<T, bool>> expression);
        Task<T> FindAsync(Expression<Func<T, bool>> expression);
        Task<T> FindAsyncNoTracking(Expression<Func<T, bool>> expression);
        ICollection<T> FindAll(Expression<Func<T, bool>> expression);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> expression);
        Task<ICollection<T>> FindAllAsyncNoTracking(Expression<Func<T, bool>> expression);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsyncNoTracking(Expression<Func<T, bool>> predicate);        
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> expression);
        Task<int> SaveAsync();
        IQueryable<T> GetAll();
        void Create(T entity);

        //ToDo
        T CreateReturnEntity(T entity);
        void Update(T entity);
        void Update(IEnumerable<T> entity);
        void Create(IEnumerable<T> entity);
        DataSet GetDataSet(string sql, CommandType commandType, Dictionary<string, object> inparams);
        SqlDataset GetDataSet(string sql, CommandType commandType, Dictionary<string, object> inparams, Dictionary<string, object> outparams);
        Task<ICollection<TResult>> FindAsync<TResult>(Expression<Func<T, bool>> condition, Expression<Func<T, TResult>> selector);
    }
}
