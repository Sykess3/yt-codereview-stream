using Source.Configs;
using Source.Controllers;
using Source.Infrastructure.Services.AssetManagement;
using Source.Models;
using Source.Models.Balls;
using Source.Models.Randomizators;
using Source.Views;
using Source.Views.Balls;
using UnityEngine;

namespace Source.Infrastructure.Services.Factories
{
    public class GameObjectsFactory : IGameObjectsFactory
    {
        private readonly IAssetsProvider _assetsProvider;

        public GameObjectsFactory(ServiceLocator services)
        {
            _assetsProvider = services.Single<IAssetsProvider>();
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

    }
}