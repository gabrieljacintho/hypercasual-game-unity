using UnityEngine;

namespace Bits.Animation
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _maxSpeed = 1f;
        [SerializeField] private string _movementAmountPropertyName = "MovementAmount";
        [SerializeField] private string _movementSpeedPropertyName = "MovementSpeed";
        [SerializeField] private float _maxMovementAmountThreshold = 0.9f;
        [SerializeField] private float _transitionSpeed = 1f;

        private Animator _animator;

        private int _movementAmountPropertyId;
        private int _movementSpeedPropertyId;

        private float _currentMovementAmount;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _movementAmountPropertyId = Animator.StringToHash(_movementAmountPropertyName);
            _movementSpeedPropertyId = Animator.StringToHash(_movementSpeedPropertyName);
        }

        private void LateUpdate()
        {
            float currentSpeed = _characterController.velocity.magnitude;
            float targetMovementAmount = Mathf.Clamp01(currentSpeed / _maxSpeed);

            UpdateMovementAmount(targetMovementAmount);
            UpdateMovementSpeed(targetMovementAmount);
        }

        private void UpdateMovementAmount(float target)
        {
            if (target > 0f)
            {
                target = target >= _maxMovementAmountThreshold ? 1f : 0.5f;
            }

            _currentMovementAmount = Mathf.MoveTowards(_currentMovementAmount, target, _transitionSpeed * Time.deltaTime);

            _animator.SetFloat(_movementAmountPropertyId, _currentMovementAmount);
        }

        private void UpdateMovementSpeed(float targetMovementAmount)
        {
            float movementSpeed;
            if (targetMovementAmount > 0f)
            {
                float t = Mathf.Clamp01(targetMovementAmount / 0.5f);
                movementSpeed = Mathf.Lerp(0f, 1f, t);
            }
            else
            {
                movementSpeed = 1f;
            }

            _animator.SetFloat(_movementSpeedPropertyId, movementSpeed);
        }
    }
}