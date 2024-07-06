using UnityEngine;
using UnityEngine.Events;

namespace Bits.Economy
{
    public class BuyEvent : MonoBehaviour
    {
        [SerializeField] private string _currencyId;
        [SerializeField] private float _price;

        [Space]
        public UnityEvent OnBuy;


        public void Buy()
        {
            if (!CanBuy())
            {
                return;
            }

            EconomyManager.Instance.RemoveCurrency(_currencyId, _price);

            OnBuy?.Invoke();
        }

        protected bool CanBuy()
        {
            return EconomyManager.Instance != null && EconomyManager.Instance.CanBuy(_currencyId, _price);
        }
    }
}