using Bits.Extensions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Bits
{
    public class TransformRestorer : MonoBehaviour
    {
        public bool restoreOnEnable;

        [Space]
        public UnityEvent onRestore;

        private Dictionary<Transform, TransformValues> _defaultTransformValues;


        private void Awake()
        {
            CacheValues();
        }

        private void OnEnable()
        {
            if (restoreOnEnable)
            {
                Restore();
            }
        }

        public void CacheValues()
        {
            _defaultTransformValues = transform.AllValues();
        }

        public void Restore()
        {
            _defaultTransformValues?.LoadValues();

            onRestore?.Invoke();
        }
    }
}