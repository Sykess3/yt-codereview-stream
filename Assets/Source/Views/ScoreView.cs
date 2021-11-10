using Source.Models;
using Source.Models.Level;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Views
{
    public class ScoreView : View<Score>
    {
        [SerializeField] private Text _text;

        public void UpdateScore(int newScore) => 
            _text.text = newScore.ToString();
    }
}