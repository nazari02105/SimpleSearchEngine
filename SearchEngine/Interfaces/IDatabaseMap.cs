using System.Collections.Generic;

namespace SearchEngine.Interfaces
{
    public interface IDatabaseMap<K, V>
    {
        List<V> Get(K key);
        void Add(K key, V value);
        bool Delete();
        bool Create();
        void Save();
        List<K> GetHints(string hint);
    }
}