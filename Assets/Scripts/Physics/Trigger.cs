using UnityEngine;
using UnityEngine.Events;

namespace Bits.Physics
{
    public class Trigger : TriggerComponent
    {
        [Space]
        public UnityEvent OnEnter;


        protected override void OnValidTriggerEnter(Collider other)
        {
            OnEnter?.Invoke();
        }
    }
}