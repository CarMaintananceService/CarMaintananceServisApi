
using Business.Shared.Dx.Filter;
using Business.Shared.Dx.Search;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;



namespace Dx.Common
{
    public class JsonExpressionParser
    {
        private const string StringStr = "string";

        private readonly string BooleanStr = nameof(Boolean).ToLower();
        private readonly string Number = nameof(Number).ToLower();
        private readonly string In = nameof(In).ToLower();
        private readonly string And = nameof(And).ToLower();
        private readonly string GreaterThan = nameof(GreaterThan).ToLower();
        private readonly string LessThan = nameof(LessThan).ToLower();
        private readonly string LessThanOrEqual = nameof(LessThanOrEqual).ToLower();
        private readonly string GreaterThanOrEqual = nameof(GreaterThanOrEqual).ToLower();
        private readonly string StartsWith = nameof(StartsWith).ToLower();
        private readonly string EndsWith = nameof(EndsWith).ToLower();
        private readonly string NotEqual = nameof(NotEqual).ToLower();
        private readonly string Contains = nameof(Contains).ToLower();
        private readonly string NotContains = nameof(NotContains).ToLower();

        private readonly MethodInfo MethodContains = typeof(Enumerable).GetMethods(
                        BindingFlags.Static | BindingFlags.Public)
                        .Single(m => m.Name == nameof(Enumerable.Contains)
                            && m.GetParameters().Length == 2);

        private delegate Expression Binder(Expression left, Expression right);

        private Expression ParseTree<T>(
            JsonElement condition,
            ParameterExpression parm)
        {
            Expression left = null;
            var gate = condition.GetProperty(nameof(condition)).GetString();

            JsonElement rules = condition.GetProperty(nameof(rules));

            Binder binder = gate == And ? (Binder)Expression.And : Expression.Or;

            Expression bind(Expression left, Expression right) =>
                left == null ? right : binder(left, right);

            try
            {

                foreach (var rule in rules.EnumerateArray())
                {
                    if (rule.TryGetProperty(nameof(condition), out JsonElement check))
                    {
                        var right = ParseTree<T>(rule, parm);
                        left = bind(left, right);
                        continue;
                    }

                    string @operator = rule.GetProperty(nameof(@operator)).GetString();
                    string type = rule.GetProperty(nameof(type)).GetString();
                    string field = rule.GetProperty(nameof(field)).GetString();

                    JsonElement value = rule.GetProperty(nameof(value));

                    Expression property = null;

                    if(field.Contains("."))
                    {
                        property = field.Split('.').Aggregate((Expression)parm, Expression.Property);
                    }
                    else
                    {
                        property = Expression.Property(parm, field);
                    }

                    if (@operator == In)
                    {
                        var contains = MethodContains.MakeGenericMethod(typeof(string));
                        object val = value.EnumerateArray().Select(e => e.GetString()).ToList();
                        var right = Expression.Call(
                            contains,
                            Expression.Constant(val),
                            property);
                        left = bind(left, right);
                    }
                    else if (@operator == GreaterThan)
                    {
                        object val = GetValue(value, type, property.Type);
                        var toCompare = Expression.Constant(val);
                        var right = CustomGreaterThan(property, toCompare);
                        //var right = Expression.GreaterThan(property, toCompare);
                        left = bind(left, right);
                    }
                    else if (@operator == GreaterThanOrEqual)
                    {
                        object val = GetValue(value, type, property.Type);
                        var toCompare = Expression.Constant(val);
                        var right = CustomGreaterThanOrEqual(property, toCompare);
                        //var right = Expression.GreaterThanOrEqual(property, toCompare);
                        left = bind(left, right);
                    }
                    else if (@operator == LessThan)
                    {
                        object val = GetValue(value, type, property.Type);
                        var toCompare = Expression.Constant(val);
                        var right = CustomLessThan(property, toCompare);
                        //var right = Expression.LessThan(property, toCompare);
                        left = bind(left, right);
                    }
                    else if (@operator == LessThanOrEqual)
                    {
                        object val = GetValue(value, type, property.Type);
                        var toCompare = Expression.Constant(val);
                        var right = CustomLessThanOrEqual(property, toCompare);
                        //var right = Expression.LessThanOrEqual(property, toCompare);
                        left = bind(left, right);
                    }
                    else if (@operator == StartsWith)
                    {
                        object val = GetValue(value, type, property.Type);

                        MethodInfo StartsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });

                        var toCompare = Expression.Constant(val);

                        var right = Expression.Call(property, StartsWithMethod, toCompare);

                        left = bind(left, right);
                    }
                    else if (@operator == EndsWith)
                    {
                        object val = GetValue(value, type, property.Type);

                        MethodInfo EndsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });

                        var toCompare = Expression.Constant(val);

                        var right = Expression.Call(property, EndsWithMethod, toCompare);

