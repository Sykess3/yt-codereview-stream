using Source.Configs;
using Source.Controllers;
using Source.Infrastructure.Services;
using Source.Infrastructure.Services.Factories;
using Source.Models;
using Source.Views;
using UnityEngine.SceneManagement;

namespace Source.Infrastructure.Root
{
    public class SceneCompositeRoot
    {
        private readonly ServiceLocator _services;
        private readonly IGameObjectsFactory _gameObjectsFactory;
        private readonly IConfigProvider _configProvider;

        public SceneCompositeRoot(ServiceLocator services)
        {
            _services = services;
            _gameObjectsFactory = _services.Single<IGameObjectsFactory>();
            _configProvider = _services.Single<IConfigProvider>();
        }

        public void Composite()
        {
            var camera = _gameObjectsFactory.CreateCamera();
            IPlayerInput input = _gameObjectsFactory.CreateInput(camera);
            _gameObjectsFactory.CreateBallsInputHandler(input);
            _gameObjectsFactory.CreateBallsSpawner(CurrentSceneName());
            
        }

        private static string CurrentSceneName() => SceneManager.GetActiveScene().name;
    }
}