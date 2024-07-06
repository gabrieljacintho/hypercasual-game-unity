using TMPro;
using UnityEngine;

namespace Bits.GameObjectStack
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class GameObjectStackMaxItemsText : MonoBehaviour
    {
        [SerializeField] private GameObjectStack _stack;

        private TextMeshProUGUI _text;


        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void LateUpdate()
        {
            _text.text = _stack.Count + "/" + _stack.MaxItems;
        }
    }
}