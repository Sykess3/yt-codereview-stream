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
            var gameObjectsFactory = _services.Single<IGameObjectsFactory>();
            var camera = gameObjectsFactory.CreateCamera();
            IPlayerInput input = gameObjectsFactory.CreateInput(camera);
            
            gameObjectsFactory.CreateBallsInputHandler(input);

            IViewsFactory viewsFactory = _services.Single<IViewsFactory>();
            IBallsSpawnerFactory ballsSpawnerFactory = _services.Single<IBallsSpawnerFactory>();
            ballsSpawnerFactory.Initialize(
                viewsFactory.CreateFixedUpdatable(),
                viewsFactory.CreateUpdatable(), 
                new BallsObjectPool(_services.Single<IBallsFactory>()));

            ballsSpawnerFactory.CreateSpawner(CurrentSceneName());
        }

        private static string CurrentSceneName() => SceneManager.GetActiveScene().name;
    }
}