using Bits.Extensions;
using Bits.Vitals;
using UnityEngine;

namespace Bits.GameObjectStack
{
    public class PushGameObjectToStack : MonoBehaviour
    {
        [SerializeField] private GameObjectStack _targetStack;
        [SerializeField] private float _defaultDelay;


        public void Push(GameObject target)
        {
            Push(target, _defaultDelay);
        }

        public void PushIfDead(Health health)
        {
            if (health.IsDead)
            {
                Push(health.gameObject, _defaultDelay);
            }
        }

        private void Push(StackableGameObject target, float delay)
        {
            void Push()
            {
                _targetStack.Push(target);
            }

            if (delay > 0f)
            {
                this.DOAfterSeconds(Push, delay);
            }
            else
            {
                Push();
            }
        }

        private void Push(GameObject target, float delay)
        {
            if (target.TryGetComponent(out StackableGameObject stackableGameObject))
            {
                Push(stackableGameObject, delay);
            }
        }
    }
}