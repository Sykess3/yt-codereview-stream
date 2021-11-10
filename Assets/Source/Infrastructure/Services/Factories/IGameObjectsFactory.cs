using Source.Common;
using Source.Models;
using Source.Models.Balls;
using Source.Models.Randomizators;
using Source.Views;
using UnityEngine;

namespace Source.Infrastructure.Services.Factories
{
    public interface IGameObjectsFactory
    {
        Camera CreateCamera();
        BallsInputHandler CreateBallsInputHandler(IPlayerInput input, Score score);
        IPlayerInput CreateInput(Camera camera);
    }
}