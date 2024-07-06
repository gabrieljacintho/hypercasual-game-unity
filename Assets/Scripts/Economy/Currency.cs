using System;

namespace Bits.Economy
{
    [Serializable]
    public struct Currency
    {
        public CurrencyData Data;
        public float Value;

        public readonly string Id => Data != null ? Data.Id : string.Empty;


        public Currency(CurrencyData data, float value)
        {
            Data = data;
            Value = value;
        }
    }
}