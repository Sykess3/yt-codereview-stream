using Source.Models;
using Source.Models.Balls;
using Source.Views;

namespace Source.Controllers
{
    public class BallsSpawnerController : Controller<BallsSpawner, BallsSpawnerView>
    {
        public BallsSpawnerController(BallsSpawnerView view, BallsSpawner model) : base(view, model)
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