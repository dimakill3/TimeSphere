using UnityEngine.Pool;

namespace Core
{
    public abstract class CustomObjectPool<T> where T : class, IPoolItem
    {
        private ObjectPool<T> _pool;

        public CustomObjectPool(int poolSize) => 
            _pool = new ObjectPool<T>(OnCreateItem, OnGetItem, OnReleaseItem, OnItemDestroy, defaultCapacity: poolSize, maxSize: poolSize);

        public T GetItem() => 
            _pool.Get();

        public void ReleaseItem(T item) => 
            _pool.Release(item);

        public void Dispose() => 
            _pool.Dispose();

        protected virtual void OnItemDestroy(T item)
        {
        }

        private void OnReleaseItem(IPoolItem item) => 
            ReleaseItem(item as T);

        protected virtual void OnGetItem(T item) => 
            item.PoolItemDestroyed += OnReleaseItem;

        protected virtual void OnReleaseItem(T item) => 
            item.PoolItemDestroyed -= OnReleaseItem;

        protected abstract T OnCreateItem();
    }
}