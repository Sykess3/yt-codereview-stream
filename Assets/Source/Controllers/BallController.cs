using System;
using Source.Models;
using Source.Models.Balls;
using Source.Views;
using Source.Views.Balls;
using UnityEngine;

namespace Source.Controllers
{
    public class BallController : Controller
    {
        private Ball _model => Model as Ball;
        private BallView _view => View as BallView;

        public BallController(View view, IModel model) : base(view, model) { }

        protected override void Subscribe()
        {
            _view.Clicked += OnClick;
            _view.FeltOutOfBounds += OnFeltOutOfBounds;
            _model.PositionChanged += _view.ChangePosition;
            _model.Initialized += OnModelInitialize;
        }

        protected override void UnSubscribe()
        {
            _model.PositionChanged -= _view.ChangePosition;
            _view.Clicked -= OnClick;
            _view.FeltOutOfBounds -= OnFeltOutOfBounds;
            _model.Initialized -= OnModelInitialize;
        }

        private void OnModelInitialize()
        {
            _view.gameObject.SetActive(true);
            Physics.SyncTransforms();
        }

        private void OnFeltOutOfBounds(BallView ballView)
        {
            _model.FallOutOfBounds();
            ballView.gameObject.SetActive(false);
        }

        private void OnClick(BallView ballView)
        {
            _model.Pop();
            ballView.gameObject.SetActive(false);
        }
    }
}