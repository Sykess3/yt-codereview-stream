using System;
using Source.Configs;
using Source.Controllers;
using Source.Models;
using Source.Models.Balls;
using Source.Models.Factories;
using UnityEngine;

namespace Source.Infrastructure.Services.Factories
{
    public class BallsFactory : IBallsFactory
    {
        private readonly IConfigProvider _configProvider;
        private readonly IViewsFactory _viewsFactory;
        private readonly IFallingAccelerationService _fallingAccelerationService;
        private readonly Vector3 _bottomCameraBorder;
        private PlayerHealth _playerHealth;

        public BallsFactory(
            IConfigProvider configProvider,
            IViewsFactory viewsFactory,
            IFallingAccelerationService fallingAccelerationService,
            Vector3 bottomCameraBorder)
        {
            _configProvider = configProvider;
            _viewsFactory = viewsFactory;
            _fallingAccelerationService = fallingAccelerationService;
            _bottomCameraBorder = bottomCameraBorder;
        }

        public void Initialize(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
        }
        
        
        public Ball Create(BallType type)
        {
            var ballConfig = _configProvider.Get<BallConfig, BallType>(identifier: type, ConfigPath.Balls);

            var ballModel = new Ball(
                ballConfig, 
                _fallingAccelerationService.GetCurrentSceneAccelerator(),
                _bottomCameraBorder);
            var ballView = _viewsFactory.CreateBallView(type);
            ballView.Construct(ballModel);

            new BallController(ballView, ballModel, _playerHealth).Initialize();
            return ballModel;
        }
        
    }
}