using UnityEngine;
using UnityEngine.Events;

namespace Bits.GameObjectStack
{
    public class PopGameObjectFromStack : MonoBehaviour
    {
        [SerializeField] private StackDetector _stackDetector;

        [Space]
        public UnityEvent<StackableGameObject> OnPop;
        public UnityEvent<GameObject> OnPopGameObject;


        public StackableGameObject Pop()
        {
            if (!_stackDetector.IsDetecting)
            {
                return null;
            }

            GameObjectStack stack = _stackDetector.DetectedComponents[0];
            if (stack == null)
            {
                return null;
            }

            StackableGameObject stackableGameObject = stack.Pop();

            OnPop?.Invoke(stackableGameObject);
            OnPopGameObject?.Invoke(stackableGameObject.gameObject);

            return stackableGameObject;
        }

        // Allows the method to be called by Unity event
        public void InvokePop()
        {
            Pop();
        }
    }
}