using Bits.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Bits.GameObjectStack
{
    public class StackableGameObject : MonoBehaviour
    {
        [SerializeField] private float _size = 1f;

        private List<StackableGameObjectDisplacer> _displacers;
        private GameObjectStack _stack;

        public float Size => _size;
        public List<StackableGameObjectDisplacer> Displacers
        {
            get
            {
                if (_displacers == null)
                {
                    _displacers = GetComponentsInChildren<StackableGameObjectDisplacer>().ToList();
                }

                return _displacers;
            }
        }
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