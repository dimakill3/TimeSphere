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

        private Vector2 _initialVelocity;
        private bool _isSlowed;
        
        public void ApplyImpulse(Vector3 direction, float force) => 
            ballRigidbody2D.AddForce(direction * force, ForceMode2D.Impulse);

        public void Slow(float slowModifier)
        {
            ballRigidbody2D.gravityScale = 1f / (slowModifier * slowModifier - 1);
            ballRigidbody2D.velocity /= slowModifier;
        }

        public void Unslow(float slowModifier)
        {
            ballRigidbody2D.gravityScale = 1;
            ballRigidbody2D.AddForce(ballRigidbody2D.velocity.normalized * slowModifier, ForceMode2D.Impulse);
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