using Bits.Extensions;
using UnityEngine;

namespace Bits.Physics
{
    public abstract class TriggerComponent : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;


        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!CanDetect(other))
            {
                return;
            }

            OnValidTriggerEnter(other);
        }

        protected virtual bool CanDetect(Collider other)
        {
            return _layerMask.Contains(other.gameObject.layer);
        }

        protected virtual void OnValidTriggerEnter(Collider other) { }
    }
}