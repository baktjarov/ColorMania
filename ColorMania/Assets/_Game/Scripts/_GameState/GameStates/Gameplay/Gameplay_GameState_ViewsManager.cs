using Gameplay;
using Services;
using System;
using UI.Views;
using Object = UnityEngine.Object;

namespace GameStates
{
    public class Gameplay_GameState_ViewsManager
    {
        public Action onMainMenuClicked;
        public Action onNextClicked;

        private Gameplay_GameState_Model _model;

        private Gameplay_View _gameplayView;
        private Pause_View _pauseView;
        private Win_View _winView;
        private Drawable _drawable;

        public Gameplay_GameState_ViewsManager(Gameplay_GameState_Model model)
        {
            _model = model;
        }

        public void Initialize()
        {
            InjectService.Inject(this);

            Gameplay_View gameplay_View_Prefab = _model.listOfAllViews.GetView<Gameplay_View>();
            _gameplayView = Object.Instantiate(gameplay_View_Prefab);

            Pause_View pause_View_Prefab = _model.listOfAllViews.GetView<Pause_View>();
            _pauseView = Object.Instantiate(pause_View_Prefab);

            Win_View win_View_Prefab = _model.listOfAllViews.GetView<Win_View>();
            _winView = Object.Instantiate(win_View_Prefab);

            _drawable = Object.FindObjectOfType<Drawable>();

            _gameplayView.Construct(_pauseView, _winView, _drawable, _model.colorPicker);
            _pauseView.SetOpenOnCloseView(_gameplayView);

            _gameplayView.Open();

            _pauseView.onMainMenuButtonClicked += OnMainMenuClicked;
            _winView.onMainMenuButtonClicked += OnMainMenuClicked;
            _winView.onNextButtonClicked += OnNextClicked;
        }

        private void OnMainMenuClicked()
        {
            onMainMenuClicked?.Invoke();
        }

        private void OnNextClicked()
        {
            onNextClicked?.Invoke();
        }
    }
}