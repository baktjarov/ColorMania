using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public class AudioService : MonoBehaviour
    {
        [SerializeField] private List<AudioSource> _audioSources = new();

        public void PlayAudio(AudioClip audioClip)
        {
            AudioSource audioSource = null;

            foreach (AudioSource source in _audioSources)
            {
                if (source != null && source.isPlaying == false)
                {
                    audioSource = source;
                    break;
                }
            }

            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}