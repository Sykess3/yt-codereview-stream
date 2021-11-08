using System;
using Source.Configs;
using Source.Controllers;
using Source.Infrastructure.Services.AssetManagement;
using Source.Models;
using Source.Models.Balls;
using Source.Models.Factories;
using Source.Views;
using Source.Views.Balls;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Infrastructure.Services.Factories
{
    public class BallsFactory : IBallsFactory
    {
        private readonly IConfigProvider _configProvider;
        private readonly IViewsFactory _viewsFactory;

        public BallsFactory(IConfigProvider configProvider, IViewsFactory viewsFactory)
        {
            _configProvider = configProvider;
            _viewsFactory = viewsFactory;
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

            var ballView = _viewsFactory.CreateBallView(BallType.Red);
            var ballModel = new RedBall(ballConfig);
            new BallController(ballView, ballModel).Initialize();

            return ballModel;
        }
    }
}