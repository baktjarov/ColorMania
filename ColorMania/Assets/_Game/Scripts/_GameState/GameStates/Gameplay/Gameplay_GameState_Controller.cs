using Gameplay;
using GameStates.Interfaces;
using Interfaces;
using Services;
using Zenject;

namespace GameStates
{
    public class Gameplay_GameState_Controller : IGameState
    {
        [Inject] private SceneLoader _sceneLoader;
        [Inject] private IGameStatesManager _gameStatesManager;
        [Inject] private ILevelSaveService _levelSaveService;

        private Drawable _drawable;
        private Picture _picture;

        private Gameplay_GameState_Model _model;
        private Gameplay_GameState_ViewsManager _viewsManager;

        public void Enter()
        {
            _sceneLoader?.LoadScene("Gameplay", () =>
            {
                _model = new Gameplay_GameState_Model();
                _model.Initialize();

                _viewsManager = new Gameplay_GameState_ViewsManager(_model);
                _viewsManager.Initialize();

                _viewsManager.onMainMenuClicked += EnterMainMenuState;
                _viewsManager.onNextClicked += ReloadGameplayState;

                _drawable = UnityEngine.Object.FindObjectOfType<Drawable>(true);

                int pictureIndex = _levelSaveService.GetCurrentLevel();

                if (pictureIndex >= _model.listOfAllPictures.picturesCount)
                {
                    _levelSaveService.SetLevel(0);
                    _levelSaveService.GetCurrentLevel();
                }

                _picture = UnityEngine.Object.Instantiate(_model.listOfAllPictures.GetPicture(pictureIndex));
                _drawable.Initialize(_picture?.pictureSpriteRenderer);

                _model.colorPicker.Initialize(_picture.colors);
            });
        }

        public void Exit()
        {

        }

        public void Update()
        {

        }

        private void EnterMainMenuState()
        {
            _gameStatesManager.ChangeState(new MainMenu_GameState_Controller());
        }

        private void ReloadGameplayState()
        {
            _gameStatesManager.ChangeState(new Gameplay_GameState_Controller());
            _levelSaveService.SetLevel(_levelSaveService.GetCurrentLevel() + 1);
        }
    }
}