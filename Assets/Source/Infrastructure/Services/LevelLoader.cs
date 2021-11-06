using System;
using Source.Infrastructure.Root;
using Source.Views;

namespace Source.Infrastructure.Services
{
    public class LevelLoader : ILevelLoader
    {
        private readonly ServiceLocator _services;
        private readonly ISceneLoader _sceneLoader;

        public LevelLoader(ServiceLocator services, ISceneLoader sceneLoader)
        {
            _services = services;
            _sceneLoader = sceneLoader;
        }

        public void LoadLevel(string name)
        {
            _sceneLoader.Load(name, EnterSceneCompositeRoot);
        }

        private void EnterSceneCompositeRoot()
        {
            new SceneCompositeRoot(_services).Composite();
        }
    }
}