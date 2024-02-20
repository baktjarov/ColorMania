using Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class Gameplay_View : ViewBase
    {
        [Header("Buttons")]
        [SerializeField] private Button _pauseButton;

        private Drawable _drawable;
        private Pause_View _pauseView;
        private Win_View _winView;

        private float _percentage;

        private void Update()
        {
            OpenWin();
        }

        public void Construct(Pause_View pauseView, Win_View winView, Drawable drawable)
        {
            _pauseView = pauseView;
            _winView = winView;
            _drawable = drawable;
        }

        public override void Open()
        {
            base.Open();

            _pauseButton.onClick.AddListener(OpenPause);
        }

        public override void Close()
        {
            base.Close();

            _pauseButton.onClick.RemoveListener(OpenPause);
        }

        private void OpenPause()
        {
            _pauseView?.Open();
        }

        private void OpenWin()
        {

            _percentage = _drawable.GetPercentageOfColoring();

            if (_percentage >= 99)
            {
                _winView?.Open();
            }
        }
    }
}