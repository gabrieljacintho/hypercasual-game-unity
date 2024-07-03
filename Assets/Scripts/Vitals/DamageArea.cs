using Bits.Physics;
using UnityEngine;
using UnityEngine.Events;

namespace Bits.Vitals
{
    public class DamageArea : TriggerComponent
    {
        [Space]
        public UnityEvent<Health> OnHit;


        protected override void OnValidTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Health health) && !health.IsDead)
            {
                Hit(health);
            }
        }

        protected virtual void Hit(Health health)
        {
            health.TakeDamage();
            OnHit?.Invoke(health);
        }
    }
}