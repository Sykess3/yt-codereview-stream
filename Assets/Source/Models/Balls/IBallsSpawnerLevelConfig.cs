using Source.Configs.DataStructures;

namespace Source.Models.Balls
{
    public interface IBallsSpawnerLevelConfig
    {
        IntRange BallsByOneSpawn { get; }
        FloatRange DelayBetweenSpawn { get; }
    }
}