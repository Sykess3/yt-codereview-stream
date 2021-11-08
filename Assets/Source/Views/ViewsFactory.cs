using Source.Configs;
using Source.Infrastructure.Services;
using Source.Infrastructure.Services.AssetManagement;
using Source.Models.Balls;
using Source.Views.Balls;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Views
{
    public class ViewsFactory : IViewsFactory
    {
        private readonly IAssetsProvider _assetsProvider;
        private readonly IConfigProvider _configProvider;
        private Transform _ballsParent;
        private FixedUpdatableView _fixedUpdatableView;
        private UpdatableView _updatableView;

        public ViewsFactory(IAssetsProvider assetsProvider, IConfigProvider configProvider)
        {
            _assetsProvider = assetsProvider;
            _configProvider = configProvider;
        }

        public FixedUpdatableView CreateFixedUpdatable()
        {
            if (_fixedUpdatableView == null)
                _fixedUpdatableView = new GameObject("FixedUpdatableView").AddComponent<FixedUpdatableView>();

            return _fixedUpdatableView;
        }

        public UpdatableView CreateUpdatable()
        {
            if (_updatableView == null) 
                _updatableView = new GameObject("UpdatableView").AddComponent<UpdatableView>();

            return _updatableView;
        }

        public BallView CreateBallView(BallType type)
        {
            if (_ballsParent == null)
                _ballsParent = new GameObject("Balls").transform;

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
    }
}