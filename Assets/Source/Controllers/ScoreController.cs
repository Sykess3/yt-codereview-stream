using Source.Models;
using Source.Views;

namespace Source.Controllers
{
    public class ScoreController : Controller<Score, ScoreView>
    {
        public ScoreController(ScoreView view, Score model) : base(view, model)
        {
        }

        protected override void Subscribe()
        {
            Model.Updated += View.UpdateScore;
            Model.IncreaseBy(0);
        }

        protected override void UnSubscribe()
        {
            Model.Updated -= View.UpdateScore;
        }
    }
}