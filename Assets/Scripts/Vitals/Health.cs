using UnityEngine;
using UnityEngine.Events;

namespace Bits.Vitals
{
    public class Health : MonoBehaviour
    {
        private bool _isDead;

        public bool IsDead => _isDead;

        public UnityEvent OnTakeDamage;


        public void TakeDamage()
        {
            _isDead = true;
            OnTakeDamage?.Invoke();
        }
    }
}