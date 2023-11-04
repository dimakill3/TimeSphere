using System;
using Core;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour, IPoolItem, ISlowableObject
    {
        public event Action<IPoolItem> PoolItemDestroyed;
        
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private LayerMask _destroyLayer;

        public void ApplyImpulse(Vector3 direction, float force) => 
            _rigidbody2D.AddForce(direction * force, ForceMode2D.Impulse);

        public void Slow(float slowModifier)
        {
            _rigidbody2D.velocity /= slowModifier * 0.5f;
            _rigidbody2D.gravityScale /= slowModifier;
        }

        public void Unslow()
        {
            _rigidbody2D.gravityScale = 1;
        }

        public void Destroy() => 
            PoolItemDestroyed?.Invoke(this);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_destroyLayer != (_destroyLayer.value | 1 << other.gameObject.layer))
                return;

            Destroy();
        }
    }
}