using System;
using System.Linq.Expressions;

namespace Testing.Repository.Specifications
{
    public class LambdaSpecification<T> : Specification<T>
    {
        readonly Expression<Func<T, bool>> _expression;

        public LambdaSpecification(Expression<Func<T, bool>> expression)
        {
            _expression = expression;
        }

        public override Expression<Func<T, bool>> IsSatisfied()
        {
            return _expression;
        }
    }
}