using Source.Models;
using Source.Models.Balls;
using Source.Views;

namespace Source.Controllers
{
    public class FallingAccelerationController : Controller<FallingAcceleration, FallingAccelerationView>
    {
        public FallingAccelerationController(FallingAccelerationView view, FallingAcceleration model) : base(view, model)
        {
        }

        protected override void Subscribe() => Model.SpeedIncreased += View.InformAboutSpeedUp;

        protected override void UnSubscribe() => Model.SpeedIncreased -= View.InformAboutSpeedUp;
    }
}