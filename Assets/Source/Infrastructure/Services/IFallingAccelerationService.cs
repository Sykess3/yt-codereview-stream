using Source.Models;
using Source.Models.Balls;

namespace Source.Infrastructure.Services
{
    public interface IFallingAccelerationService
    {
        FallingAcceleration GetCurrentSceneAccelerator();
    }
}