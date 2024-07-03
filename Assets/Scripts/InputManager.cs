using UnityEngine;
using UnityEngine.InputSystem;

namespace Bits
{
    public class InputManager : Singleton<InputManager>
    {
        [SerializeField] private InputActionReference _pointerPressInput;
        [SerializeField] private InputActionReference _pointerPositionInput;
        [SerializeField] private float _sensitivity = 1f;
        [SerializeField] private float _swipeResistance;

        private bool _isPressing = false;
        private Vector2 _startPointerPosition;
        private Vector2 _currentPointerPosition;
        private static Vector2 _swipeDirection;

        public static Vector2 SwipeDirection => _swipeDirection;


        protected override void Awake()
        {
            base.Awake();

            _pointerPressInput.action.performed += OnPointerPressPerformed;
            _pointerPressInput.action.canceled += OnPointerPressCanceled;
        }

        private void Update()
        {
            UpdateSwipe();
        }

        private void UpdateSwipe()
        {
            if (!_isPressing)
            {
                return;
            }

            _currentPointerPosition = GetPointerPosition();

            Vector2 delta = (_currentPointerPosition - _startPointerPosition) * _sensitivity;
            _swipeDirection = Vector2.zero;

            if (Mathf.Abs(delta.x) > _swipeResistance)
            {
                _swipeDirection.x = Mathf.Clamp(delta.x, -1f, 1f);
            }

            if (Mathf.Abs(delta.y) > _swipeResistance)
            {
                _swipeDirection.y = Mathf.Clamp(delta.y, -1f, 1f);
            }
        }

        private Vector2 GetPointerPosition()
        {
            return _pointerPositionInput.action.ReadValue<Vector2>();
        }

        private void OnPointerPressPerformed(InputAction.CallbackContext context)
        {
            _isPressing = true;
            _startPointerPosition = GetPointerPosition();
        }

        private void OnPointerPressCanceled(InputAction.CallbackContext context)
        {
            _isPressing = false;
            _swipeDirection = Vector2.zero;
        }
    }
}