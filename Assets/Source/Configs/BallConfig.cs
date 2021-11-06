using Source.Infrastructure;
using Source.Infrastructure.Services;
using Source.Models;
using Source.Views;
using UnityEngine;

namespace Source.Configs
{
    [CreateAssetMenu(fileName = "BallConfig", menuName = "Configs/Ball", order = 0)]
    public class BallConfig : ScriptableObject, IBallConfig, IConfigWithIdentifier<BallType>
    {
        [SerializeField] private BallView _ballView;
        [SerializeField] private BallType _type;
        [Tooltip("Units is second")]
        [SerializeField] private float _startSpeed;
        public BallView Prefab => _ballView;

        public float StartSpeed => _startSpeed;
        public BallType Type => _type;
        BallType IConfigWithIdentifier<BallType>.Identifier => _type;
    }
}