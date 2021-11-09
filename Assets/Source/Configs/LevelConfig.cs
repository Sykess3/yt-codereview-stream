using Source.Configs.DataStructures;
using Source.Models;
using Source.Models.Balls;
using Source.Models.Level;
using UnityEngine;

namespace Source.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/Level", order = 0)]
    public class LevelConfig : ScriptableObject, ILevelConfig, IBallsSpawnerLevelConfig, IFallingAccelerationLevelConfig, IConfigWithIdentifier<string>
    {
        [SerializeField] private string _sceneName;

        [Tooltip("In Points")]
        [SerializeField] private int _goal;

        [Tooltip("In seconds")]
        [SerializeField] private FloatRange _delayBetweenSpawn;
        [SerializeField] private float _timeToSpeedUp;
        [SerializeField] private float _speedIncreaseAmountAfterSpeedUp;
        [SerializeField] private IntRange _ballsByOneSpawn;
        
        public IntRange BallsByOneSpawn => _ballsByOneSpawn;
        public FloatRange DelayBetweenSpawn => _delayBetweenSpawn;
        public int Goal => _goal;
        public float TimeToSpeedUp => _timeToSpeedUp;
        public float SpeedIncreaseAmountAfterSpeedUp => _speedIncreaseAmountAfterSpeedUp;

        string IConfigWithIdentifier<string>.Identifier => _sceneName;
    }
}