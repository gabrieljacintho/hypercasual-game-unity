using UnityEngine;

namespace Bits
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorMovement : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private string _movementAmountPropertyName = "MovementAmount";
        [SerializeField] private string _movementSpeedPropertyName = "MovementSpeed";
        [SerializeField] private float _maxMovementAmountThreshold = 0.9f;
        [SerializeField] private float _transitionSmoothTime = 0.3f;

        private Animator _animator;

        private int _movementAmountPropertyId;
        private int _movementSpeedPropertyId;

        private float _currentMovementAmount;
        private float _currentTransitionVelocity;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _movementAmountPropertyId = Animator.StringToHash(_movementAmountPropertyName);
            _movementSpeedPropertyId = Animator.StringToHash(_movementSpeedPropertyName);
        }

        private void LateUpdate()
        {
            float currentSpeed = _playerMovement.CurrentVelocity.magnitude;
            float maxSpeed = _playerMovement.Speed;
            float targetMovementAmount = Mathf.Clamp01(currentSpeed / maxSpeed);

            UpdateMovementAmount(targetMovementAmount);
            UpdateMovementSpeed(targetMovementAmount);
        }

        private void UpdateMovementAmount(float target)
        {
            if (target > 0f)
            {
                target = target >= _maxMovementAmountThreshold ? 1f : 0.5f;
            }

            if (_transitionSmoothTime > 0f)
            {
                _currentMovementAmount = Mathf.SmoothDamp(_currentMovementAmount, target,
                    ref _currentTransitionVelocity, _transitionSmoothTime, Mathf.Infinity, Time.deltaTime);
            }
            else
            {
                _currentMovementAmount = target;
            }

            _animator.SetFloat(_movementAmountPropertyId, _currentMovementAmount);
        }

        private void UpdateMovementSpeed(float targetMovementAmount)
        {
            float t = Mathf.Clamp01(targetMovementAmount / 0.5f);
            float movementSpeed = Mathf.Lerp(0f, 1f, t);

            _animator.SetFloat(_movementSpeedPropertyId, movementSpeed);
        }
    }
}