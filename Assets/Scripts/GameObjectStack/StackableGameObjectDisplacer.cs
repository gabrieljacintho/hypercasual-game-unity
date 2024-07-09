using UnityEngine;

namespace Bits.GameObjectStack
{
    public class StackableGameObjectDisplacer : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _rotateSpeed = 180f;
        [SerializeField] private float _inertiaSpeed = 1f;
        [SerializeField] private Vector3 _upDirection = Vector3.up;
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private Vector3 _rotationOffset;

        private StackableGameObject _stackableGameObject;
        private Rigidbody _rigidbody;

        private Vector3 _lookPosition;
        //private Vector3 _lookPositionVelocity;

        private bool _started;
        private bool _targetPositionReached;
        private bool _targetRotationReached;

        public Vector3 UpDirection => transform.InverseTransformDirection(_upDirection);
        private GameObjectStack Stack => _stackableGameObject != null ? _stackableGameObject.GameObjectStack : null;


        private void Awake()
        {
            _stackableGameObject = GetComponentInParent<StackableGameObject>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_lookPosition, 0.1f);

            Vector3 direction = (_lookPosition - transform.position).normalized;
            Vector3 lookPosition = transform.position + direction * 2f;
            Gizmos.DrawLine(transform.position, lookPosition);
        }

        public void UpdateDisplacer()
        {
            if (_stackableGameObject == null || Stack == null)
            {
                return;
            }

            if (!_started)
            {
                OnStack();
                _started = true;
            }

            GetTargetPositionAndRotation(out Vector3 targetPosition, out Quaternion targetRotation);
            Vector3 position = UpdatePosition(targetPosition);
            Quaternion rotation = UpdateRotation(targetRotation);

            transform.SetPositionAndRotation(position, rotation);
        }

        private void OnStack()
        {
            _lookPosition = GetLookPosition(UpDirection);

            if (_rigidbody != null)
            {
                _rigidbody.isKinematic = true;
            }
        }

        private Vector3 UpdatePosition(Vector3 target)
        {
            target += Stack.transform.TransformVector(_positionOffset);
            Vector3 position;

            if (_targetPositionReached)
            {
                position = target;
            }
            else
            {
                position = Vector3.MoveTowards(transform.position, target, _moveSpeed * Time.deltaTime);
                if (position == target)
                {
                    _targetPositionReached = true;
                }
            }

            return position;
        }

        private Quaternion UpdateRotation(Quaternion target)
        {
            target *= Quaternion.Euler(_rotationOffset);
            Quaternion rotation;

            if (_targetRotationReached)
            {
                if (_stackableGameObject.gameObject == gameObject)
                {
                    rotation = GetInertiaRotation(target);
                }
                else
                {
                    rotation = target;
                }
            }
            else
            {
                rotation = Quaternion.RotateTowards(transform.rotation, target, _rotateSpeed * Time.deltaTime);
                if (rotation == target)
                {
                    _targetRotationReached = true;
                }
            }

            return rotation;
        }

        private Quaternion GetInertiaRotation(Quaternion target)
        {
            Vector3 targetLookPosition = GetLookPosition(GetTargetUpDirection());

            /*Vector3 velocity = targetLookPosition - _lookPosition;
            Vector3 acceleration = velocity - _lookPositionVelocity;

            _lookPositionVelocity += Time.deltaTime * acceleration;
            _lookPosition += _inertiaSpeedScale * Time.deltaTime * _lookPositionVelocity;*/

            _lookPosition = Vector3.MoveTowards(_lookPosition, targetLookPosition, _inertiaSpeed * Time.deltaTime);

            Vector3 upward = _lookPosition - transform.position;
            Vector3 forward = Vector3.Cross(transform.right, upward);

            Quaternion rotation = Quaternion.LookRotation(forward, _upDirection);
            rotation *= Quaternion.Euler(_rotationOffset);

            Vector3 eulerAngles = rotation.eulerAngles;
            rotation.eulerAngles = new Vector3(eulerAngles.x, target.eulerAngles.y, eulerAngles.z);
            rotation = Quaternion.RotateTowards(transform.rotation, rotation, _rotateSpeed * Time.deltaTime);

            return rotation;
        }

        private void GetTargetPositionAndRotation(out Vector3 position, out Quaternion rotation)
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

        private Vector3 GetTargetUpDirection()
        {
            if (_stackableGameObject.gameObject == gameObject)
            {
                return Stack.GetUpDirection(_stackableGameObject);
            }
            else
            {
                return transform.parent.up;
            }
        }

        private Vector3 GetLookPosition(Vector3 direction)
        {
            return transform.position + direction * _stackableGameObject.Size;
        }
    }
}