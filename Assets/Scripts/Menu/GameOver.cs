using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniJamGame16.Menu
{
    public class GameOver : MonoBehaviour
    {
        public void LoadMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
