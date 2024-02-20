using UnityEngine;

namespace Sound
{
    public class BackgroundMusic : MonoBehaviour
    {
        [SerializeField] private SoundManager _soundManager;
        [SerializeField] private AudioClip _music;
        [SerializeField] private SoundSettings _musicSettings = new SoundSettings();

        private void OnEnable()
        {
            _soundManager.TryPlay(_music, _musicSettings);
        }

        private void OnDisable()
        {
            _soundManager.StopAllSoundsOf(_music);
        }
    }
}