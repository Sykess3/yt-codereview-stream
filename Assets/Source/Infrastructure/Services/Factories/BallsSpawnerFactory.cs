using Source.Common;
using Source.Configs;
using Source.Controllers;
using Source.Infrastructure.Services.AssetManagement;
using Source.Models;
using Source.Models.Balls;
using Source.Models.DataStructures;
using Source.Models.Randomizators;
using Source.Views;
using Source.Views.Balls;

namespace Source.Infrastructure.Services.Factories
{
    public class BallsSpawnerFactory : IBallsSpawnerFactory
    {
        private readonly IConfigProvider _configProvider;
        private readonly IRandomPositionGenerator _randomPositionGenerator;
        private FixedUpdatableView _fixedUpdatableView;
        private UpdatableView _updatableView;
        private IRandomBallGenerator _randomBallGenerator;

        public BallsSpawnerFactory(
            IConfigProvider configProvider,
            IRandomPositionGenerator randomPositionGenerator)
        {
            _configProvider = configProvider;
            _randomPositionGenerator = randomPositionGenerator;
        }

        public void Initialize(
            FixedUpdatableView fixedUpdatableView,
            UpdatableView updatableView,
            BallsObjectPool ballsObjectPool)
        {
            _fixedUpdatableView = fixedUpdatableView;
            _updatableView = updatableView;
            _randomBallGenerator = new RandomBallGenerator(ballsObjectPool);

        }

        public BallsSpawner CreateSpawner(string currentSceneName)
        {
            LevelConfig config = _configProvider.Get<LevelConfig, string>(identifier: currentSceneName, ConfigPath.Levels);
            
            var ballsSpawnerModel = new BallsSpawner(_randomBallGenerator, config, _randomPositionGenerator);
            new BallsSpawnerController(_updatableView, ballsSpawnerModel).Initialize();
            
            return ballsSpawnerModel;
        }
    }
}