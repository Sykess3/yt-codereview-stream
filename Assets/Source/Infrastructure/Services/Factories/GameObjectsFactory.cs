using Source.Common;
using Source.Infrastructure.Services.AssetManagement;
using Source.Models;
using Source.Views;
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

        public BallsInputHandler CreateBallsInputHandler(IPlayerInput input, Score score)
        {
            var inputHandler = _assetsProvider.Instantiate<BallsInputHandler>(PrefabPath.BallsInputHandler);
            return inputHandler.Construct(input, score);
        }

        public IPlayerInput CreateInput(Camera camera)
        {
            var input = _assetsProvider.Instantiate<PlayerInput>(PrefabPath.Input);
            return input.Construct(camera);
        }

    }
}