using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore.Util;

namespace Testing.Repository.Tests
{
    public interface IObjectContext
    {
        IEnumerable<T> GetObjects<T>() where T : IEntity, new();
        void Add<T>(T entity) where T : IEntity, new();
        void Delete<T>(T entity) where T : IEntity, new();
    }

    public class InMemoryObjectContext : IObjectContext
    {
        readonly Dictionary<Type, HashSet<object>> _objectHashSets;

        public InMemoryObjectContext()
        {
            _objectHashSets = new Dictionary<Type, HashSet<object>>();
        }

        public IEnumerable<T> GetObjects<T>() where T : IEntity, new()
        {
            var type = typeof(T);
            EnsureHashSet(type);
            return _objectHashSets[type].Cast<T>();
        }

        public void Add<T>(T entity) where T : IEntity, new()
        {
            var type = typeof(T);
            EnsureHashSet(type);
            _objectHashSets[type].Add(entity);
        }

        public void Delete<T>(T entity) where T : IEntity, new()
        {
            var type = typeof(T);
            EnsureHashSet(type);
            var hashSet = _objectHashSets[type];
            if (hashSet.Contains(entity))
            {
                hashSet.Remove(entity);
            }
        }

        private void EnsureHashSet(Type type)
        {
            if (!_objectHashSets.ContainsKey(type))
            {
                _objectHashSets[type] = new HashSet<object>();
            }
        }
    }

    public class InMemoryObjectWithCacheContext : IObjectContext
    {
        readonly Cache<Type, HashSet<object>> _objectHashSets;

        public InMemoryObjectWithCacheContext()
        {
            _objectHashSets = new Cache<Type, HashSet<object>>(x => new HashSet<object>());
        }

        public IEnumerable<T> GetObjects<T>() where T : IEntity, new()
        {
            var type = typeof(T);
            return _objectHashSets[type].Cast<T>();
        }

        public void Add<T>(T entity) where T : IEntity, new()
        {
            var type = typeof(T);
            _objectHashSets[type].Add(entity);
        }

        public void Delete<T>(T entity) where T : IEntity, new()
        {
            var type = typeof(T);
            var hashSet = _objectHashSets[type];
            if (hashSet.Contains(entity))
            {
                hashSet.Remove(entity);
            }
        }
    }
}