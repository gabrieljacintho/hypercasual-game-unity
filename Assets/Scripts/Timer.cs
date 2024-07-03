using UnityEngine;
using UnityEngine.Events;

namespace Bits
{
    public class Timer : MonoBehaviour
    {
        [Tooltip("Timer duration in seconds.")]
        [SerializeField] private float _duration;
        [SerializeField] private bool _startOnEnable;

        private bool _isRunning;
        private float _elapsedTime;

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

        private void OnEndTimer()
        {
            _isRunning = false;
            OnEnd?.Invoke();
        }
    }
}