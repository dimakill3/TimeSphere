using Core;
using UnityEngine;

namespace Game
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Ball ballPrefab;
        [SerializeField] private Transform ballSpawnPoint;
        [SerializeField] private float fireForce;
        [SerializeField] private float fireDelayInSeconds = 3;
        [Range(5, 20)]
        [SerializeField] private int poolSize = 10;

        private BallsItemPool _ballsItemPool;
        private bool _isActivated;
        private float _timer;

        public void Initialize() => 
            _ballsItemPool = new BallsItemPool(ballPrefab, poolSize);

        public void Activate() => 
            _isActivated = true;

        public void Destruct()
        {
            _isActivated = false;
            _ballsItemPool.Dispose();
        }

        private void Update()
        {
            if (!_isActivated)
                return;

            _timer -= Time.deltaTime;

            if (!(_timer <= 0)) 
                return;
            
            Fire();
            _timer = fireDelayInSeconds;
        }

        private void Fire()
        {
            Ball ball = _ballsItemPool.GetItem();
            ball.transform.position = ballSpawnPoint.position;
            ball.ApplyImpulse((ballSpawnPoint.position - transform.position).normalized, fireForce);
        }
    }
}
