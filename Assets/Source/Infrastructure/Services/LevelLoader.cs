using System;
using Source.Infrastructure.Root;
using Source.Views;
using UnityEngine.SceneManagement;

namespace Source.Infrastructure.Services
{
    public class LevelLoader : ILevelLoader
    {
        private static  ServiceLocator _services;
        private static  ISceneLoader _sceneLoader;

        public LevelLoader(ServiceLocator services, ISceneLoader sceneLoader)
        {
            _services = services;
            _sceneLoader = sceneLoader;
        }

        public void LoadLevel(string name)
        {
            _sceneLoader.Load(name, EnterSceneCompositeRoot);
        }

        public static void Reload()
        {
            _sceneLoader.Reload(EnterSceneCompositeRoot);
        }

        private static void EnterSceneCompositeRoot()
        {
            new SceneCompositeRoot(_services).Composite();
        }
    }
}