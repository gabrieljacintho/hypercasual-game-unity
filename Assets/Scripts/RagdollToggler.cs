using Bits.Extensions;
using UnityEngine;

namespace Bits
{
    public class RagdollToggler : MonoBehaviour
    {
        [SerializeField] private bool _startEnabled;
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;

        private Rigidbody[] _rigidbodies;
        private TransformValues _defaultTransformValues;


        private void Start()
        {
            LoadReferences();
            SetRagdoll(_startEnabled);
        }

        public void EnableRagdoll()
        {
            SetRagdoll(true);
        }

        public void DisableRagdoll()
        {
            SetRagdoll(false);
        }

        public void SetRagdoll(bool enabled)
        {
            if (!enabled)
            {
                ResetTransformValues();
            }

            foreach (Rigidbody rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = !enabled;
            }

            if (_animator != null)
            {
                _animator.enabled = !enabled;
            }

            if (_characterController != null)
            {
                _characterController.enabled = !enabled;
            }
        }

        public void ResetTransformValues()
        {
            if (_rigidbodies[0].transform == transform)
            {
                return;
            }

            transform.position = _rigidbodies[0].transform.position - _defaultTransformValues.Position;

            _rigidbodies[0].transform.SetValues(_defaultTransformValues);
        }

        private void LoadReferences()
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
            _defaultTransformValues = _rigidbodies[0].transform.Values();
        }
    }
}