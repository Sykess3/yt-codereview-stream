using System;
using Source.Models;
using Source.Views;

namespace Source.Controllers
{
    public class BallController : Controller
    {
        private Ball _model => Model as Ball;
        private BallView _view => View as BallView;

        public BallController(View view, IModel model) : base(view, model) { }

        protected override void Subscribe()
        {
            _model.PositionChanged += _view.ChangePosition;
            _view.Clicked += OnClick;
        }

        protected override void UnSubscribe()
        {
            _model.PositionChanged -= _view.ChangePosition;
            _view.Clicked -= OnClick;
        }

        private void OnClick(BallView ballView) => _model.Pop();
    }
}