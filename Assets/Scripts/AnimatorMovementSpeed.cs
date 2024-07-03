using UnityEngine;

namespace Bits
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorMovementSpeed : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private string _movementSpeedPropertyName = "MovementSpeed";
        [SerializeField] private float _transitionSmoothTime = 0.3f;

        private Animator _animator;

        private int _movementSpeedPropertyId;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _movementSpeedPropertyId = Animator.StringToHash(_movementSpeedPropertyName);
        }

        private void LateUpdate()
        {
            float currentSpeed = _playerMovement.CurrentVelocity.magnitude;
            float maxSpeed = _playerMovement.Speed;
            float targetMovementAmount = Mathf.Clamp01(currentSpeed / maxSpeed);

            float movementAmount = _animator.GetFloat(_movementSpeedPropertyId);

            
        }
    }
}