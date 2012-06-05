using System;
using System.Linq.Expressions;

namespace Testing.Repository.Specifications
{
    public class GetByKeySpecification<T> : Specification<T> where T : IEntity
    {
        readonly int _key;

        public GetByKeySpecification(int key)
        {
            _key = key;
        }

        public override Expression<Func<T, bool>> IsSatisfied()
        {
            return x => x.Id == _key;
        }
    }
}