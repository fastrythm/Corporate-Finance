using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.Helper.Extension
{
    
        public interface IPagedList
        {
            int TotalCount { get; }
            int PageCount { get; }
            int Page { get; }
            int PageSize { get; }
        }

        public class PagedList<T> : List<T>, IPagedList
        {
            public int TotalCount { get; private set; }
            public int PageCount { get; private set; }
            public int Page { get; private set; }
            public int PageSize { get; private set; }

            public PagedList(IQueryable<T> source, int page, int pageSize)
            {
                TotalCount = source.Count();
                PageCount = GetPageCount(pageSize, TotalCount);
                Page = page < 1 ? 0 : page - 1;
                PageSize = pageSize;

                AddRange(source.Skip(Page * PageSize).Take(PageSize).ToList());
            }

            //public PagedList(IQueryable<T> source, int page, int pageSize, DataTablesViewModel receivedparam)
            //{
            //    //TotalCount = source.Count();

            //    int index = 0;
            //    foreach (var columnData in receivedparam.columns)
            //    {
            //        if (!string.IsNullOrEmpty(columnData.search.value) && columnData.searchable)
            //            source = PageListFiterOrderExtension.WhereStringContains(source, columnData.data, columnData.search.value);//.OrderBy(columnData.data);
            //        if (columnData.orderable)
            //            foreach (var row in receivedparam.order.Where(i => i.column == index))
            //            {
            //                if (row.dir == "asc")
            //                    source = source.OrderBy(columnData.data);
            //                else
            //                    source = source.OrderByDescending(columnData.data);
            //            }

            //        index += 1;
            //    }
            //    if (!string.IsNullOrEmpty(receivedparam.search.value))
            //        source = PageListFiterOrderExtension.WhereStringContainsOr(source, null, receivedparam.search.value, receivedparam);//.OrderBy(columnData.data);

            //    //var users = this.entities.tableUsers.WhereStringContains(searchField, search)
            //    //.OrderBy(searchField);
            //    TotalCount = source.Count();
            //    PageCount = GetPageCount(pageSize, TotalCount);
            //    Page = page < 1 ? 0 : page - 1;
            //    PageSize = pageSize;

            //    AddRange(source.Skip(Page * PageSize).Take(PageSize).ToList());
            //}

            private int GetPageCount(int pageSize, int totalCount)
            {
                if (pageSize == 0)
                    return 0;

                var remainder = totalCount % pageSize;
                return (totalCount / pageSize) + (remainder == 0 ? 0 : 1);
            }

            //private int GetPageCount(int pageSize, int totalCount, DataTablesViewModel param)
            //{
            //    if (pageSize == 0)
            //        return 0;

            //    var remainder = totalCount % pageSize;
            //    return (totalCount / pageSize) + (remainder == 0 ? 0 : 1);
            //}

        }

        public static class PagedListExtensions
        {
            public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int page, int pageSize)
            {
                return new PagedList<T>(source, page, pageSize);
            }

       
        }

        public static class PageListFiterOrderExtension
        {
            public static IQueryable<T> WhereStringContains<T>(this IQueryable<T> query, string propertyName, string contains)
            {
                var parameter = Expression.Parameter(typeof(T), "type");
                var propertyExpression = Expression.Property(parameter, propertyName);

                MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                Type[] typeArray = new Type[1];
                typeArray.SetValue(typeof(DateTime), 0);

                MethodInfo methodTruncTime = typeof(EntityFunctions).GetMethod("TruncateTime", typeArray);
                var someValue = Expression.Constant(contains.ToUpper(), typeof(string));
                if (propertyExpression.Type.Name.ToString().ToLower().Contains("decimal"))
                    return query;
                else if (propertyExpression.Type.Name.ToString().ToLower().Contains("string"))
                {
                    var containsExpression = Expression.Call(
                                                Expression.Call( // <=== this one is new
                                                    propertyExpression,
                                                    "ToUpper", null),
                                                "Contains", null,
                                             someValue);
                    return query.Where(Expression.Lambda<Func<T, bool>>(containsExpression, parameter));
                }
                else if (propertyExpression.Type.Name.ToString().ToLower().Contains("datetime"))
                {
                    try
                    {
                        #region Commit
                        //Expression left = propertyExpression;
                        //Expression right = Expression.Constant(date.ToString("dd/MM/yyyy"));

                        //Type type = typeof(T);
                        //PropertyInfo idProperty = type.GetProperty(propertyName);
                        //ParameterExpression lambdaParam = Expression.Parameter(type, "type");
                        //MemberExpression body = Expression.Property(lambdaParam, idProperty);
                        //UnaryExpression converted = Expression.Convert(body, typeof(string));
                        //LambdaExpression expr = Expression.Lambda(Expression.Call(
                        // Expression.Convert(body, typeof(string)),
                        // typeof(string).GetMethod("ToString")), lambdaParam);

                        ////var equalExpression = Expression.Equal(Expression.Call(
                        ////Expression.Convert(expr, typeof(object)),
                        ////typeof(object).GetMethod("Contains")),
                        ////                             right);

                        //var equalExpression = Expression.Equal(expr,
                        //                              right);

                        //DateTime date = Convert.ToDateTime(someValue.ToString().Substring(1, 10));
                        #endregion

                        Expression left = Expression.Convert(propertyExpression, typeof(DateTime));
                        Expression right = Expression.Constant(Convert.ToDateTime(contains));

                        var converted = Expression.Convert(right, typeof(DateTime));
                        //Expression result3 = Expression.LessThanOrEqual(left, right);

                        var equalExpression = Expression.Equal(left,
                                                      converted);

                        return query.Where(Expression.Lambda<Func<T, bool>>(equalExpression, parameter));

                    }
                    catch (Exception ex)
                    {
                        return query;
                    }
                }
                else
                {
                    return query;
                }

            }

            //public static IQueryable<T> WhereStringContainsOr<T>(this IQueryable<T> query, string propertyName, string contains, DataTablesViewModel receivedparam)
            //{
            //    Expression compoundExpression = null;
            //    ParameterExpression parameter = Expression.Parameter(typeof(T), "type");
            //    #region Commit Methods
            //    //MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            //    //MethodInfo methodUpper = typeof(string).GetMethod("ToUpper", new[] { typeof(string) });
            //    //MethodInfo equalsMethod = typeof(string).GetMethod("Equals", new[] { typeof(string) });
            //    //MethodInfo methodDateTimeParse = typeof(DateTime).GetMethod("Parse", new Type[] { typeof(string) });
            //    //MethodInfo methodDateTimeNUllableParse = typeof(DateTime?).GetMethod("Parse", new Type[] { typeof(string) });
            //    #endregion

            //    foreach (var columnData in receivedparam.columns)
            //    {
            //        //var predicate =  PredicateBuilder.False<T>();
            //        propertyName = columnData.data;

            //        var propertyExpression = Expression.Property(parameter, propertyName);
            //        var someValue = Expression.Constant(contains.ToString().ToUpper(), typeof(string));
            //        #region Code Version(s)
            //        //var containsExpression = Expression.Call(propertyExpression, method, someValue);
            //        //var containsExpression = Expression.Call(
            //        //                            Expression.Call(Expression.Call( // <=== this one is new
            //        //                                            propertyExpression,
            //        //                                            "ToString", null), // <=== this one is new
            //        //                                            "ToUpper", null),
            //        //                            "Contains", null,
            //        //                          someValue);
            //        //var containsExpression = Expression.Call(
            //        //                                Expression.Call( // <=== this one is new
            //        //                                    propertyExpression,
            //        //                                    "ToUpper", null),
            //        //                                 "Contains", null,
            //        //                          someValue);

            //        //var containsExpression = Expression.Call(propertyExpression,
            //        //                                    equalsMethod, null,
            //        //                              someValue);
            //        //var containsExpression = Expression.Call(
            //        //                                Expression.Call( // <=== this one is new
            //        //                                    propertyExpression,
            //        //                                    "ToUpper", null),
            //        //                                 "Contains", null,
            //        //                          someValue);
            //        #endregion
            //        if (propertyExpression.Type.Name.ToString().ToLower().Contains("decimal"))
            //            continue;
            //        else if (propertyExpression.Type.Name.ToString().ToLower().Contains("string"))
            //        {
            //            var containsExpression = Expression.Call(
            //                                           Expression.Call( // <=== this one is new
            //                                                 propertyExpression,
            //                                                 "ToUpper", null),
            //                                                 "Contains", null,
            //                                           someValue);


            //            if (compoundExpression != null)
            //                compoundExpression = Expression.Or(compoundExpression, containsExpression);
            //            else
            //                compoundExpression = containsExpression;

            //            query = query.Where(Expression.Lambda<Func<T, bool>>(compoundExpression, parameter));

            //        }
            //        else if (propertyExpression.Type.Name.ToString().ToLower().Contains("datetime"))
            //        {
            //            //try
            //            //{
            //            //    Expression left = Expression.Convert(propertyExpression, typeof(DateTime));
            //            //    Expression right = Expression.Constant(Convert.ToDateTime(contains));
            //            //    //Expression result3 = Expression.LessThanOrEqual(left, right);

            //            //    var equalExpression = Expression.Equal(left,
            //            //                                  right);

            //            //    if (compoundExpression != null)
            //            //        compoundExpression = Expression.Or(compoundExpression, equalExpression);
            //            //    else
            //            //        compoundExpression = equalExpression;

            //            //    //tmp = tmp.Where(Expression.Lambda<Func<T, bool>>(compoundExpression, parameter));
            //            //    //predicate = predicate.Or(Expression.Lambda<Func<T, bool>>(compoundExpression, parameter));
            //            //    query = query.Where(Expression.Lambda<Func<T, bool>>(compoundExpression, parameter));
            //            //}
            //            //catch (Exception)
            //            //{

            //            //}

            //        }
            //        else
            //        {
            //            //Expression left = propertyExpression;
            //            //Expression right = Expression.Constant(contains);
            //            ////Expression result3 = Expression.LessThanOrEqual(left, right);

            //            //var equalExpression = Expression.Equal(left,
            //            //                              right);

            //            //if (compoundExpression != null)
            //            //    compoundExpression = Expression.Or(compoundExpression, equalExpression);
            //            //else
            //            //    compoundExpression = equalExpression;

            //            ////tmp = tmp.Where(Expression.Lambda<Func<T, bool>>(compoundExpression, parameter));
            //            ////predicate = predicate.Or(Expression.Lambda<Func<T, bool>>(compoundExpression, parameter));
            //            //query = query.Where(Expression.Lambda<Func<T, bool>>(compoundExpression, parameter));
            //        }

            //    }
            //    return query;
            //}

            public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string propertyName)
            {
                var propertyType = typeof(T).GetProperty(propertyName).PropertyType;
                var parameter = Expression.Parameter(typeof(T), "type");
                var propertyExpression = Expression.Property(parameter, propertyName);
                var lambda = Expression.Lambda(propertyExpression, new[] { parameter });

                return typeof(Queryable).GetMethods()
                                        .Where(m => m.Name == "OrderBy" && m.GetParameters().Length == 2)
                                        .Single()
                                        .MakeGenericMethod(new[] { typeof(T), propertyType })
                                        .Invoke(null, new object[] { query, lambda }) as IOrderedQueryable<T>;
            }

            public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string propertyName)
            {
                var propertyType = typeof(T).GetProperty(propertyName).PropertyType;
                var parameter = Expression.Parameter(typeof(T), "type");
                var propertyExpression = Expression.Property(parameter, propertyName);
                var lambda = Expression.Lambda(propertyExpression, new[] { parameter });

                return typeof(Queryable).GetMethods()
                                        .Where(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2)
                                        .Single()
                                        .MakeGenericMethod(new[] { typeof(T), propertyType })
                                        .Invoke(null, new object[] { query, lambda }) as IOrderedQueryable<T>;
            }
        }


        public static class PredicateBuilder
        {
            public static Expression<Func<T, bool>> True<T>() { return f => true; }
            public static Expression<Func<T, bool>> False<T>() { return f => false; }

            public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                                Expression<Func<T, bool>> expr2)
            {
                var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
                return Expression.Lambda<Func<T, bool>>
                      (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
            }

            public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                                 Expression<Func<T, bool>> expr2)
            {
                var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
                return Expression.Lambda<Func<T, bool>>
                      (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
            }
        }
 

}
