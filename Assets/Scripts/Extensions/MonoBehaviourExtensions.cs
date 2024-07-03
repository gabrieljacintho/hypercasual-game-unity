using System;
using System.Collections;
using UnityEngine;

namespace Bits.Extensions
{
    public static class MonoBehaviourExtensions
    {
        public static void DOAfterSeconds(this MonoBehaviour monoBehaviour, Action action, float seconds)
        {
            IEnumerator Routine()
            {
                yield return new WaitForSeconds(seconds);

                action?.Invoke();
            }

            monoBehaviour.StartCoroutine(Routine());
        }
    }
}
