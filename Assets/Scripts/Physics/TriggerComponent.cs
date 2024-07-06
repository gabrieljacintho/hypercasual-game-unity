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

        protected virtual void OnTriggerStay(Collider other)
        {
            if (!CanDetect(other))
            {
                return;
            }

            OnValidTriggerStay(other);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (!CanDetect(other))
            {
                return;
            }

            OnValidTriggerExit(other);
        }

        protected virtual bool CanDetect(Collider other)
        {
            return _layerMask.Contains(other.gameObject.layer);
        }

        protected virtual void OnValidTriggerEnter(Collider other) { }

        protected virtual void OnValidTriggerStay(Collider other) { }

        protected virtual void OnValidTriggerExit(Collider other) { }
    }
}