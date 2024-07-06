using Bits.ExperienceSystem;
using UnityEngine;

namespace Bits.GameObjectStack
{
    [RequireComponent(typeof(GameObjectStack))]
    public class LevelGameObjectStack : MonoBehaviour
    {
        [SerializeField] private int _maxItemsIncreasePerLevel = 1;

        private GameObjectStack _stack;
        private int _initialStackMaxItems;

        
        private void Awake()
        {
            _stack = GetComponent<GameObjectStack>();
            _initialStackMaxItems = _stack.MaxItems;
        }

        private void LateUpdate()
        {
            int level = ExperienceManager.Instance.ExperienceLevel;

            _stack.MaxItems = _initialStackMaxItems + (level * _maxItemsIncreasePerLevel);
        }
    }
}