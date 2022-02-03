using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace RAMMS.MobileApps
{
    public enum EnclosedType
    {
        None,
        Array,
        List,
        ObservableCollection
    }

    public class ManyToManyMetaInfo
    {
        public Type IntermediateType { get; set; }

        public PropertyInfo OriginProperty { get; set; }

        public PropertyInfo DestinationProperty { get; set; }
    }

    public static class ReflectionExtensions
    {
        public static T GetAttribute<T>(this Type type) where T : Attribute
        {
            T attribute = null;
            var attributes = (T[])type.GetTypeInfo().GetCustomAttributes(typeof(T), true);
            if (attributes.Length > 0)
            {
                attribute = attributes[0];
            }
            return attribute;
        }

        public static T GetAttribute<T>(this PropertyInfo property) where T : Attribute
        {
            T attribute = null;
            var attributes = (T[])property.GetCustomAttributes(typeof(T), true);
            if (attributes.Length > 0)
            {
                attribute = attributes[0];
            }
            return attribute;
        }

        public static Type GetEntityType(this PropertyInfo property, out EnclosedType enclosedType)
        {
            var type = property.PropertyType;
            enclosedType = EnclosedType.None;

            var typeInfo = type.GetTypeInfo();
            if (type.IsArray)
            {
                type = type.GetElementType();
                enclosedType = EnclosedType.Array;
            }
            else if (typeInfo.IsGenericType && typeof(List<>).GetTypeInfo().IsAssignableFrom(type.GetGenericTypeDefinition().GetTypeInfo()))
            {
                type = typeInfo.GenericTypeArguments[0];
                enclosedType = EnclosedType.List;
            }
            else if (typeInfo.IsGenericType && typeof(ObservableCollection<>).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo().GetGenericTypeDefinition().GetTypeInfo()))
            {
                type = typeInfo.GenericTypeArguments[0];
                enclosedType = EnclosedType.ObservableCollection;
            }
            return type;
        }

        public static object GetDefault(this Type type)
        {
            return type.GetTypeInfo().IsValueType ? Activator.CreateInstance(type) : null;
        }

        public static PropertyInfo GetProperty<T>(Expression<Func<T, object>> expression)
        {
            var type = typeof(T);
            var body = expression.Body as MemberExpression;
            Debug.Assert(body != null, "Expression should be a property member expression");

            var propertyName = body.Member.Name;
            return type.GetRuntimeProperty(propertyName);
        }

        public static List<PropertyInfo> GetTextBlobProperties(this Type type)
        {
            return (from property in type.GetRuntimeProperties()
                    where property.IsPublicInstance() && property.GetAttribute<TextBlobAttribute>() != null
                    select property).ToList();
        }

        public static string GetTableName(this Type type)
        {
            var tableName = type.Name;
            var tableAttribute = type.GetAttribute<TableAttribute>();
            if (tableAttribute != null && tableAttribute.Name != null)
                tableName = tableAttribute.Name;

            return tableName;
        }

        public static string GetColumnName(this PropertyInfo property)
        {
            var column = property.Name;
            var columnAttribute = property.GetAttribute<ColumnAttribute>();
            if (columnAttribute != null && columnAttribute.Name != null)
                column = columnAttribute.Name;

            return column;
        }

        private static bool IsPublicInstance(this PropertyInfo propertyInfo)
        {
            return propertyInfo != null &&
                ((propertyInfo.GetMethod != null && !propertyInfo.GetMethod.IsStatic && propertyInfo.GetMethod.IsPublic) &&
                    (propertyInfo.SetMethod != null && !propertyInfo.SetMethod.IsStatic && propertyInfo.SetMethod.IsPublic));
        }
    }
}