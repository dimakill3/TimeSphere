using System;

namespace Core
{
    public interface IPoolItem
    {
        public event Action<IPoolItem> PoolItemDestroyed;
        public void Destroy();
    }
}