using System;
using Source.Infrastructure.Services;
using UnityEngine;

namespace Source.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/Level", order = 0)]
    public class LevelConfig : ScriptableObject, IConfigWithIdentifier<string>
    {
        [SerializeField] private float _maxTimeToSpawnInSeconds;
        [SerializeField] private float _minTimeToSpawnInSeconds;
        [SerializeField] private string _sceneName;

        public float MinTimeToSpawnInSeconds => _minTimeToSpawnInSeconds;
        public float MaxTimeToSpawnInSeconds => _maxTimeToSpawnInSeconds;
        string IConfigWithIdentifier<string>.Identifier => _sceneName;
    }
}