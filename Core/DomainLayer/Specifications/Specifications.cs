using System.Linq.Expressions;
using DomainLayer.Entities;

namespace DomainLayer.Specifications;

public class Specification<TEntity>
    where TEntity : class
{
    public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

    public List<Expression<Func<TEntity, object>>> Includes { get; }
        = new();

    public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

    public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

    public int Skip { get; private set; }

    public int Take { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    protected void AddCriteria(
      Expression<Func<TEntity, bool>> criteria)
    {
        if (Criteria is null)
        {
            Criteria = criteria;
            return;
        }

        var parameter = Expression.Parameter(typeof(TEntity), "entity");

        var left = Expression.Invoke(Criteria, parameter);
        var right = Expression.Invoke(criteria, parameter);

        var body = Expression.AndAlso(left, right);

        Criteria = Expression.Lambda<Func<TEntity, bool>>(
            body,
            parameter);
    }

    protected void AddInclude(
        Expression<Func<TEntity, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected void AddOrderBy(
        Expression<Func<TEntity, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void AddOrderByDescending(
        Expression<Func<TEntity, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}