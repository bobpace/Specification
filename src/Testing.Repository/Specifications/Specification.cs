using System;
using System.Linq.Expressions;

namespace Testing.Repository.Specifications
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public abstract Expression<Func<T, bool>> IsSatisfied();

        public Specification<T> And(ISpecification<T> otherSpecification)
        {
            return new AndSpecification<T>(this, otherSpecification);
        }

        public Specification<T> Or(ISpecification<T> otherSpecification)
        {
            return new OrSpecification<T>(this, otherSpecification);
        }

        public Specification<T> Negated(ISpecification<T> otherSpecification)
        {
            return new NegatedSpecification<T>(this);
        }
    }
}