using System;
using System.Linq;
using System.Linq.Expressions;

namespace Testing.Repository.Specifications
{
    public class NegatedSpecification<T> : Specification<T>
    {
        readonly ISpecification<T> _original;

        public NegatedSpecification(ISpecification<T> original)
        {
            _original = original;
        }

        public override Expression<Func<T, bool>> IsSatisfied()
        {
            Expression<Func<T, bool>> originalTree = _original.IsSatisfied();
            return Expression.Lambda<Func<T, bool>>(
                Expression.Not(originalTree.Body),
                originalTree.Parameters.Single());
        }
    }
}