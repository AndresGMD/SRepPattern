using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace SRepPattern.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TObject> WhereByProperty<TObject>(this IQueryable<TObject> query, string property, [CallerArgumentExpression(nameof(property))] string propertyName = "")
    {
        var parameter = Expression.Parameter(typeof(TObject), "x");
        var left = Expression.Property(parameter, propertyName);
        var rightparameter = (Expression<Func<string>>)(() => (string)property);
        var body = Expression.Equal(left, rightparameter.Body);
        var predicate = Expression.Lambda<Func<TObject, bool>>(body, new ParameterExpression[] { parameter });

        return query.Where(predicate);
    }

    public static IQueryable<TObject> WhereContains<TObject>(this IQueryable<TObject> query, string[] propertyArray, [CallerArgumentExpression(nameof(propertyArray))] string propertyName = "")
    {
        if (propertyArray == null || !propertyArray.Any() || string.IsNullOrEmpty(propertyName))
            return query;
        var parameter = Expression.Parameter(typeof(TObject), "x");
        var selector = Expression.PropertyOrField(parameter, propertyName);
        var predicate = Expression.Lambda<Func<TObject, bool>>(
        Expression.Call(typeof(Enumerable), "Contains", new[] { typeof(string) },
            Expression.Constant(propertyArray), selector), parameter);

        return query.Where(predicate);
    }

    public static IQueryable<TEntity> SelectEntity<TObject, TEntity>(this IQueryable<TObject> query) where TEntity : class where TObject : class
    {
        var parameter = Expression.Parameter(typeof(TObject), "x");
        var left = Expression.Property(parameter, typeof(TEntity).Name.Replace("Entity",string.Empty));
        var selector = Expression.Lambda<Func<TObject, TEntity>>(left, parameter);
        return query.Select(selector);
    }

    public static IQueryable<TObject> IncludeEntity<TObject, TEntity>(this IQueryable<TObject> query) where TEntity : class where TObject : class
    {
        var parameter = Expression.Parameter(typeof(TObject), "x");
        var left = Expression.Property(parameter, typeof(TEntity).Name.Replace("Entity",string.Empty));
        var selector = Expression.Lambda<Func<TObject, TEntity>>(left, parameter);
        return query.Include(selector);
    }

    public static IQueryable<TObject> OrderByParameter<TObject,TProperty>(this IQueryable<TObject> query, TProperty property, [CallerArgumentExpression(nameof(property))] string propertyName = "") where TObject : class
    {
        var parameter = Expression.Parameter(typeof(TObject), "x");
        var left = Expression.Property(parameter, propertyName);
        var selector = Expression.Lambda<Func<TObject, string>>(left, parameter);
        return query.OrderBy(selector);
    }
}
