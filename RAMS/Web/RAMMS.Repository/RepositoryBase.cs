using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using RAMMS.Common;
using System.Reflection.Metadata;
using RAMMS.Repository.Interfaces;
using System.Threading;
using RAMMS.Repository;

namespace RAMS.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public RAMMSContext _context { get; set; }      

        public RepositoryBase(RAMMSContext context)
        {
            _context = context;
        }
        public IQueryable<T> Search(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().Where(exp);
        }

        public virtual T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async virtual Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public async virtual Task<ICollection<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async virtual Task<ICollection<T>> GetAllAsyncNoTracking()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public virtual T Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().SingleOrDefault(expression);
        }

        public async virtual Task<T> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(expression);
        }

        public async virtual Task<T> FindAsyncNoTracking(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(expression);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<ICollection<TResult>> FindAsync<TResult>(Expression<Func<T, bool>> condition, Expression<Func<T, TResult>> selector)
        {
            if (condition != null)
                return await _context.Set<T>().Where(condition).Select(selector).ToListAsync();
            else
                return await _context.Set<T>().Select(selector).ToListAsync();            
        }

        public async Task<ICollection<T>> FindAllAsyncNoTracking(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AsNoTracking().Where(expression).ToListAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual async Task<ICollection<T>> FindByAsyncNoTracking(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        //ToDo
        public T CreateReturnEntity(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public void Create(IEnumerable<T> entity)
        {
            _context.Set<T>().AddRange(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry<T>(entity).State = EntityState.Modified;
        }

        public virtual void Update(IEnumerable<T> entity)
        {
            foreach (T item in entity)
            {
                Update(item);
            }
        }

        public virtual void Delete(Expression<Func<T, bool>> exp)
        {
            _context.Set<T>().RemoveRange(Search(exp));
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async virtual Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public DataSet GetDataSet(string sql, CommandType commandType, Dictionary<string, object> inparams)
        {
            return GetDataSet(sql, commandType, inparams, null).dataSet;
        }

        public SqlDataset GetDataSet(string sql, CommandType commandType, Dictionary<string, object> inparams, Dictionary<string, object> outparams)
        {
            var result = new DataSet();
            SqlDataset sqlDataset = new SqlDataset();

            var context = _context;
            {
                var connection = context.Database.GetDbConnection();

                var cmd = connection.CreateCommand();
                cmd.CommandType = commandType;
                cmd.CommandText = sql;
                cmd.CommandTimeout = 2000;
                if (inparams != null)
                {
                    foreach (var pr in inparams)
                    {
                        var p = cmd.CreateParameter();
                        p.ParameterName = pr.Key;
                        p.Value = pr.Value;
                        cmd.Parameters.Add(p);
                    }
                }
                if (outparams != null)
                {
                    foreach (var pr in outparams)
                    {
                        var p = cmd.CreateParameter();
                        p.ParameterName = pr.Key;
                        p.Value = pr.Value;
                        p.Size = -1;
                        p.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(p);
                    }
                }
                try
                {
                    connection.Open();
                    var reader = cmd.ExecuteReader();
                    do
                    {
                        var tb = new DataTable();
                        tb.Load(reader);
                        result.Tables.Add(tb);
                        sqlDataset.dataSet = result;
                    } while (!reader.IsClosed);

                    Dictionary<string, object> returnparam = new Dictionary<string, object>();
                    if (outparams != null)
                    {
                        foreach (var op in outparams)
                        {
                            for (int i = 0; i < cmd.Parameters.Count; i++)
                            {
                                if (cmd.Parameters[i].ParameterName == op.Key)
                                {
                                    returnparam.Add(op.Key, cmd.Parameters[i].Value);
                                    break;
                                }
                            }
                        }
                    }

                    sqlDataset.outparam = returnparam;

                }
                catch (Exception e)
                {

                }
                finally
                {
                    connection.Close();
                }

            }

            return sqlDataset;
        }
    }
}
