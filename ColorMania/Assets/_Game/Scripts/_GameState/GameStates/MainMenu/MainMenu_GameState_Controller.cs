using GameStates.Interfaces;
using Interfaces;
using Services;
using UnityEngine;
using Zenject;

namespace GameStates
{
    public class MainMenu_GameState_Controller : IGameState
    {
        [Inject] private SceneLoader _sceneLoader;
        [Inject] private IGameStatesManager _gameStatesManager;
        [Inject] private IAdsShower _adsShower;

        private MainMenu_GameState_ViewsManager _viewsManager;

        public void Enter()
        {
            _sceneLoader?.LoadScene("MainMenu", () =>
            {
                _viewsManager = new MainMenu_GameState_ViewsManager();
                _viewsManager.Initialize();

                _viewsManager.onPlayClicked += EnterGameplayState;
                _viewsManager.onQuitClicked += QuitGame;

                //_adsShower?.ShowBanner();
            });
        }

        public void Exit()
        {

        }

        public void Update()
        {

        }

        private void EnterGameplayState()
        {
            _gameStatesManager.ChangeState(new Gameplay_GameState_Controller());
        }

        private void QuitGame()
        {
            Application.Quit();
        }
    }
}