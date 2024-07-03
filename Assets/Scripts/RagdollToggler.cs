using UnityEngine;

namespace Bits
{
    public class RagdollToggler : MonoBehaviour
    {
        [SerializeField] private bool _startEnabled;
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;

        private Rigidbody[] _rigidbodies;


        private void Start()
        {
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
            if (_rigidbodies == null)
            {
                _rigidbodies = GetComponentsInChildren<Rigidbody>();
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
    }
}