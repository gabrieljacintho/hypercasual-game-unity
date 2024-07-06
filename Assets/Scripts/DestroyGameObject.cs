using UnityEngine;

namespace Bits
{
    public class DestroyGameObject : MonoBehaviour
    {
        [Tooltip("If null the component GameObject is used.")]
        [SerializeField] private GameObject _target;
        [Tooltip("Destroy delay in seconds.")]
        [SerializeField] private float _delay;
        [SerializeField] private bool _startOnAwake = true;

        private bool _started;

        public GameObject Target
        {
            get => _target != null ? _target : gameObject;
            set => _target = value;
        }


        private void Awake()
        {
            if (_startOnAwake)
            {
                Destroy();
            }
        }

        public void Destroy(float delay)
        {
            if (_started)
            {
                return;
            }

            _started = true;
            Destroy(Target, delay);
        }

        public void Destroy()
        {
            Destroy(_delay);
        }

        public void Destroy(GameObject target)
        {
            Destroy(target, _delay);
        }
    }
}