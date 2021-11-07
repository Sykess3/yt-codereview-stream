using UnityEngine;

namespace Source.Models
{
    public interface IBallConfig
    {
        Vector3 Velocity { get; }
        BallType Type { get; }
    }
}