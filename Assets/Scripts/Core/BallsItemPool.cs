using Game;
using UnityEngine;

namespace Core
{
    public class BallsItemPool : CustomObjectPool<Ball>
    {
        protected Ball _ballPrefab;
        
        public BallsItemPool(Ball ballPrefab, int poolSize) : base(poolSize) => 
            _ballPrefab = ballPrefab;

        protected override void OnItemDestroy(Ball item)
        {
            if (item == null)
                return;
            
            GameObject.Destroy(item.gameObject);
        }

        protected override void OnGetItem(Ball item)
        {
            item.gameObject.SetActive(true);
            base.OnGetItem(item);
        }

        protected override void OnReleaseItem(Ball item)
        {
            item.gameObject.SetActive(false);
            base.OnReleaseItem(item);
        }

        protected override Ball OnCreateItem() => 
            GameObject.Instantiate(_ballPrefab);
    }
}