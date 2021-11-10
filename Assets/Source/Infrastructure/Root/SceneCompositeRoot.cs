using System;
using Source.Configs;
using Source.Controllers;
using Source.Infrastructure.Services;
using Source.Infrastructure.Services.Factories;
using Source.Models;
using Source.Models.Balls;
using Source.Models.DataStructures;
using Source.Models.Factories;
using Source.Models.Randomizators;
using Source.Views;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Infrastructure.Root
{
    public class SceneCompositeRoot
    {
        private readonly ServiceLocator _services;

        public SceneCompositeRoot(ServiceLocator services)
        {
            _services = services;
        }

        public void Composite()
        {
            _services.Single<IPersistentProgress>().Player = LoadProgressOrInitNew();
            var uiFactory = _services.Single<IUIFactory>();
            uiFactory.CreateUIRoot();
            GameObject hud = uiFactory.CreateHUD();
            uiFactory.CreatePauseButton().Construct(
                _services.Single<IUIFactory>());

            var playerHealth = LinkPlayerHealth(hud.GetComponentInChildren<PlayerHealthView>());
            playerHealth.OnGameOver += OnGameOver;
            InitializeRandomPositionGenerator();
            var scoreView = hud.GetComponentInChildren<ScoreView>();
            var score = LinkScore(scoreView);
            score.GoalReached += OnGameOver;

            _services.Single<IBallsFactory>().Initialize(playerHealth);

            var camera = _services.Single<IGameObjectsFactory>().CreateCamera();
            
            IPlayerInput input = _services.Single<IGameObjectsFactory>().CreateInput(camera);
            
            _services.Single<IGameObjectsFactory>().CreateBallsInputHandler(
                input,
                score);

            var ballsSpawnerFactory = InitializeBallsSpawnerFactory();

            ballsSpawnerFactory.CreateSpawner(CurrentSceneName());
        }

        private PlayerProgress LoadProgressOrInitNew() =>
            _services.Single<ISavedLoadService>().LoadProgress() 
            ?? new PlayerProgress(0);

        private void OnGameOver()
        {
            
            Time.timeScale = 0;
            _services.Single<IUIFactory>().CreateGameOverWindow();
        }

        private Score LinkScore(ScoreView scoreView)
        {
            var config = _services.Single<IConfigProvider>()
                .Get<LevelConfig, string>(identifier: CurrentSceneName(), ConfigPath.Balls);
            var score = new Score(
                config,
                progress: _services.Single<IPersistentProgress>().Player);
            scoreView.Construct(score);
            new ScoreController(scoreView, score).Initialize();
            return score;
        }

        private IBallsSpawnerFactory InitializeBallsSpawnerFactory()
        {
            IBallsSpawnerFactory ballsSpawnerFactory = _services.Single<IBallsSpawnerFactory>();
            ballsSpawnerFactory.Initialize(
                new BallsObjectPool(_services.Single<IBallsFactory>()));
            return ballsSpawnerFactory;
        }

        private PlayerHealth LinkPlayerHealth(PlayerHealthView view)
        {
            ILevelHealthConfig levelConfig =  _services.Single<IConfigProvider>().Get<LevelConfig, string>(identifier: CurrentSceneName(), ConfigPath.Levels);
            var model = new PlayerHealth(levelConfig.StartHP); 
            view.Construct(model);
            new PlayerHealthController(view, model).Initialize();
            return model;
        }

        private void InitializeRandomPositionGenerator()
        {
            var configProvider = _services.Single<IConfigProvider>();
            IBallsSpawnerLevelConfig levelConfig =
                configProvider.Get<LevelConfig, string>(identifier: CurrentSceneName(), ConfigPath.Levels);
            _services.Single<IRandomPositionGenerator>().Initialize(
                positionsDoNotRepeatAmount: levelConfig.BallsByOneSpawn.Max);
        }

        private static string CurrentSceneName() => SceneManager.GetActiveScene().name;
    }
}