using AutoMapper;
using Business.Shared.Dx.Filter;
using Business.Shared.Dx.Search;
using Dx.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Business
{
	public class QueryHelper
	{
		IMapper _objectMapper;
		public QueryHelper(IMapper objectMapper)
		{

			_objectMapper = objectMapper;
		}


		private Expression<Func<T, T>> _dynamicSelectGenerator<T>(string Fields = "")
		{
			string[] EntityFields;
			if (Fields == "")
				// get Properties of the T
				EntityFields = typeof(T).GetProperties().Select(propertyInfo => propertyInfo.Name).ToArray();
			else
				EntityFields = Fields.Split(',');

			// input parameter "o"
			var xParameter = Expression.Parameter(typeof(T), "o");

			// new statement "new Data()"
			var xNew = Expression.New(typeof(T));

			// create initializers
			var bindings = EntityFields.Select(o => o.Trim())
				.Select(o =>
				{

					// property "Field1"
					var mi = typeof(T).GetProperty(o);

					// original value "o.Field1"
					var xOriginal = Expression.Property(xParameter, mi);

					// set value "Field1 = o.Field1"
					return Expression.Bind(mi, xOriginal);
				}
			);

			// initialization "new Data { Field1 = o.Field1, Field2 = o.Field2 }"
			var xInit = Expression.MemberInit(xNew, bindings);

			// expression "o => new Data { Field1 = o.Field1, Field2 = o.Field2 }"
			var lambda = Expression.Lambda<Func<T, T>>(xInit, xParameter);
			return lambda;
			// compile to Func<Data, Data>
			//return lambda.Compile();
		}



		public async Task<(List<K> list, int count)> GetSearchResult<T, K>(IQueryable<T> query, DxSearchInput searchInput)
		{
			var predicate = new JsonExpressionParser().ParsePredicateOfDxSearch<T>(searchInput);

			var filteredQuery = query.WhereIf<T>(predicate != null, predicate);
			var itemCount = filteredQuery.Count();

			List<DxSort> sorts = new List<DxSort>();
			if (!string.IsNullOrEmpty(searchInput.SearchExpr))
			{
				sorts.Add(new DxSort() { Selector = searchInput.SearchExpr, Desc = false });
			}

			var sortedQuery = LinqExtensions.ApplySorting<T>(filteredQuery, sorts);
			var pagedQuery = LinqExtensions.ApplyPaging<T>(sortedQuery, searchInput.Skip, searchInput.Take);

#if (DEBUG)
			var queryStr = pagedQuery.ToQueryString();
#endif
			var result = await pagedQuery.Select(s => _objectMapper.Map<K>(s)).ToListAsync();

			return (result, itemCount);
		}


		public async Task<(List<K> list, int count)> GetSearchResult<T, K>(IQueryable<T> query, string[] fields, int skip, int take, Expression<Func<T, bool>> searchExpression)
		{
			var filteredQuery = query.WhereIf<T>(searchExpression != null, searchExpression);

			var itemCount = filteredQuery.Count();

			List<DxSort> sorts = new List<DxSort>();

			foreach (var field in fields)
			{

				sorts.Add(new DxSort() { Selector = field, Desc = false });
			}

			var sortedQuery = LinqExtensions.ApplySorting<T>(filteredQuery, sorts);
			var pagedQuery = LinqExtensions.ApplyPaging<T>(sortedQuery, skip, take);

			var result = await pagedQuery.Select(s => _objectMapper.Map<K>(s)).ToListAsync();

			return (result, itemCount);
		}

		public async Task<(List<T> list, int count)> GetFilterResult<T>(IQueryable<T> query, DxFilterInput filterInput)
		{
			var predicate = new JsonExpressionParser().ParsePredicateOfDxSearch<T>(filterInput);

			var filteredQuery = query.WhereIf<T>(predicate != null, predicate);
			var itemCount = filteredQuery.Count();

			var sortedQuery = LinqExtensions.ApplySorting<T>(filteredQuery, filterInput.Sort);
			var pagedQuery = LinqExtensions.ApplyPaging<T>(sortedQuery, filterInput.Skip, filterInput.Take);

			var items = await pagedQuery.Select(e => e).ToListAsync();
			return (items, itemCount);
		}

		public async Task<(List<K> list, int count)> GetFilterResult<T, K>(IQueryable<T> query, DxFilterInput filterInput)
		{
			var predicate = new JsonExpressionParser().ParsePredicateOfDxSearch<T>(filterInput);

			var filteredQuery = query.WhereIf<T>(predicate != null, predicate);
			var itemCount = filteredQuery.Count();

			var sortedQuery = LinqExtensions.ApplySorting<T>(filteredQuery, filterInput.Sort);
			var pagedQuery = LinqExtensions.ApplyPaging<T>(sortedQuery, filterInput.Skip, filterInput.Take);

			var items = await pagedQuery.Select(s => _objectMapper.Map<K>(s)).ToListAsync();
			return (items, itemCount);
		}

		public async Task<List<object>> GetDistinctFilterResult<T>(IQueryable<T> query, string dataField)
		{
			var items = await query.Select(_dynamicSelectGenerator<T>(dataField)).Distinct().ToListAsync();
			List<object> list = new List<object>();
			foreach(T item in items)
			{
				list.Add(item.GetType().GetProperty(dataField).GetValue(item));
			}

			return list;
		}

	}
}
