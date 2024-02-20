using Gameplay;
using GameStates.Interfaces;
using Interfaces;
using Services;
using SO;
using Zenject;

namespace GameStates
{
    public class Gameplay_GameState_Controller : IGameState
    {
        [Inject] private SceneLoader _sceneLoader;
        [Inject] private IGameStatesManager _gameStatesManager;
        [Inject] private ListOfAllPictures _listOfAllPictures;
        [Inject] private ILevelSaveService _levelSaveService;

        private Drawable _drawable;
        private Picture _picture;

        private Gameplay_GameState_ViewsManager _viewsManager;

        public void Enter()
        {
            _sceneLoader?.LoadScene("Gameplay", () =>
            {
                _viewsManager = new Gameplay_GameState_ViewsManager();
                _viewsManager.Initialize();

                _viewsManager.onMainMenuClicked += EnterMainMenuState;
                _viewsManager.onNextClicked += ReloadGameplayState;

                _drawable = UnityEngine.Object.FindObjectOfType<Drawable>(true);

                _picture = UnityEngine.Object.Instantiate(_listOfAllPictures.GetPicture(_levelSaveService.GetCurrentLevel()));
                _drawable.Initialize(_picture?.pictureSpriteRenderer);
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