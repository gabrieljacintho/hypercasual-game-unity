using UnityEngine;

namespace Bits.Extensions
{
    public static class LayerMaskExtensions
    {
        public static bool Contains(this LayerMask layerMask, int layer)
        {
            return layerMask.value == (layerMask.value | (1 << layer));
        }
    }
}