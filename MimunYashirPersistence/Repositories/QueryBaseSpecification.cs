using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirPersistence.Repositories
{
    public class QueryBaseSpecification<T> : IQuerySpecification<T>
    {
        public QueryBaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool isPagingEnabled { get; private set; } = false;
        public List<Expression<Func<T, bool>>> ExtraCriterias { get; } = new List<Expression<Func<T, bool>>>();
        public void AddWhere(Expression<Func<T, bool>> whereExpression)
        {
            ExtraCriterias.Add(whereExpression);
        }
        public void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        public void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
        public void ApplyPaging(int page, int pageSize)
        {
            Skip = Math.Max(0, (page - 1) * pageSize);
            Take = pageSize;
            isPagingEnabled = true;
        }
        public void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        public void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }
    }
}
