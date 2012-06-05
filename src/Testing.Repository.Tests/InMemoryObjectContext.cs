using System;
using System.Collections.Generic;
using System.Linq;

namespace Testing.Repository.Tests
{
    public class InMemoryObjectContext
    {
        readonly Dictionary<Type, HashSet<object>> _objectHashSets;

        public InMemoryObjectContext()
        {
            _objectHashSets = new Dictionary<Type, HashSet<object>>();
        }

        public HashSet<T> GetObjects<T>() where T : IEntity, new()
        {
            var type = typeof(T);
            if (!_objectHashSets.ContainsKey(type))
            {
                var set = Enumerable.Range(0, 100).Select(x => new T
                {
                    Id = x
                }).Cast<object>();
                _objectHashSets[type] = new HashSet<object>(set);
            }
            return new HashSet<T>(_objectHashSets[type].Cast<T>());
        }

        public void Add<T>(T entity) where T : IEntity, new()
        {
            var hashSet = GetObjects<T>();
            hashSet.Add(entity);
        }
    }
}