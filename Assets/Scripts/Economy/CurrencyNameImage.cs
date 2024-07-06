using UnityEngine;
using UnityEngine.UI;

namespace Bits.Economy
{
    [RequireComponent(typeof(Image))]
    public class CurrencyNameImage : MonoBehaviour
    {
        [SerializeField] private string _currencyId;

        private Image _image;


        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void LateUpdate()
        {
            Currency currency = EconomyManager.Instance.GetCurrency(_currencyId);
            Sprite icon = currency.Data != null ? currency.Data.Icon : null;

            _image.sprite = icon;
        }
    }
}