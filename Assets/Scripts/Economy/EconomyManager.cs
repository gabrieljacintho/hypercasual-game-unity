using Bits.Patterns;
using System.Collections.Generic;
using UnityEngine;

namespace Bits.Economy
{
    public class EconomyManager : Singleton<EconomyManager>
    {
        [SerializeField] private List<Currency> _initialCurrencies = new List<Currency>();
        [SerializeField] private List<Currency> _currencies = new List<Currency >();


        protected override void Awake()
        {
            base.Awake();
            _currencies = _initialCurrencies;
        }

        public void AddCurrency(string id, float value)
        {
            bool Match(Currency currency) => currency.Id == id;

            if (!_currencies.Exists(Match))
            {
                Debug.LogError("Currency not found! (\"" + id + "\")", gameObject);
                return;
            }

            int index = _currencies.FindIndex(Match);
            Currency currency = _currencies[index];
            currency.Value = Mathf.Max(currency.Value + value, 0);

            _currencies[index] = currency;
        }

        public void AddCurrency(CurrencyData data, float value = 0f)
        {
            bool Match(Currency currency) => currency.Id == data.Id;

            if (_currencies.Exists(Match))
            {
                AddCurrency(data.Id, value);
                return;
            }
            else if (data != null)
            {
                Currency currency = new Currency(data, value);
                _currencies.Add(currency);
            }
            else
            {
                Debug.LogError("Currency Data is null!", gameObject);
            }
        }

        public void RemoveCurrency(string id, float value)
        {
            AddCurrency(id, -value);
        }

        public void RemoveCurrency(CurrencyData data, float value)
        {
            RemoveCurrency(data.Id, value);
        }

        public void RemoveCurrency(Currency currency)
        {
            RemoveCurrency(currency.Id, currency.Value);
        }

        public Currency GetCurrency(string id)
        {
            return _currencies.Find(currency => currency.Id == id);
        }

        public void CleanCurrency(string id)
        {
            bool Match(Currency currency) => currency.Id == id;

            if (_currencies.Exists(Match))
            {
                int index = _currencies.FindIndex(Match);
                _currencies.RemoveAt(index);
            }
        }

        public void CleanAllCurrencies()
        {
            _currencies.Clear();
        }

        public bool CanBuy(string currencyId, float price)
        {
            float value = GetCurrency(currencyId).Value;
            return price < value;
        }
    }
}