using Source.Models;
using Source.Views;

namespace Source.Controllers
{
    public class BallsSpawnerController : Controller
    {
        public BallsSpawnerController(View view, IModel model) : base(view, model)
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