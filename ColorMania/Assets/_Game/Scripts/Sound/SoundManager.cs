using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSourcePrefab;
        [SerializeField] private List<AudioSource> _audioSourcePool = new List<AudioSource>();

        public void TryPlay(AudioClip clip, SoundSettings soundSettings = null)
        {
            AudioSource audioSource = GetAudioSource();

            if (soundSettings != null)
            {
                audioSource.loop = soundSettings.isLoop;
                audioSource.volume = soundSettings.volume;
            }

            audioSource.clip = clip;
            audioSource.Play();
        }

        public void StopAll()
        {
            foreach (AudioSource audioSource in _audioSourcePool)
            {
                audioSource.Stop();
            }
        }

        public void StopAllSoundsOf(AudioClip clip)
        {
            foreach (AudioSource audioSource in _audioSourcePool)
            {
                if (audioSource.clip == clip) { audioSource.Stop(); }
            }
        }

        public void StopAllSoundsExcept(AudioClip clip)
        {
            foreach (AudioSource audioSource in _audioSourcePool)
            {
                if (audioSource.clip != clip) { audioSource.Stop(); }
            }
        }

        private AudioSource GetAudioSource()
        {
            AudioSource audioSource = null;

            foreach (AudioSource audioSourceInPool in _audioSourcePool)
            {
                if (audioSourceInPool.isPlaying == false)
                {
                    audioSource = audioSourceInPool;
                }
            }

            if (audioSource == null)
            {
                audioSource = Instantiate(_audioSourcePrefab, transform);
                _audioSourcePool.Add(audioSource);
            }

            return audioSource;
        }
    }
}