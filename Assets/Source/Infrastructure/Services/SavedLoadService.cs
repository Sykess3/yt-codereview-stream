using UnityEngine;

namespace Source.Infrastructure.Services
{
    public class SavedLoadService : ISavedLoadService
    {
        private const string ProgressKey = "Progress";

        public void SaveProgress(PlayerProgress progress) => 
            PlayerPrefs.SetString(ProgressKey, progress.ToJson());

        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
    }
}