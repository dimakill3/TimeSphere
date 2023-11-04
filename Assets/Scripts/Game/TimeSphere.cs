using UnityEngine;

namespace Game
{
    public class TimeSphere : MonoBehaviour
    {
        [SerializeField] private float slowModifier = 10;

        private void OnTriggerEnter2D(Collider2D other)
        {
            ISlowableObject slowableObject;
            
            if (other.TryGetComponent<ISlowableObject>(out slowableObject))
                slowableObject.Slow(slowModifier);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            ISlowableObject slowableObject;
            
            if (other.TryGetComponent<ISlowableObject>(out slowableObject))
                slowableObject.Unslow();
        }
    }
}