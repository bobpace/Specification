using System;
using System.Linq.Expressions;
using Testing.Repository.Specifications;

namespace Testing.Repository.Examples
{
    public class NumberOfLegsSpecification : Specification<Animal>
    {
        readonly int _numberOfLegs;

        public NumberOfLegsSpecification(int numberOfLegs)
        {
            _numberOfLegs = numberOfLegs;
        }

        public override Expression<Func<Animal, bool>> IsSatisfied()
        {
            return x => x.NumberOfLegs == _numberOfLegs;
        }
    }
}