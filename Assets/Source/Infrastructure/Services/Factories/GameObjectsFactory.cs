using Source.Configs;
using Source.Controllers;
using Source.Infrastructure.Services.AssetManagement;
using Source.Models;
using Source.Views;
using UnityEngine;

namespace Source.Infrastructure.Services.Factories
{
    public class GameObjectsFactory : IGameObjectsFactory
    {
        private readonly ServiceLocator _services;
        private readonly IAssetsProvider _assetsProvider;
        private readonly IConfigProvider _configProvider;

        public GameObjectsFactory(ServiceLocator services)
        {
            _services = services;
            _assetsProvider = services.Single<IAssetsProvider>();
            _configProvider = services.Single<IConfigProvider>();
        }

        public Camera CreateCamera()
        {
            return _assetsProvider.Instantiate<Camera>(PrefabPath.Camera);
        }

        public BallsInputHandler CreateBallsInputHandler(IPlayerInput input)
        {
            var inputHandler = _assetsProvider.Instantiate<BallsInputHandler>(PrefabPath.BallsInputHandler);
            return inputHandler.Construct(input);
        }

        public IPlayerInput CreateInput(Camera camera)
        {
            var input = _assetsProvider.Instantiate<PlayerInput>(PrefabPath.Input);
            return input.Construct(camera);
        }

        public void CreateBallsSpawner(string currentSceneName, IRandomBall randomBall)
        {
            LevelConfig config = _configProvider.Get<LevelConfig, string>(identifier: currentSceneName, ConfigPath.Levels);
            
            var ballsSpawnerView = _assetsProvider.Instantiate<BallsSpawnerView>(PrefabPath.BallsSpawnerView);
            var ballsSpawnerModel = new BallsSpawner(randomBall, config);
            new BallsSpawnerController(ballsSpawnerView, ballsSpawnerModel).Initialize();
        }
    }
}