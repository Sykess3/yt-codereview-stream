using Source.Models.Balls;
using Source.Models.DataStructures;
using Source.Views;

namespace Source.Infrastructure.Services.Factories
{
    public interface IBallsSpawnerFactory
    {
        void Initialize(BallsObjectPool ballsObjectPool);

        BallsSpawner CreateSpawner(string currentSceneName);
    }
}