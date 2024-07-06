using UnityEngine;

namespace Bits.Economy
{
    [CreateAssetMenu(menuName = "67 Bits/Currency Data", fileName = "New Currency Data")]
    public class CurrencyData : ScriptableObject
    {
        public string Id;
        public string Name;
        public Sprite Icon;
    }
}