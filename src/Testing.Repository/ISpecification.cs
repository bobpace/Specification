using System;
using System.Linq.Expressions;

namespace Testing.Repository
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> IsSatisfied();
    }
}