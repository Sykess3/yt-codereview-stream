using Source.Configs;
using Source.Controllers;
using Source.Infrastructure.Services;
using Source.Infrastructure.Services.Factories;
using Source.Models;
using Source.Models.Balls;
using Source.Models.DataStructures;
using Source.Models.Factories;
using Source.Models.Randomizators;
using Source.Views;
using UnityEngine.SceneManagement;

namespace Source.Infrastructure.Root
{
    public class SceneCompositeRoot
    {
        private readonly ServiceLocator _services;

        public SceneCompositeRoot(ServiceLocator services)
        {
            _services = services;
        }

        public void Composite()
        {
            InitializeRandomPositionGenerator();

            var gameObjectsFactory = _services.Single<IGameObjectsFactory>();
            
            var camera = gameObjectsFactory.CreateCamera();
            IPlayerInput input = gameObjectsFactory.CreateInput(camera);
            gameObjectsFactory.CreateBallsInputHandler(input);

            IViewsFactory viewsFactory = _services.Single<IViewsFactory>();
            var ballsSpawnerFactory = InitializeBallsSpawnerFactory();

            ballsSpawnerFactory.CreateSpawner(CurrentSceneName());
        }

        private IBallsSpawnerFactory InitializeBallsSpawnerFactory()
        {
            IBallsSpawnerFactory ballsSpawnerFactory = _services.Single<IBallsSpawnerFactory>();
            ballsSpawnerFactory.Initialize(
                new BallsObjectPool(_services.Single<IBallsFactory>()));
            return ballsSpawnerFactory;
        }

        private void InitializeRandomPositionGenerator()
        {
            var configProvider = _services.Single<IConfigProvider>();
            IBallsSpawnerLevelConfig levelConfig =
                configProvider.Get<LevelConfig, string>(identifier: CurrentSceneName(), ConfigPath.Levels);
            _services.Single<IRandomPositionGenerator>().Initialize(
                positionsDoNotRepeatAmount: levelConfig.BallsByOneSpawn.Max);
        }

        private static string CurrentSceneName() => SceneManager.GetActiveScene().name;
    }
}