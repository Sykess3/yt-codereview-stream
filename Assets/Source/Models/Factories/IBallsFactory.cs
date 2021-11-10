using Source.Models.Balls;

namespace Source.Models.Factories
{
    public interface IBallsFactory
    {
        Ball Create(BallType type);
        void Initialize(PlayerHealth playerHealth);
    }
}