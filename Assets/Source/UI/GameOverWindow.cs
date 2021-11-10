using System;
using Source.Infrastructure.Services;
using TMPro;
using UnityEngine;

namespace Source.UI
{
    public class GameOverWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _maxScoreText;
        [SerializeField] private TextMeshProUGUI _currentScoreText;
        private PlayerProgress _progress;
        private ISavedLoadService _savedLoadService;

        public void Construct(PlayerProgress progress, ISavedLoadService savedLoadService)
        {
            _progress = progress;
            _savedLoadService = savedLoadService;
        }

        private void Start()
        {
            if (_progress.MaxScore < _progress.CurrentScore)
            {
                _progress.MaxScore = _progress.CurrentScore;
                _savedLoadService.SaveProgress(_progress);
            }
            
            _maxScoreText.text += _progress.MaxScore.ToString();
            _currentScoreText.text += _progress.CurrentScore.ToString();
        }
    }
}