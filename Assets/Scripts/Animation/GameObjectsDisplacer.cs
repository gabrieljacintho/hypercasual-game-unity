using System.Collections.Generic;
using UnityEngine;

namespace Bits.Animation
{
    public class GameObjectsDisplacer : MonoBehaviour
    {
        [Tooltip("If null the component Transform is used.")]
        [SerializeField] private Transform _target;
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _rotateSpeed = 360f;

        private Dictionary<GameObject, Rigidbody> _rigidbodyByGameObject = new Dictionary<GameObject, Rigidbody>();

        private Vector3 TargetPosition => _target != null ? _target.position : transform.position;
        private Quaternion TargetRotation => _target != null ? _target.rotation : transform.rotation;


        private void Update()
        {
            List<GameObject> targets = new List<GameObject>(_rigidbodyByGameObject.Keys);
            foreach (GameObject target in targets)
            {
                UpdateGameObject(target);
            }
        }

        public void AddGameObject(GameObject target)
        {
            if (_rigidbodyByGameObject.ContainsKey(target))
            {
                return;
            }

            Rigidbody rigidbody = target.GetComponentInChildren<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.isKinematic = true;
            }

            _rigidbodyByGameObject.Add(target, rigidbody);
        }

        private void RemoveGameObject(GameObject target)
        {
            if (!_rigidbodyByGameObject.ContainsKey(target))
            {
                return;
            }

            Rigidbody rigidbody = _rigidbodyByGameObject[target];
            if (rigidbody != null)
            {
                rigidbody.isKinematic = false;
            }

            _rigidbodyByGameObject.Remove(target);
        }

        private void UpdateGameObject(GameObject target)
        {
            Transform targetTransform = target.transform;
            UpdatePosition(targetTransform);
            UpdateRotation(targetTransform);

            if (targetTransform.position == TargetPosition)
            {
                RemoveGameObject(target);
            }
        }

        private void UpdatePosition(Transform target)
        {
            Vector3 position = target.position;
            position = Vector3.MoveTowards(position, TargetPosition, _moveSpeed * Time.deltaTime);

            target.transform.position = position;
        }

        private void UpdateRotation(Transform target)
        {
            Quaternion rotation = target.rotation;
            rotation = Quaternion.RotateTowards(rotation, TargetRotation, _rotateSpeed * Time.deltaTime);

            target.transform.rotation = rotation;
        }
    }
}