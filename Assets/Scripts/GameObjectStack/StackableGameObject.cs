using Bits.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Bits.GameObjectStack
{
    public class StackableGameObject : MonoBehaviour
    {
        [SerializeField] private float _size = 1f;
        [SerializeField] private Vector3 _upDirection = Vector3.up;
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private Vector3 _rotationOffset;

        [Header("Transition Parameters")]
        [SerializeField] private float _speed = 1f;

        private GameObjectStack _stack;
        private bool _fixed;

        public float Size => _size;
        public Vector3 UpDirection => transform.InverseTransformDirection(_upDirection);

        [Space]
        public UnityEvent OnStack;


        private void LateUpdate()
        {
            if (_stack == null)
            {
                return;
            }

            _stack.GetPositionAndRotation(this, out Vector3 position, out Quaternion rotation);
            UpdatePosition(position);
            UpdateRotation(rotation);
        }

        public void Stack(GameObjectStack stack)
        {
            _stack = stack;

            gameObject.SetLayerRecursively(_stack.gameObject.layer);

            Rigidbody rigidbody = GetComponentInChildren<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.isKinematic = true;
            }

            OnStack?.Invoke();
        }

        private void UpdatePosition(Vector3 origin)
        {
            Vector3 position;

            if (_fixed)
            {
                position = origin + _stack.transform.TransformVector(_positionOffset);
            }
            else
            {
                position = Vector3.MoveTowards(transform.position, origin, _speed * Time.deltaTime);

                if (position == origin)
                {
                    _fixed = true;
                }
            }

            transform.position = position;
        }

        private void UpdateRotation(Quaternion origin)
        {
            transform.rotation = origin * Quaternion.Euler(_rotationOffset);
        }
    }
}