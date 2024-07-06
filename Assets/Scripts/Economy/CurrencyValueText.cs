using TMPro;
using UnityEngine;

namespace Bits.Economy
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class CurrencyValueText : MonoBehaviour
    {
        [SerializeField] private string _currencyId;
        [SerializeField] private string _format = "F0";

        private TextMeshProUGUI _text;


        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void LateUpdate()
        {
            Currency currency = EconomyManager.Instance.GetCurrency(_currencyId);

            _text.text = currency.Value.ToString(_format);
        }
    }
}