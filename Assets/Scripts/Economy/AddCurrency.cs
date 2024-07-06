using UnityEngine;

namespace Bits.Economy
{
    public class AddCurrency : MonoBehaviour
    {
        [SerializeField] private CurrencyData _currency;


        public void Add(float value)
        {
            EconomyManager.Instance.AddCurrency(_currency, value);
        }

        public void Remove(float value)
        {
            EconomyManager.Instance.RemoveCurrency(_currency, value);
        }
    }
}