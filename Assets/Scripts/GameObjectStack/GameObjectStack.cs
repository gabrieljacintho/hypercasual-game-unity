using System.Collections.Generic;
using UnityEngine;

namespace Bits.GameObjectStack
{
    public class GameObjectStack : MonoBehaviour
    {
        [Tooltip("Set negative to not limit.")]
        [SerializeField] private int _maxItems = -1;

        private Stack<StackableGameObject> _gameObjects = new Stack<StackableGameObject>();
#if UNITY_EDITOR
        // Variable to show in inspector (debug)
        private List<StackableGameObject> _gameObjectsList;
#endif

        public int MaxItems
        {
            get => _maxItems;
            set => _maxItems = value;
        }
        public int Count => _gameObjects.Count;


        private void LateUpdate()
        {
            foreach (StackableGameObject stackableGameObject in _gameObjects)
            {
                foreach (StackableGameObjectDisplacer displacer in stackableGameObject.Displacers)
                {
                    displacer.UpdateDisplacer();
                }
            }
#if UNITY_EDITOR
            _gameObjectsList = new List<StackableGameObject>(_gameObjects);
#endif
        }

        public void Push(StackableGameObject target)
        {
            if (_maxItems >= 0 && _gameObjects.Count >= _maxItems)
            {
                return;
            }

            _gameObjects.Push(target);
            target.OnStack(this);
        }

        public StackableGameObject Pop()
        {
            if (_gameObjects.Count == 0)
            {
                return null;
            }

            StackableGameObject target = _gameObjects.Pop();
            target.OnUnstack();

            return target;
        }

        public Transform GetPivot(StackableGameObject target, out StackableGameObject stackableGameObject)
        {
            bool targetFound = false;
            StackableGameObject[] gameObjectsArray = _gameObjects.ToArray();

            for (int i = 0; i < gameObjectsArray.Length; i++)
            {
                if (gameObjectsArray[i] == target)
                {
                    targetFound = true;
                    continue;
                }

                if (!targetFound)
                {
                    continue;
                }

                stackableGameObject = gameObjectsArray[i];
                return stackableGameObject.transform;
            }

            stackableGameObject = null;
            return transform;
        }

        public Vector3 GetPosition(StackableGameObject target)
        {
            Transform pivot = GetPivot(target, out StackableGameObject stackableGameObject);
            Vector3 position = pivot.position;

            if (stackableGameObject != null)
            {
                position += pivot.up * stackableGameObject.Size;
            }

            return position;
        }

        public Quaternion GetRotation(StackableGameObject target)
        {
            return GetPivot(target, out StackableGameObject stackableGameObject).rotation;
        }

        public void GetPositionAndRotation(StackableGameObject target, out Vector3 position, out Quaternion rotation)
        {
            Transform pivot = GetPivot(target, out StackableGameObject stackableGameObject);

            position = pivot.position;
            rotation = pivot.rotation;

            if (stackableGameObject != null)
            {
                position += pivot.up * stackableGameObject.Size;
            }
        }

        public Vector3 GetUpDirection(StackableGameObject target)
        {
            return GetPivot(target, out StackableGameObject stackableGameObject).up;
        }
    }
}