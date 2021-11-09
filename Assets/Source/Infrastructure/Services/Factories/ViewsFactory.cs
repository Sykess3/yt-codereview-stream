using Source.Configs;
using Source.Infrastructure.Services.AssetManagement;
using Source.Models.Balls;
using Source.Views;
using Source.Views.Balls;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Infrastructure.Services.Factories
{
    public class ViewsFactory : IViewsFactory
    {
        private readonly IAssetsProvider _assetsProvider;
        private readonly IConfigProvider _configProvider;
        private Transform _ballsParent;
        private Transform _otherViewsParent;

        public ViewsFactory(IAssetsProvider assetsProvider, IConfigProvider configProvider)
        {
            _assetsProvider = assetsProvider;
            _configProvider = configProvider;
        }

        public FallingAccelerationView CreateSpeedCalculatorView()
        {
            var speedCalculatorObject = new GameObject("SpeedCalculator");
            SetViewsParentTo(speedCalculatorObject);
            return speedCalculatorObject.AddComponent<FallingAccelerationView>();
        }

        public BallsSpawnerView CreateBallsSpawnerView() => 
            new GameObject("BallsSpawnerView").AddComponent<BallsSpawnerView>();

        public BallView CreateBallView(BallType type)
        {
            if (_ballsParent == null)
            {
                _ballsParent = new GameObject("Balls").transform;
                SetViewsParentTo(_ballsParent.gameObject);
            }

            switch (type)
            {
                case BallType.Red:
                    return CreateRedBall();
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private BallView CreateRedBall()
        {
            var config = _configProvider.Get<BallConfig, BallType>(identifier: BallType.Red, ConfigPath.Balls);
            return Object.Instantiate(config.Prefab, _ballsParent);
        }

        private void SetViewsParentTo(GameObject gameObject)
        {
            if (_otherViewsParent == null)
                _otherViewsParent = new GameObject("Views").transform;

            gameObject.transform.parent = _otherViewsParent;
        }
    }
}