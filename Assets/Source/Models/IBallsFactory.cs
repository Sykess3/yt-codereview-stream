using Source.Models.Balls;

namespace Source.Models
{
    public interface IBallsFactory
    {
        Ball Create(BallType type);
    }
}