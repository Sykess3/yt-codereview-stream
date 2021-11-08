using Source.Models.Balls;
using Source.Views.Balls;

namespace Source.Views
{
    public interface IViewsFactory
    {
        FixedUpdatableView CreateFixedUpdatable();
        UpdatableView CreateUpdatable();
        BallView CreateBallView(BallType type);
    }
}