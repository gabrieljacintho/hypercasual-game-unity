using UnityEngine;

namespace Bits
{
    public class SetActiveChildrenColliders : MonoBehaviour
    {
        private Collider[] _colliders;


        private void Awake()
        {
            _colliders = GetComponentsInChildren<Collider>();
        }

        public void EnableColliders()
        {
            SetActiveColliders(true);
        }

        public void DisableColliders()
        {
            SetActiveColliders(false);
        }

        public void SetActiveColliders(bool value)
        {
            foreach (Collider collider in _colliders)
            {
                collider.enabled = value;
            }
        }
    }
}