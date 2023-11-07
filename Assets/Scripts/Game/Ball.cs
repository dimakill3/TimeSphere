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

        public void ApplyImpulse(Vector3 direction, float force) => 
            ballRigidbody2D.AddForce(direction * force, ForceMode2D.Impulse);

        public void Slow(float slowModifier)
        {
            ballRigidbody2D.gravityScale /= slowModifier;
            ballRigidbody2D.velocity -= ballRigidbody2D.velocity.normalized * slowModifier;
        }

        public void Unslow(float slowModifier)
        {
            ballRigidbody2D.gravityScale *= slowModifier;
            ballRigidbody2D.velocity += ballRigidbody2D.velocity.normalized * slowModifier;
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