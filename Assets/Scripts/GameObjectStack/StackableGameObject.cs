using Bits.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Bits.GameObjectStack
{
    public class StackableGameObject : MonoBehaviour
    {
        [SerializeField] private float _size = 1f;
        [SerializeField] private Vector3 _upDirection = Vector3.up;
        
        private GameObjectStack _stack;

        public float Size => _size;
        public Vector3 UpDirection => transform.InverseTransformDirection(_upDirection);
        public GameObjectStack GameObjectStack => _stack;

        [Space]
        public UnityEvent OnStackEvent;
        public UnityEvent OnUnstackEvent;


        public void OnStack(GameObjectStack stack)
        {
            _stack = stack;

            gameObject.SetLayerRecursively(_stack.gameObject.layer);

            OnStackEvent?.Invoke();
        }

        public void OnUnstack()
        {
            _stack = null;

            // TODO: SetLayerRecursively to default

            OnUnstackEvent?.Invoke();
        }
    }
}