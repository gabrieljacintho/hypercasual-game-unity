using Bits.Extensions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Bits
{
    public class EventsInvoker : MonoBehaviour
    {
        [SerializeField] private List<KeyValue<string, UnityEvent>> _events;


        public void InvokeEvent(string key)
        {
            foreach (KeyValue<string, UnityEvent> keyEvent in _events)
            {
                if (keyEvent.Key == key)
                {
                    keyEvent.Value?.Invoke();
                }
            }
        }
    }
}