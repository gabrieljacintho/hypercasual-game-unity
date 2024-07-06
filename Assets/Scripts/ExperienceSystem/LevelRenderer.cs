using System.Collections.Generic;
using UnityEngine;

namespace Bits.ExperienceSystem
{
    [RequireComponent(typeof(Renderer))]
    public class LevelRenderer : MonoBehaviour
    {
        [SerializeField] private List<Material> _material;

        private Renderer _renderer;


        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void LateUpdate()
        {
            int index = ExperienceManager.Instance.ExperienceLevel;
            index = Mathf.Min(index, _material.Count - 1);

            _renderer.material = _material[index];
        }
    }
}