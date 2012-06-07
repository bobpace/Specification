using System.Collections.Generic;
using System.Linq;

namespace Testing.Repository.Tests
{
    public class InMemoryRepository : IRepository
    {
        readonly IObjectContext _context;

        public InMemoryRepository(IObjectContext context)
        {
            _context = context;
        }

        public T GetByKey<T>(int key) where T : IEntity, new()
        {
            var objects = _context.GetObjects<T>();
            return objects.FirstOrDefault(x => x.Id == key);
        }

        public IEnumerable<T> Query<T>(ISpecification<T> specification) where T : IEntity, new()
        {
            var objects = _context.GetObjects<T>();
            var expression = specification.IsSatisfied();
            return objects.Where(expression.Compile());
        }

        public T Single<T>(ISpecification<T> specification) where T : IEntity, new()
        {
            var objects = _context.GetObjects<T>();
            var expression = specification.IsSatisfied();
            return objects.Single(expression.Compile());
        }

        public T First<T>(ISpecification<T> specification) where T : IEntity, new()
        {
            var objects = _context.GetObjects<T>();
            var expression = specification.IsSatisfied();
            return objects.First(expression.Compile());
        }

        public void Add<T>(T entity) where T : IEntity, new()
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : IEntity, new()
        {
        }

        public int Delete<T>(ISpecification<T> specification) where T : IEntity, new()
        {
            var objects = _context.GetObjects<T>();
            var expression = specification.IsSatisfied();
            var filteredQuery = objects.Where(expression.Compile());
            var deletedCount = 0;
            foreach (var item in filteredQuery)
            {
                deletedCount++;
                _context.Delete(item);
            }
            return deletedCount;
        }

        public int Count<T>(ISpecification<T> specification) where T : IEntity, new()
        {
            var objects = _context.GetObjects<T>();
            var expression = specification.IsSatisfied();
            return objects.Count(expression.Compile());
        }
    }
}