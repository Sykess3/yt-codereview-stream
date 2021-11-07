using Source.Models;
using Source.Views;
using UnityEngine;

namespace Source.Infrastructure.Services.Factories
{
    public interface IGameObjectsFactory
    {
        Camera CreateCamera();
        BallsInputHandler CreateBallsInputHandler(IPlayerInput input);
        IPlayerInput CreateInput(Camera camera);
        void CreateBallsSpawner(string currentSceneName, IRandomBall randomBall);
    }
}