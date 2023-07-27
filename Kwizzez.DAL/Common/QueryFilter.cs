using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

public class QueryFilter<T>
{
    public Expression<Func<T, bool>> Filter { get; set; }
    public string IncludeProperties { get; set; } = "";
    public Func<IQueryable<T>, IOrderedQueryable<T>> OrderExpression { get; set; }
    public int Take { get; set; }
    public int Skip { get; set; }
}