using Business.Shared.Dx.Filter;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Shared.Entities;
using Business.Shared;
using Core.Constant;
using Core.Security;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Business
{
    

    public static class LinqExtensions
    {
        //public static IQueryable<TEntity> ApplySorting<TEntity>(this IQueryable<TEntity> source, List<dtoDxSort> sortList, Dictionary<string, string> joinedTableFields)
        //{
        //    var sort = sortList != null ? sortList[0] : null;

        //    if (sort != null)
        //    {
        //        string command = sort.Desc ? "OrderByDescending" : "OrderBy";
        //        var type = typeof(TEntity);
        //        var property = type.GetProperty(sort.Selector, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        //        var parameter = Expression.Parameter(type, "p");
        //        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        //        var orderByExpression = Expression.Lambda(propertyAccess, parameter);
        //        var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
        //                                      source.Expression, Expression.Quote(orderByExpression));
        //        return source.Provider.CreateQuery<TEntity>(resultExpression);
        //    }


        //    return source;
        //}

        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> source, List<DxSort> sortModels)
        {
            if (sortModels == null)
                return source;

            var expression = source.Expression;
            int count = 0;
            foreach (var item in sortModels)
            {
                var parameter = Expression.Parameter(typeof(T), "x");

                Expression selector = null;

                //var field = item.Selector;


                if (item.Selector.Contains("."))
                {
                    //item.Selector = joinedTableFields[item.Selector];

                    selector = item.Selector.Split('.').Aggregate((Expression)parameter, Expression.Property);
                }
                else
                {
                    selector = Expression.Property(parameter, item.Selector);
                }

                //var selector = Expression.Property(parameter, item.Selector);
                var method = item.Desc ?
                    (count == 0 ? "OrderByDescending" : "ThenByDescending") :
                    (count == 0 ? "OrderBy" : "ThenBy");
                //var method = string.Equals(item.Desc, "desc", StringComparison.OrdinalIgnoreCase) ?
                //    (count == 0 ? "OrderByDescending" : "ThenByDescending") :
                //    (count == 0 ? "OrderBy" : "ThenBy");
                expression = Expression.Call(typeof(Queryable), method,
                    new Type[] { source.ElementType, selector.Type },
                    expression, Expression.Quote(Expression.Lambda(selector, parameter)));
                count++;
            }
            return count > 0 ? source.Provider.CreateQuery<T>(expression) : source;
        }

        public static IQueryable<TEntity> ApplyPaging<TEntity>(this IQueryable<TEntity> source, int skip, int take)
        {
            // 1 den küçük bir take değeri, sorting uygulamıyorum anlamına gelmektedir..
            if (take < 1)
                return source;

            var query = source.Skip(skip).Take(take);

            return query;
        }

        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

		


		public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            if (left == null) return right;
            var and = Expression.AndAlso(left.Body, right.Body);
            return Expression.Lambda<Func<T, bool>>(and, left.Parameters.Single());
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            if (left == null) return right;
            var and = Expression.OrElse(left.Body, right.Body);
            return Expression.Lambda<Func<T, bool>>(and, left.Parameters.Single());
        }

        //public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, int, bool> predicate)
        //{
        //    if (condition)
        //        return source.Where(predicate);
        //    else
        //        return source;
        //}

        //public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        //{
        //    if (condition)
        //        return source.Where(predicate);
        //    else
        //        return source;
        //}



        public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            using var enumerator = query.Provider.Execute<IEnumerable<TEntity>>(query.Expression).GetEnumerator();
            var relationalCommandCache = enumerator.Private("_relationalCommandCache");
            var selectExpression = relationalCommandCache.Private<SelectExpression>("_selectExpression");
            var factory = relationalCommandCache.Private<IQuerySqlGeneratorFactory>("_querySqlGeneratorFactory");

            var sqlGenerator = factory.Create();
            var command = sqlGenerator.GetCommand(selectExpression);

            string sql = command.CommandText;
            return sql;
        }

        private static object Private(this object obj, string privateField) => obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);
        private static T Private<T>(this object obj, string privateField) => (T)obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);
        public static void DetachLocal<T>(this DbContext _context, T t, string entryId) where T : class, IEntity
        {

            /* //use as below:
                _context.DetachLocal(tmodel, id);
                _context.SaveChanges();
             */

            var local = _context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entryId));

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            _context.Entry(t).State = EntityState.Modified;
        }




    }

    
}
