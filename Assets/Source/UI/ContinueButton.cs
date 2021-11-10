using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI
{
    public class ContinueButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        public void Start()
        {
            _button.onClick.AddListener(ContinueGame);
        }

        private void ContinueGame()
        {
            Time.timeScale = 1f;
            Destroy(transform.parent.gameObject);
        }
    }
}