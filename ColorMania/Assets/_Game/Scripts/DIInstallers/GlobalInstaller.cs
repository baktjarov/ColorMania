using GameStates;
using GameStates.Interfaces;
using Interfaces;
using Services;
using SO;
using Sound;
using UIElements;
using UnityEngine;
using Zenject;

namespace DIInstallers
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private SoundManager _soundManager;
        [SerializeField] private ListOfAllViews _listOfAllViews;
        [SerializeField] private ListOfAllPens _listOffAllPens;
        [SerializeField] private ListOfAllPictures _listOfAllPictures;
        [SerializeField] private PenShopUnit _penShopUnityPrefab;

        private PenSaveService _penSaveService;
        private GameSaveService _gameSaveService;

        private IAdsShower _adsShower;

        public override void InstallBindings()
        {
            _adsShower = new AdsShower_AdMob();
            (_adsShower as AdsShower_AdMob).Initialize();

            _penSaveService = new PenSaveService();
            _gameSaveService = new GameSaveService();

            BindServices();
            BindLists();
            BindPrefabs();

            _listOffAllPens.Initialize(_penSaveService);
        }

        private void BindServices()
        {
            Container.Bind<IGameStatesManager>().FromInstance(new GameStatesManager(Container));
            Container.Bind<SceneLoader>().FromInstance(_sceneLoader);
            Container.Bind<SoundManager>().FromInstance(_soundManager);
            Container.Bind<IAdsShower>().FromInstance(_adsShower);
            Container.Bind<IPenUnlocker>().FromInstance(new PenUnlocker_Ad(_adsShower, _penSaveService));
            Container.Bind<IPenSelecter>().FromInstance(_penSaveService);
            Container.Bind<ILevelSaveService>().FromInstance(_gameSaveService);

            InjectService.SetDIContainer(Container);
        }

        private void BindLists()
        {
            Container.Bind<ListOfAllPens>().FromInstance(_listOffAllPens);
            Container.Bind<ListOfAllViews>().FromInstance(_listOfAllViews);
            Container.Bind<ListOfAllPictures>().FromInstance(_listOfAllPictures);

        }

        private void BindPrefabs()
        {
            Container.Bind<PenShopUnit>().FromInstance(_penShopUnityPrefab);
        }
    }
}