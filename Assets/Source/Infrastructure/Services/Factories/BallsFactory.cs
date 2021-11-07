using System;
using Source.Configs;
using Source.Controllers;
using Source.Infrastructure.Services.AssetManagement;
using Source.Models;
using Source.Models.Balls;
using Source.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Infrastructure.Services.Factories
{
    public class BallsFactory : IBallsFactory
    {
        private readonly IConfigProvider _configProvider;

        public BallsFactory(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public Ball Create(BallType type)
        {
            switch (type)
            {
                case BallType.Red:
                    return CreateRedBall();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private Ball CreateRedBall()
        {
            var ballConfig = _configProvider.Get<BallConfig, BallType>(identifier: BallType.Red, ConfigPath.Balls);
            
            BallView ballView = Object.Instantiate(ballConfig.Prefab);
            var ballModel = new RedBall(ballConfig);
            new BallController(ballView, ballModel).Initialize();

            return ballModel;
        }
    }
}