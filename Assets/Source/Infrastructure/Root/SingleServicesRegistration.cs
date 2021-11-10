using Source.Infrastructure.Services;
using Source.Infrastructure.Services.AssetManagement;
using Source.Infrastructure.Services.Factories;
using Source.Models.Balls;
using Source.Models.Factories;
using Source.Models.Randomizators;
using Source.Views;
using UnityEngine;

namespace Source.Infrastructure.Root
{
    public class SingleServicesRegistration
    {
        private readonly ServiceLocator _services;
        private readonly ILevelLoader _levelLoader;

        public SingleServicesRegistration(ServiceLocator services, ILevelLoader levelLoader)
        {
            _services = services;
            _levelLoader = levelLoader;
        }

        public void Execute()
        {
            _services.RegisterSingle(_levelLoader);
            _services.RegisterSingle<IAssetsProvider>(new AssetsProvider());

            _services.RegisterSingle<IConfigProvider>(new ConfigProvider());

            _services.RegisterSingle<IRandomPositionGenerator>(GetRandomPositionGenerator());

            _services.RegisterSingle<IViewsFactory>(new ViewsFactory(
                _services.Single<IAssetsProvider>(),
                _services.Single<IConfigProvider>()));

            _services.RegisterSingle<IFallingAccelerationService>(new FallingAccelerationService(
            _services.Single<IConfigProvider>(),
            _services.Single<IViewsFactory>()));

            _services.RegisterSingle<IBallsSpawnerFactory>(new BallsSpawnerFactory(
                _services.Single<IConfigProvider>(),
                _services.Single<IRandomPositionGenerator>(),
                _services.Single<IViewsFactory>()));

            _services.RegisterSingle<IBallsFactory>(new BallsFactory(
                _services.Single<IConfigProvider>(),
                _services.Single<IViewsFactory>(),
                _services.Single<IFallingAccelerationService>(),
                ScreenBottomBorder()));

            _services.RegisterSingle<IGameObjectsFactory>(new GameObjectsFactory(
                _services));
            _services.RegisterSingle<ISavedLoadService>(new SavedLoadService());
            _services.RegisterSingle<IPersistentProgress>(new PersistentProgress());
            _services.RegisterSingle<IUIFactory>(new UIFactory(
                _services.Single<IAssetsProvider>(),
                _services.Single<IPersistentProgress>(),
                _services.Single<ISavedLoadService>()));
        }

        private RandomPositionGenerator GetRandomPositionGenerator()
        {

            Vector3 screenWidthAndHeight = ScreenWidthAndHeight();

            (float, float) xBorders = (-screenWidthAndHeight.x, screenWidthAndHeight.x);
            float height = screenWidthAndHeight.y;
            float depth = screenWidthAndHeight.z;
            return new RandomPositionGenerator(xBorders, depth, height);
        }

        private Vector3 ScreenWidthAndHeight()
        {
            var camera = _services.Single<IAssetsProvider>().Load<Camera>(PrefabPath.Camera);
            return camera.ScreenToWorldPoint(
                new Vector3(Screen.width, Screen.height, 10));
        }

        private Vector3 ScreenBottomBorder() => new Vector3(0, -ScreenWidthAndHeight().y, 0);
    }
}