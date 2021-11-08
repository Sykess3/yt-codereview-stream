using Source.Models.Balls;
using UnityEngine;

namespace Source.Models.Configs
{
    public interface IBallConfig
    {
        Vector3 Velocity { get; }
        BallType Type { get; }
    }
}