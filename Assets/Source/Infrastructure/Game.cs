using System;
using Source.Infrastructure.Root;
using Source.Infrastructure.Services;
using Source.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Infrastructure
{
    public class Game : MonoBehaviour, ICoroutineRunner
    {
        private ServiceLocator _services;

        private void Awake()
        {
            Constants.Init();
            _services = new ServiceLocator();
            var levelLoader = CreateLevelLoader();

            var singleServiceRegistration = new SingleServicesRegistration(_services, levelLoader);
            singleServiceRegistration.Execute();
            
            
            DontDestroyOnLoad(gameObject);
            
            levelLoader.LoadLevel("SampleScene");
        }

        private LevelLoader CreateLevelLoader()
        {
            var sceneLoader = new SceneLoader(this);
            var levelLoader = new LevelLoader(_services, sceneLoader);
            return levelLoader;
        }
    }
}