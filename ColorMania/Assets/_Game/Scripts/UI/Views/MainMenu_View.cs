using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class MainMenu_View : ViewBase
    {
        public Action onPlayButtonClicked;
        public Action onQuitButtonClicked;

        [Header("Buttons")]
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;

        private Shop_View _shopView;

        public void Construct(Shop_View shopView)
        {
            _shopView = shopView;
        }

        public override void Open()
        {
            base.Open();

            _shopButton.onClick.AddListener(OpenShop);
            _playButton.onClick.AddListener(Play);
            _quitButton.onClick.AddListener(Quit);
        }

        public override void Close()
        {
            base.Close();

            _shopButton.onClick.RemoveListener(OpenShop);
            _playButton.onClick.RemoveListener(Play);
            _quitButton.onClick.RemoveListener(Quit);

        }

        private void OpenShop()
        {
            _shopView?.Open();
        }

        private void Play()
        {
            onPlayButtonClicked?.Invoke();
        }

        private void Quit()
        {
            onQuitButtonClicked?.Invoke();
        }
    }
}