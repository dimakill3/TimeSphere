using System;
using Core;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour, IPoolItem, ISlowableObject
    {
        public event Action<IPoolItem> PoolItemDestroyed;
        
        [SerializeField] private Rigidbody2D ballRigidbody2D;
        [SerializeField] private LayerMask destroyLayer;

        private float _appliedSlowModifier;
        
        public void ApplyImpulse(Vector3 direction, float force) => 
            ballRigidbody2D.AddForce(direction * force, ForceMode2D.Impulse);

        public void Slow(float slowModifier)
        {
            float gravityModifier = slowModifier * slowModifier;
            
            ballRigidbody2D.gravityScale = 1 / gravityModifier;
            ballRigidbody2D.velocity /= slowModifier;

            _appliedSlowModifier = slowModifier;
        }

        public void Unslow()
        {
            ballRigidbody2D.gravityScale = 1;
            ballRigidbody2D.velocity *= _appliedSlowModifier;
        }

        public void Destroy() => 
            PoolItemDestroyed?.Invoke(this);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (destroyLayer != (destroyLayer.value | 1 << other.gameObject.layer))
                return;

            Destroy();
        }
    }
}