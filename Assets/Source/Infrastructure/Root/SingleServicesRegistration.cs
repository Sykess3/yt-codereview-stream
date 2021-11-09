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
                _services.Single<IFallingAccelerationService>()));

            _services.RegisterSingle<IGameObjectsFactory>(new GameObjectsFactory(
                _services));
        }

        private RandomPositionGenerator GetRandomPositionGenerator()
        {
            var assetsProvider = _services.Single<IAssetsProvider>();
            var camera = assetsProvider.Load<Camera>(PrefabPath.Camera);

            Vector3 screenToWorldPoint = camera.ScreenToWorldPoint(
                new Vector3(Screen.width, Screen.height, 10));

            (float, float) xBorders = (-screenToWorldPoint.x * 0.5f, screenToWorldPoint.x * 0.5f);
            float height = screenToWorldPoint.y;
            float depth = screenToWorldPoint.z;
            return new RandomPositionGenerator(xBorders, depth, height);
        }
    }
}