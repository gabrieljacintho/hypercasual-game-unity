using UnityEngine;

namespace Bits.Vitals
{
    public class ExplosionArea : DamageArea
    {
        [Header("Explosion Parameters")]
        [SerializeField] private float _force = 6f;
        [SerializeField] private float _radius = 2f;
        [SerializeField] private float _upwardsModifier = 1f;


        protected override void Hit(Health health)
        {
            base.Hit(health);

            Collider[] colliders = health.gameObject.GetComponentsInChildren<Collider>();
            for (int i = 0; i < colliders.Length; i++)
            {
                ApplyForce(colliders[i].transform);
            }
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