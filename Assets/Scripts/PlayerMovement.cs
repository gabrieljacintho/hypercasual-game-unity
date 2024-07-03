using UnityEngine;

namespace Bits
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [Tooltip("Movement speed in meters per second.")]
        [SerializeField] private float _moveSpeed = 1f;
        [Tooltip("Rotate speed in meters per second.")]
        [SerializeField] private float _rotateSpeed = 1f;

        private CharacterController _characterController;

        private Vector3 _currentVelocity;
        private Quaternion _targetRotation;

        public float Speed
        {
            get => _moveSpeed;
            set => _moveSpeed = value;
        }
        public Vector3 CurrentVelocity => _currentVelocity;


        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Vector3 delta = InputManager.SwipeDirection;
            delta = new Vector3(delta.x, 0f, delta.y);

            UpdatePosition(delta);
            UpdateRotation(delta);
        }

        private void UpdatePosition(Vector3 delta)
        {
            _currentVelocity = _moveSpeed * delta;
            _characterController.Move(_currentVelocity * Time.deltaTime);
        }

        private void UpdateRotation(Vector3 delta)
        {
            if (delta.magnitude > 0f)
            {
                _targetRotation = Quaternion.LookRotation(delta, transform.up);
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotateSpeed * Time.deltaTime);
        }
    }
}