                        left = bind(left, right);
                    }
                    else if (@operator == Contains)
                    {
                        object val = GetValue(value, type, property.Type);

                        MethodInfo ContainsWithMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

                        var toCompare = Expression.Constant(val);

                        var right = Expression.Call(property, ContainsWithMethod, toCompare);

                        left = bind(left, right);
                    }
                    else if (@operator == NotContains)
                    {
                        object val = GetValue(value, type, property.Type);

                        MethodInfo ContainsWithMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

                        var toCompare = Expression.Constant(val);

                        var right = Expression.Not(Expression.Call(property, ContainsWithMethod, toCompare));

                        left = bind(left, right);
                    }
                    else if (@operator == NotEqual)
                    {
                        //Expression.Convert(enumMember, typeof(int));
						object val = GetValue(value, type, property.Type);
                        var toCompare = Expression.Constant(val);
                        var right = Expression.NotEqual(property, toCompare);
                        left = bind(left, right);
                    }
					else
					{
						if(property.Type.IsEnum)
                        {
							object val = value.GetInt32();
							var toCompare = Expression.Constant(val);
							var right = Expression.Equal(Expression.Convert(property, typeof(int)), toCompare);
							left = bind(left, right);
						}
                        else
                        {
							object val = GetValue(value, type, property.Type);
                            var toCompare = Expression.Constant(val);
                            var right = Expression.Equal(Expression.Convert(property, IsNullableType(property.Type) ? Nullable.GetUnderlyingType(property.Type) : property.Type), toCompare);
                            left = bind(left, right);
                        }
					}
					//else
					//{
					//    object val = GetValue(value, type, property.Type);
					//    var toCompare =  Expression.Constant(val);
					//    var right = Expression.Equal(property, toCompare);
					//    left = bind(left, right);
					//}
				}
            }
            catch (Exception e)
            {

                throw e;
            }


            return left;
        }



		private object GetValue(JsonElement value, string type, Type propType)
        {
            if (IsNullableType(propType))
                type = Nullable.GetUnderlyingType(propType).Name.ToLower();


			object val = null;
            switch(type)
            {
                case "integer":
                case "ınteger":
                    if (propType == typeof(Int16))
                        val = value.GetInt16();
                    else if (propType == typeof(Int32))
                        val = value.GetInt32();
                    else if (propType == typeof(Int64))
                        val = value.GetInt64();
                    else
					    throw new Exception("dx json expression parser unknown json convert type!");
					break;
                case "float":
                case "decimal":
                case "decımal":
                    val = value.GetDecimal();
                    break;
                case "double":
                    val = value.GetDouble();
                    break;
                case "boolean":
                case "bool":
                    val = value.GetBoolean();
                    break;
                case "date":
                case "datetime":
                case "datetıme":
                    val = value.GetDateTime();
                    break;
                default:
                    val = (object)value.GetString();
                    break;

            }


            return val;
        }

        private Expression CustomGreaterThan(Expression e1, Expression e2)
        {
            if (IsNullableType(e1.Type) && !IsNullableType(e2.Type))
                e2 = Expression.Convert(e2, e1.Type);
            //else if (!IsNullableType(e1.Type) && IsNullableType(e2.Type))
            //    e1 = Expression.Convert(e1, e2.Type);
            return Expression.GreaterThan(e1, e2);
        }

        private Expression CustomGreaterThanOrEqual(Expression e1, Expression e2)
        {
            if (IsNullableType(e1.Type) && !IsNullableType(e2.Type))
                e2 = Expression.Convert(e2, e1.Type);
            //else if (!IsNullableType(e1.Type) && IsNullableType(e2.Type))
            //    e1 = Expression.Convert(e1, e2.Type);
            return Expression.GreaterThanOrEqual(e1, e2);
        }

        private Expression CustomLessThan(Expression e1, Expression e2)
        {
            if (IsNullableType(e1.Type) && !IsNullableType(e2.Type))
                e2 = Expression.Convert(e2, e1.Type);
            //else if (!IsNullableType(e1.Type) && IsNullableType(e2.Type))
            //    e1 = Expression.Convert(e1, e2.Type);
            return Expression.LessThan(e1, e2);
        }

        private Expression CustomLessThanOrEqual(Expression e1, Expression e2)
        {
            if (IsNullableType(e1.Type) && !IsNullableType(e2.Type))
                e2 = Expression.Convert(e2, e1.Type);
            //else if (!IsNullableType(e1.Type) && IsNullableType(e2.Type))
            //    e1 = Expression.Convert(e1, e2.Type);
            return Expression.LessThanOrEqual(e1, e2);
        }

        private bool IsNullableType(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public Expression<Func<T, bool>> ParseExpressionOf<T>(JsonDocument doc)
        {
            var itemExpression = Expression.Parameter(typeof(T));


            var conditions = ParseTree<T>(doc.RootElement, itemExpression);
            if (conditions.CanReduce)
            {
                conditions = conditions.ReduceAndCheck();
            }

            //Console.WriteLine(conditions.ToString());

            var query = Expression.Lambda<Func<T, bool>>(conditions, itemExpression);
            return query;
        }

        public Func<T, bool> ParsePredicateOf<T>(JsonDocument doc)
        {
            var query = ParseExpressionOf<T>(doc);
            return query.Compile();
        }

        public Expression<Func<T, bool>> ParsePredicateOfDxFilter<T>(DxFilterInput dxInput)
        {
            /*
             * IsLoading all true olduğu anda herhangi bir filtre uygulama anlamına gelir..
             */
            //if (dxInput.IsLoadingAll == true)
            //    return null;

            //if (!string.IsNullOrEmpty(dxInputJson))
            //{
            //    var dxInput = JsonConvert.DeserializeObject<dtoDxSourceInput>(dxInputJson);

            var dxExpression = DxFilterToExpressionObj(dxInput.Filter);

            if (dxExpression != null)
            {
                dxInput.Filter = JsonConvert.SerializeObject(dxExpression);

                var jsonDocument = JsonDocument.Parse(dxInput.Filter);

                return ParseExpressionOf<T>(jsonDocument);
                //return query.Compile();
            }
            //}

            return null;
        }

		public Expression<Func<T, bool>> ParsePredicateOfDxSearch<T>(IDxFilterInput dxInput)
		{
			
			var dxExpression = DxFilterToExpressionObj(dxInput.Filter);

			if (dxExpression != null)
			{
				dxInput.Filter = JsonConvert.SerializeObject(dxExpression);

				var jsonDocument = JsonDocument.Parse(dxInput.Filter);

				return ParseExpressionOf<T>(jsonDocument);
				//return query.Compile();
			}
			//}
            
			return null;
		}

		static Expression ETForStartsWith<T>(string propertyValue, PropertyInfo propertyInfo)
        {
            ParameterExpression e = Expression.Parameter(typeof(T), "e");
            MemberExpression m = Expression.MakeMemberAccess(e, propertyInfo);
            ConstantExpression c = Expression.Constant(propertyValue, typeof(string));
            MethodInfo mi = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
            Expression call = Expression.Call(m, mi, c);

            return Expression.Lambda<Func<T, bool>>(call, e);
        }

        public DxExpression DxFilterToExpressionObj(string gridFilter)
        {
            var dxEx = new DxExpression();

            if (string.IsNullOrEmpty(gridFilter))
                return null;

            JArray sqlArray = JArray.Parse(gridFilter);
            //if (!sqlArray[0].HasValues)
            sqlArray = new JArray((object)sqlArray);

            //string filterExp = "";

            iterateExpression(sqlArray, ref dxEx);
            return dxEx;
        }

        public void iterateExpression(JArray farr, ref DxExpression dxExpression)
        {
            var addedCondition = false;

            for (int i = 0; i < farr.Count; ++i)
            {
                var val = farr[i];

                if (val.HasValues)
                {
                    if (val[0] is JArray)
                    {
                        var newEx = new DxExpression();
                        iterateExpression(val as JArray, ref newEx);

                        dxExpression.Rules.Add(newEx);
                        //dxExpression.Rules.Add(new DxRule { Field =  });
                    }
                    else
                    {
                        if (val[0].ToString() == "!")
                        {

                            //dxExpression.Rules.Add(new DxRule { Field =  });

                            val[1][1] = "!" + val[1][1];
                            val = val[1];
                            //iterate(val[1] as JArray, ref sql);
                        }

                        //var currentVal = val[2];

                        var jToken = GetJTokenValueByType(val[2]);

                        DxRule dxRule = new DxRule
                        {
                            Field = val[0].ToString(),
                            Label = val[0].ToString(),
                            Type = jToken.Type,
                            Value = jToken.Value
                        };

                        switch (val[1].ToString())
                        {
                            case "contains":
                                dxRule.Operator = "contains";
                                break;
                            case "!contains":
                            case "notcontains":
                                dxRule.Operator = "notcontains";
                                break;
                            case "startswith":
                                dxRule.Operator = "startswith";
                                break;
                            case "endswith":
                                dxRule.Operator = "endswith";
                                break;
                            case "!=":
                                dxRule.Operator = "notequal";
                                break;
                            case ">":
                                dxRule.Operator = "greaterthan";
                                break;
                            case ">=":
                                dxRule.Operator = "greaterthanorequal";
                                break;
                            case "<":
                                dxRule.Operator = "lessthan";
                                break;
                            case "<=":
                                dxRule.Operator = "lessthanorequal";
                                break;
                            case "=":
                                dxRule.Operator = "equal";
                                break;
                            case "<>":
                                dxRule.Operator = "notequal";
                                break;
                        }

                        dxExpression.Rules.Add(dxRule);
                    }
                }
                else
                {
                    if (!addedCondition)
                    {
                        dxExpression.Condition = val.ToString();
                        addedCondition = true;
                    }
                }
                //	sql += val.ToString();

            }
        }

        public (JToken Value, string Type) GetJTokenValueByType(JToken value)
        {
            var newType = value.Type.ToString().ToLower();

            if (value.Type == JTokenType.String)
            {
                var isDatetime = DateTime.TryParse(value.ToString(), out _);

                if (isDatetime)
                {
                    newType = JTokenType.Date.ToString().ToLower();
                    value = DateTime.Parse(value.ToString());
                }
            }

            if (newType == "float")
                newType = "decimal";

            return (value, newType);
        }
    }
}
