using UnityEngine;
using UnityEngine.Events;

namespace Bits.Physics
{
    public class RagdollExplosive : MonoBehaviour
    {
        [SerializeField] private float _force = 6f;
        [SerializeField] private float _radius = 2f;
        [SerializeField] private float _upwardsModifier = 1f;
        [SerializeField] private bool _detonateOnEnable;

        [Space]
        public UnityEvent OnDetonate;


        private void OnEnable()
        {
            if (_detonateOnEnable)
            {
                Detonate();
            }
        }

        public void Detonate()
        {
            Collider[] colliders = GetComponentsInChildren<Collider>();
            for (int i = 0; i < colliders.Length; i++)
            {
                ApplyForce(colliders[i].transform);
            }

            OnDetonate?.Invoke();
        }

        private void ApplyForce(Transform target)
        {
            if (_force != 0f && target.TryGetComponent(out Rigidbody body))
            {
                body.AddExplosionForce(_force, transform.position, _radius, _upwardsModifier, ForceMode.Impulse);
            }
        }
    }
}