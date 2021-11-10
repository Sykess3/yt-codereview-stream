using Source.Configs;
using Source.Controllers;
using Source.Infrastructure.Services.Factories;
using Source.Models;
using Source.Models.Balls;
using Source.Views;
using UnityEngine.SceneManagement;

namespace Source.Infrastructure.Services
{
    public class FallingAccelerationService : IFallingAccelerationService
    {
        private readonly IConfigProvider _configProvider;
        private readonly IViewsFactory _viewsFactory;
        private FallingAccelerationView _currentSceneView;
        private FallingAcceleration _currentSceneModel;

        public FallingAccelerationService(IConfigProvider configProvider, IViewsFactory viewsFactory)
        {
            _configProvider = configProvider;
            _viewsFactory = viewsFactory;
        }

        public FallingAcceleration GetCurrentSceneAccelerator()
        {
            if (WasNotCreatedInCurrentScene())
                _currentSceneModel = CreateSpeedCalculator();

            return _currentSceneModel;
        }

        private FallingAcceleration CreateSpeedCalculator()
        {
            IFallingAccelerationLevelConfig levelConfig =
                _configProvider.Get<LevelConfig, string>(identifier: CurrentSceneName(), ConfigPath.Levels);

            var model = new FallingAcceleration(levelConfig);
            _currentSceneView = _viewsFactory.CreateSpeedCalculatorView();
            _currentSceneView.Construct(model);
            new FallingAccelerationController(_currentSceneView, model).Initialize();
            return model;
        }

        private bool WasNotCreatedInCurrentScene() => _currentSceneView == null;

        private static string CurrentSceneName() =>
            SceneManager.GetActiveScene().name;
    }
}