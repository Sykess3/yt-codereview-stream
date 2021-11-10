namespace Source.Infrastructure.Services
{
    public interface ISavedLoadService
    {
        void SaveProgress(PlayerProgress progress);
        PlayerProgress LoadProgress();
    }
}