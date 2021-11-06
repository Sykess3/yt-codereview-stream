using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        private const string Initial = "Initial";

        [RuntimeInitializeOnLoadMethod]
        private static void LoadBootScene()
        {
            if (SceneManager.GetActiveScene().name != Initial) 
                SceneManager.LoadScene(Initial);
        }
    }
}