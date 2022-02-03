using RAMMS.Common;
using RAMMS.DTO.JQueryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace RAMMS.Repository
{
    public static class EFExtension
    {
        public static IOrderedQueryable<TSource> Order<TSource>(this IQueryable<TSource> query, DataTableAjaxPostModel searchData, IOrderedQueryable<TSource> defaultOrder)
        {
            IOrderedQueryable<TSource> orderQuery = null;
            if (searchData != null && searchData.order != null && searchData.order.Count > 0)
            {
                //query.OrderByDescending()                
                foreach (Order order in searchData.order)
                {
                    if (order.dir == "asc")
                    {
                        if (orderQuery == null) { orderQuery = query.OrderBy(searchData.columns[order.column].data); }
                        else { orderQuery = orderQuery.ThenBy(searchData.columns[order.column].data); }
                    }
                    else
                    {
                        if (orderQuery == null) { orderQuery = query.OrderByDescending(searchData.columns[order.column].data); }
                        else { orderQuery = orderQuery.ThenByDescending(searchData.columns[order.column].data); }
                    }
                }
                orderQuery = orderQuery ?? defaultOrder;
            }
            else
            {
                orderQuery = defaultOrder;
            }
            return orderQuery;
        }
        public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> query, string orderByColumnName)
        {
            return CustomOrderBY(query, orderByColumnName, "OrderBy", null);
        }
        public static IOrderedQueryable<TSource> ThenBy<TSource>(this IOrderedQueryable<TSource> query, string orderByColumnName)
        {
            return CustomOrderBY(query, orderByColumnName, "ThenBy", null);
        }
        public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> query, string orderByColumnName, Type matchingType)
        {
            return CustomOrderBY(query, orderByColumnName, "OrderBy", matchingType);
        }
        public static IOrderedQueryable<TSource> OrderByDescending<TSource>(this IQueryable<TSource> query, string orderByColumnName)
        {
            return CustomOrderBY(query, orderByColumnName, "OrderByDescending", null);
        }
        public static IOrderedQueryable<TSource> ThenByDescending<TSource>(this IOrderedQueryable<TSource> query, string orderByColumnName)
        {
            return CustomOrderBY(query, orderByColumnName, "ThenByDescending", null);
        }
        public static IOrderedQueryable<TSource> OrderByDescending<TSource>(this IQueryable<TSource> query, string orderByColumnName, Type matchingType)
        {
            return CustomOrderBY(query, orderByColumnName, "OrderByDescending", matchingType);
        }
        private static IOrderedQueryable<TSource> CustomOrderBY<TSource>(IQueryable<TSource> query, string orderByColumnName, string type, Type matchingType)
        {
            orderByColumnName = matchingType == null ? orderByColumnName : GetMapName(matchingType, orderByColumnName);
            var entityType = typeof(TSource);
            var propertyInfo = entityType.GetProperty(orderByColumnName);
            ParameterExpression arg = Expression.Parameter(entityType, "param");
            MemberExpression property = Expression.Property(arg, orderByColumnName);
            var selector = Expression.Lambda(property, new ParameterExpression[] { arg });
            var enumarableType = typeof(System.Linq.Queryable);
            var method = enumarableType.GetMethods()
                 .Where(m => m.Name == type && m.IsGenericMethodDefinition)
                 .Where(m =>
                 {
                     var parameters = m.GetParameters().ToList();
                     return parameters.Count == 2;
                 }).Single();
            MethodInfo genericMethod = method.MakeGenericMethod(entityType, propertyInfo.PropertyType);
            var newQuery = (IOrderedQueryable<TSource>)genericMethod.Invoke(genericMethod, new object[] { query, selector });
            return newQuery;
        }
        private static IOrderedQueryable<TSource> CustomOrderBY<TSource>(IOrderedQueryable<TSource> query, string orderByColumnName, string type, Type matchingType)
        {
            orderByColumnName = matchingType == null ? orderByColumnName : GetMapName(matchingType, orderByColumnName);
            var entityType = typeof(TSource);
            var propertyInfo = entityType.GetProperty(orderByColumnName);
            ParameterExpression arg = Expression.Parameter(entityType, "param");
            MemberExpression property = Expression.Property(arg, orderByColumnName);
            var selector = Expression.Lambda(property, new ParameterExpression[] { arg });
            var enumarableType = typeof(System.Linq.Queryable);
            var method = enumarableType.GetMethods()
                 .Where(m => m.Name == type && m.IsGenericMethodDefinition)
                 .Where(m =>
                 {
                     var parameters = m.GetParameters().ToList();
                     return parameters.Count == 2;
                 }).Single();
            MethodInfo genericMethod =  method.MakeGenericMethod(entityType, propertyInfo.PropertyType);
            var newQuery = (IOrderedQueryable<TSource>)genericMethod.Invoke(genericMethod, new object[] { query, selector });
            return newQuery;
        }
        public static string GetMapName(Type type, string name)
        {
            string strName = name.ToLower();
            var prop = type.GetProperties();
            var info = prop.Where(x => x.Name.ToLower() == strName).FirstOrDefault();
            if (info != null)
            {
                object objProp = info.GetCustomAttributes(true).Where(X => X.GetType() == typeof(AutoMapper.Configuration.Conventions.MapToAttribute)).FirstOrDefault();
                if (objProp != null)
                    return ((AutoMapper.Configuration.Conventions.MapToAttribute)objProp).MatchingName;
                else
                    return name;
            }
            else
                return name;
        }
        public static IQueryable<T> WhereEquals<T>(this IQueryable<T> source, string propertyName, object value)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return source;
            }
            ParameterExpression parameter = Expression.Parameter(typeof(T), "param");
            Expression whereProperty = Expression.Property(parameter, propertyName);
            switch (whereProperty.Type.Name)
            {
                case "Int32":
                    value = Utility.ToInt(value);
                    break;
                case "Nullable`1":
                    value = Utility.ToNullInt(value);
                    break;
                case "Int64":
                    value = Utility.ToLong(value);
                    break;
                case "Boolean":
                    value = Utility.ToBool(value);
                    break;
            }

            Expression constant = Expression.Constant(value);
            Expression condition = Expression.Equal(whereProperty, constant);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(condition, parameter);
            return source.Where(lambda);
        }


    }
}
