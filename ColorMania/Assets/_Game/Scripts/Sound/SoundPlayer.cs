using UnityEngine;
using UnityEngine.UI;

namespace Sound
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private SoundManager _soundManager;
        [SerializeField] private AudioClip _clickSound;
        [SerializeField] private Button _button;
        [SerializeField] private SoundSettings _soundSettings = new SoundSettings();

        private void Awake()
        {
            if (_button == null) { _button = GetComponent<Button>(); }
            if (_soundManager == null) { _soundManager = FindObjectOfType<SoundManager>(); }
        }

        private void OnEnable()
        {
            _button?.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button?.onClick.RemoveListener(OnClick);
        }

        public void OnClick()
        {
            _soundManager?.TryPlay(_clickSound, _soundSettings);
        }
    }
}