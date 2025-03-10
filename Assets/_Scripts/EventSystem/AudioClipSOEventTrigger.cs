using UnityEngine;

namespace TexasShootEm.EventSystem
{
    public class AudioClipSOEventTrigger : AbstractEventTrigger<AudioClipSO>
    {
        [SerializeField] private AudioClipSO audioClip;
        public void Trigger() => eventToTrigger.Invoke(audioClip);
    }
}   