using Source.Infrastructure.Services.AssetManagement;
using Source.UI;
using Source.Views;
using UnityEngine;

namespace Source.Infrastructure.Services.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetsProvider _assetsProvider;
        private readonly IPersistentProgress _persistentProgress;
        private readonly ISavedLoadService _savedLoadService;
        private Transform _uiRoot;

        public UIFactory(IAssetsProvider assetsProvider, IPersistentProgress persistentProgress,
            ISavedLoadService savedLoadService)
        {
            _assetsProvider = assetsProvider;
            _persistentProgress = persistentProgress;
            _savedLoadService = savedLoadService;
        }

        public void CreateUIRoot() => 
            _uiRoot = _assetsProvider.Instantiate(PrefabPath.UIRoot).transform;

        public GameObject CreateHUD() => 
            _assetsProvider.Instantiate(PrefabPath.HUD, _uiRoot);

        public void CreateGameOverWindow()
        {
            GameObject gameOverWindowObject = _assetsProvider.Instantiate(PrefabPath.GameOverWindow, _uiRoot);
            gameOverWindowObject.GetComponent<GameOverWindow>().Construct(_persistentProgress.Player, _savedLoadService);
        }

        public PauseButton CreatePauseButton() => 
            _assetsProvider.Instantiate(PrefabPath.PauseButton, _uiRoot).GetComponent<PauseButton>();

        public void CreatePauseMenu() => 
            _assetsProvider.Instantiate(PrefabPath.PauseMenu, _uiRoot);
    }
}