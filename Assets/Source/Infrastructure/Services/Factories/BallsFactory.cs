using System;
using Source.Configs;
using Source.Controllers;
using Source.DataStructures;
using Source.Infrastructure.Services.AssetManagement;
using Source.Models;
using Source.Views;
using Object = UnityEngine.Object;

namespace Source.Infrastructure.Services.Factories
{
    public interface IBallsFactory
    {
        BallView Create(BallType type);
    }

    public class BallsFactory : IBallsFactory
    {
        private readonly IConfigProvider _configProvider;

        public BallsFactory(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public BallView Create(BallType type)
        {
            switch (type)
            {
                case BallType.Red:
                    return CreateRedBall();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private BallView CreateRedBall()
        {
            var ballConfig = _configProvider.Get<BallConfig, BallType>(identifier: BallType.Red, ConfigPath.Balls);
            
            BallView ballView = Object.Instantiate(ballConfig.Prefab);
            ballView.Construct(ballConfig.Type);
            var ballModel = new RedBall(ballConfig);
            new BallController(ballView, ballModel).Initialize();

            return ballView;
        }
    }
}