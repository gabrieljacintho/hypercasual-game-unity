using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Bits.Physics
{
    public abstract class GenericDetector<T> : TriggerComponent where T : Component
    {
        protected List<T> _detectedComponents = new List<T>();

        private bool _isDetecting;

        public List<T> DetectedComponents => new List<T>(_detectedComponents);

        public bool IsDetecting => _isDetecting;

        [Space]
        public UnityEvent<T> OnDetectComponent;
        public UnityEvent OnNotDetectComponent;


        protected override void OnValidTriggerEnter(Collider other)
        {
            T component = GetComponent(other);
            if (CanDetectComponent(component))
            {
                AddComponent(component);
            }
        }

        protected override void OnValidTriggerStay(Collider other)
        {
            T component = GetComponent(other);
            if (CanDetectComponent(component))
            {
                AddComponent(component);
            }
            else
            {
                RemoveComponent(component);
            }
        }

        protected override void OnValidTriggerExit(Collider other)
        {
            T component = GetComponent(other);
            if (component != null)
            {
                RemoveComponent(component);
            }
        }

        private void AddComponent(T component)
        {
            if (!_detectedComponents.Contains(component))
            {
                _detectedComponents.Add(component);
            }

            if (!_isDetecting && _detectedComponents.Count > 0)
            {
                _isDetecting = true;
                OnDetect(component);
                OnDetectComponent?.Invoke(component);
            }
        }

        private void RemoveComponent(T component)
        {
            if (_detectedComponents.Contains(component))
            {
                _detectedComponents.Remove(component);
            }

            if (_isDetecting && _detectedComponents.Count == 0)
            {
                _isDetecting = false;
                OnNotDetect();
                OnNotDetectComponent?.Invoke();
            }
        }

        protected virtual T GetComponent(Collider other)
        {
            return other.GetComponentInChildren<T>();
        }

        protected virtual bool CanDetectComponent(T component)
        {
            return component != null;
        }

        protected virtual void OnDetect(T component) { }

        protected virtual void OnNotDetect() { }
    }
}