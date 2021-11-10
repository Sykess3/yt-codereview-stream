using UnityEngine;

namespace Source.Models.Balls
{
    public interface IBallConfig
    {
        Vector3 Velocity { get; }
        BallType Type { get; }
        int Damage { get; }
        int Cost { get; }
    }
}