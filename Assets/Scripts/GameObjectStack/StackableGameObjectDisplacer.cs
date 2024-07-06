using UnityEngine;

namespace Bits.GameObjectStack
{
    public class StackableGameObjectDisplacer : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _rotateSpeed = 180f;
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private Vector3 _rotationOffset;

        private StackableGameObject _stackableGameObject;
        private Rigidbody _rigidbody;

        private bool _targetPositionReached;
        private bool _targetRotationReached;

        private GameObjectStack Stack => _stackableGameObject != null ? _stackableGameObject.GameObjectStack : null;


        private void Awake()
        {
            _stackableGameObject = GetComponentInParent<StackableGameObject>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void LateUpdate()
        {
            if (_stackableGameObject == null || Stack == null)
            {
                return;
            }

            if (_rigidbody != null)
            {
                _rigidbody.isKinematic = true;
            }

            GetPositionAndRotation(out Vector3 position, out Quaternion rotation);
            UpdatePosition(position);
            UpdateRotation(rotation);
        }

        private void UpdatePosition(Vector3 origin)
        {
            Vector3 targetPosition = origin + Stack.transform.TransformVector(_positionOffset);
            Vector3 position;

            if (_targetPositionReached)
            {
                position = targetPosition;
            }
            else
            {
                position = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.deltaTime);
                if (position == targetPosition)
                {
                    _targetPositionReached = true;
                }
            }

            transform.position = position;
        }

        private void UpdateRotation(Quaternion origin)
        {
            Quaternion targetRotation = origin * Quaternion.Euler(_rotationOffset);
            Quaternion rotation;

            if (_targetRotationReached)
            {
                rotation = targetRotation;
            }
            else
            {
                rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
                if (rotation == targetRotation)
                {
                    _targetRotationReached = true;
                }
            }

            transform.rotation = rotation;
        }

        private void GetPositionAndRotation(out Vector3 position, out Quaternion rotation)
        {
            if (_stackableGameObject.gameObject == gameObject)
            {
                Stack.GetPositionAndRotation(_stackableGameObject, out position, out rotation);
            }
            else
            {
                position = transform.parent.position;
                rotation = transform.parent.rotation;
            }
        }
    }
}