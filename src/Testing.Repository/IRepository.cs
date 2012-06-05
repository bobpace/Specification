using System.Collections.Generic;

namespace Testing.Repository
{
    public interface IRepository
    {
        T GetByKey<T>(int key) where T : IEntity, new();
        IEnumerable<T> Query<T>(ISpecification<T> specification) where T : IEntity, new();
        T Single<T>(ISpecification<T> specification) where T : IEntity, new();
        T First<T>(ISpecification<T> specification) where T : IEntity, new();
        void Add<T>(T entity) where T : IEntity, new();
        void Update<T>(T entity) where T : IEntity, new();
        int Delete<T>(ISpecification<T> specification) where T : IEntity, new();
        int Count<T>(ISpecification<T> specification) where T : IEntity, new();
    }
}