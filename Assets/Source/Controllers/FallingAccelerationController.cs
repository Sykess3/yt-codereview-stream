using Source.Models;
using Source.Models.Balls;
using Source.Views;

namespace Source.Controllers
{
    public class FallingAccelerationController : Controller
    {
        private FallingAccelerationView _view => View as FallingAccelerationView;
        private FallingAcceleration _model => Model as FallingAcceleration;
        public FallingAccelerationController(View view, IModel model) : base(view, model)
        {
        }

        protected override void Subscribe() => _model.SpeedIncreased += _view.InformAboutSpeedUp;

        protected override void UnSubscribe() => _model.SpeedIncreased -= _view.InformAboutSpeedUp;
    }
}