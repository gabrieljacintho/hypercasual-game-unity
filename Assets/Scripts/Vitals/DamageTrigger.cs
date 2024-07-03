using Bits.Physics;
using UnityEngine;

namespace Bits.Vitals
{
    public class DamageTrigger : Trigger
    {
        protected override bool CanDetect(Collider other)
        {
            if (!base.CanDetect(other))
            {
                return false;
            }

            Health health = other.GetComponent<Health>();

            return health != null && !health.IsDead;
        }
    }
}