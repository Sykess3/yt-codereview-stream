using Source.Models.Balls;
using Source.Views;
using Source.Views.Balls;

namespace Source.Infrastructure.Services.Factories
{
    public interface IViewsFactory
    {
        BallView CreateBallView(BallType type);
        FallingAccelerationView CreateSpeedCalculatorView();
        BallsSpawnerView CreateBallsSpawnerView();
    }
}