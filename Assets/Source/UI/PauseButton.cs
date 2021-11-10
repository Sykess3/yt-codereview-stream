using Source.Infrastructure.Services.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private IUIFactory _uiFactory;

        public void Construct(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }
        
        private void Start()
        {
            _button.onClick.AddListener(PauseGame);
        }

        private void PauseGame()
        {
            Time.timeScale = 0f;
            _uiFactory.CreatePauseMenu();
        }
    }
}
