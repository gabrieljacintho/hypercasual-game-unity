using UnityEngine;

namespace Bits
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterGravity : MonoBehaviour
    {
        [SerializeField] private float _gravityScaleOnAir = 1f;
        [SerializeField] private float _gravityScaleOnGround = 0.1f;

        [Header("Ground Check Parameters")]
        [Tooltip("If null the GameObject Transform is used.")]
        [SerializeField] private Transform _originTransform;
        [SerializeField] private float _radius = 0.2f;
        [SerializeField] private float _maxDistance = 0.4f;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private QueryTriggerInteraction _triggerInteraction;


        private CharacterController _characterController;
        private Vector3 _velocity = Vector3.zero;


        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            ResetVelocity();
        }

        private void FixedUpdate()
        {
            float gravity = UnityEngine.Physics.gravity.y;

            if (IsGrounded())
            {
                if (_velocity.y <= 0f)
                {
                    _velocity.y = gravity * _gravityScaleOnGround;
                }
            }
            else
            {
                _velocity.y += gravity * _gravityScaleOnAir * Time.fixedDeltaTime;
            }

            _characterController.Move(_velocity * Time.fixedDeltaTime);
        }

        public void ResetVelocity()
        {
            _velocity = Vector3.zero;
        }

        private bool IsGrounded()
        {
            Transform originTransform = _originTransform != null ? _originTransform : transform;
            Ray ray = new Ray(originTransform.position, -originTransform.up);

            return UnityEngine.Physics.SphereCast(ray, _radius, _maxDistance, _layerMask, _triggerInteraction);
        }
    }
}