using UnityEngine;
using UnityEngine.Events;

namespace Bits.Physics
{
    public class Trigger : TriggerComponent
    {
        [Space]
        public UnityEvent OnEnter;
        public UnityEvent<GameObject> OnEnterGameObject;
        public UnityEvent OnExit;
        public UnityEvent<GameObject> OnExitGameObject;


        protected override void OnValidTriggerEnter(Collider other)
        {
            OnEnter?.Invoke();
            OnEnterGameObject?.Invoke(other.gameObject);
        }

        protected override void OnValidTriggerExit(Collider other)
        {
            OnExit?.Invoke();
            OnExitGameObject?.Invoke(other.gameObject);
        }
    }
}