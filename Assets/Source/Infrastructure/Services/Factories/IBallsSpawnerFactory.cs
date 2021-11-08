using Source.Models.Balls;
using Source.Models.DataStructures;
using Source.Views;

namespace Source.Infrastructure.Services.Factories
{
    public interface IBallsSpawnerFactory
    {
        void Initialize(
            FixedUpdatableView fixedUpdatableView,
            UpdatableView updatableView,
            BallsObjectPool ballsObjectPool);

        BallsSpawner CreateSpawner(string currentSceneName);
    }
}