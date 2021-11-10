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
        private Transform _viewsToTurnOffOnGameOver;

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
            }

            return CreatBall(type);
        }

        private BallView CreatBall(BallType ballType)
        {
            var config = _configProvider.Get<BallConfig, BallType>(identifier: ballType, ConfigPath.Balls);
            BallView ballView = Object.Instantiate(config.Prefab, _ballsParent);

            ballView.GetComponent<BallVFX>().Construct(config.PopVFXPrefab);
            return ballView;
        }

        private void SetViewsParentTo(GameObject gameObject)
        {
            if (_viewsToTurnOffOnGameOver == null)
                _viewsToTurnOffOnGameOver = new GameObject("ViewsToTurnOffOnGameOver").transform;

            gameObject.transform.parent = _viewsToTurnOffOnGameOver;
        }
    }
}