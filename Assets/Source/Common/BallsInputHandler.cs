using Source.Infrastructure.Services;
using Source.Models;
using Source.Views.Balls;
using UnityEngine;

namespace Source.Common
{
    public class BallsInputHandler : MonoBehaviour
    {
        private IPlayerInput _input;
        private Score _score;

        public BallsInputHandler Construct(IPlayerInput input, Score score)
        {
            _input = input;
            _score = score;
            return this;
        }

        private void Start() => _input.ClickedOnGameObject += OnClick;

        private void OnDestroy() => _input.ClickedOnGameObject -= OnClick;

        private void OnClick(GameObject obj)
        {
            if (obj.TryGetComponent(out BallView ball))
            {
                ball.Click();
                _score.IncreaseBy(ball.Model.Cost);
            }
        }
    }
}