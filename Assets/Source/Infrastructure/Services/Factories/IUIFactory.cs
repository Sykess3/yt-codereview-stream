using Source.UI;
using UnityEngine;

namespace Source.Infrastructure.Services.Factories
{
    public interface IUIFactory
    {
        GameObject CreateHUD();
        void CreateUIRoot();
        void CreateGameOverWindow();
        void CreatePauseMenu();
        PauseButton CreatePauseButton();
    }
}