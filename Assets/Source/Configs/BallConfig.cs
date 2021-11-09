using System;
using Source.Infrastructure;
using Source.Infrastructure.Services;
using Source.Models;
using Source.Models.Balls;
using Source.Views;
using Source.Views.Balls;
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
        [SerializeField] private int _points;
        [SerializeField] private int _damage;
        [SerializeField] private Color _color;

        private Vector3 _velocity;
        
        public BallView Prefab => _ballView;
        public BallType Type => _type;
        BallType IConfigWithIdentifier<BallType>.Identifier => _type;
        public Vector3 Velocity => _velocity;
        public int Damage => _damage;
        public int Points => _points;
        public Color Color => _color;
        private void OnValidate() => _velocity = new Vector3(0, -_startSpeed, 0);
    }
}