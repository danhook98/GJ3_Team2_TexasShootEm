using UnityEngine;

namespace TexasShootEm.EventSystem
{
    public class AbstractEventTrigger<T> : MonoBehaviour
    {
        [SerializeField] protected AbstractEvent<T> eventToTrigger;
        
        public void Trigger(T value) => eventToTrigger.Invoke(value);
    }
}