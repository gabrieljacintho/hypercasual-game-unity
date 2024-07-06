using UnityEngine;

namespace Bits.Physics
{
    public class ColliderDetector : GenericDetector<Collider>
    {
        protected override Collider GetComponent(Collider other)
        {
            return other;
        }
    }
}