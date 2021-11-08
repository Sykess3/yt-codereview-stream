using Source.Models;
using Source.Views;

namespace Source.Controllers
{
    public class BallsCollisionController : Controller
    {
        public BallsCollisionController(View view, IModel model) : base(view, model)
        {
        }

        protected override void UnSubscribe()
        {
        }

        protected override void Subscribe()
        {
        }
    }
}