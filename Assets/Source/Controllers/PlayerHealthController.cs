using Source.Models;
using Source.Views;

namespace Source.Controllers
{
    public class PlayerHealthController : Controller<PlayerHealth, PlayerHealthView>
    {
        public PlayerHealthController(PlayerHealthView view, PlayerHealth model) : base(view, model)
        {
        }

        protected override void Subscribe()
        {
            Model.DamageTaken += View.UpdateHPBar;
        }

        protected override void UnSubscribe()
        {
            Model.DamageTaken -= View.UpdateHPBar;
        }
    }
}