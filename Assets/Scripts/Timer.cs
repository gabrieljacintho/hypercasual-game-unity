using UnityEngine;
using UnityEngine.Events;

namespace Bits
{
    public class Timer : MonoBehaviour
    {
        [Tooltip("Timer duration in seconds.")]
        [SerializeField] private float _duration = 1f;
        [SerializeField] private bool _loop;
        [SerializeField] private bool _startOnEnable;

        private bool _isRunning;
        private float _elapsedTime;

        public bool IsRunning => _isRunning;
        public float ElapsedTime => _elapsedTime;
        public float Duration => _duration;
        public float Progress => Mathf.Clamp01(_elapsedTime / _duration);

        [Space]
        public UnityEvent OnStart;
        public UnityEvent OnEnd;


        private void OnEnable()
        {
            if (_startOnEnable)
            {
                StartTimer();
            }
        }

        private void Update()
        {
            if (!_isRunning)
            {
                return;
            }

            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _duration)
            {
                OnEndTimer();
            }
        }

        public void StartTimer()
        {
            _elapsedTime = 0f;
            _isRunning = true;

            OnStart?.Invoke();
        }

        public void StopTimer()
        {
            _elapsedTime = 0f;
            _isRunning = false;
        }

        private void OnEndTimer()
        {
            _elapsedTime = 0f;
            _isRunning = _loop;

            OnEnd?.Invoke();
        }
    }
}