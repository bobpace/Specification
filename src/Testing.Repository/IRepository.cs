using System.Collections.Generic;

namespace Testing.Repository
{
    public interface IRepository
    {
        T GetByKey<T>(int key) where T : class, IEntity, new();
        IEnumerable<T> Query<T>(ISpecification<T> specification) where T : class, IEntity, new();
        T Single<T>(ISpecification<T> specification) where T : class, IEntity, new();
        T First<T>(ISpecification<T> specification) where T : class, IEntity, new();
        void Add<T>(T entity) where T : class, IEntity, new();
        void Update<T>(T entity) where T : class, IEntity, new();
        int Delete<T>(ISpecification<T> specification) where T : class, IEntity, new();
        int Count<T>(ISpecification<T> specification) where T : class, IEntity, new();
    }
}