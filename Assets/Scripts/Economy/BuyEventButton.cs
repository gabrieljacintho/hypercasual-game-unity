using UnityEngine;
using UnityEngine.UI;

namespace Bits.Economy
{
    [RequireComponent(typeof(Button))]
    public class BuyEventButton : BuyEvent
    {
        private Button _button;


        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Buy);
        }

        private void LateUpdate()
        {
            _button.interactable = CanBuy();
        }
    }
}