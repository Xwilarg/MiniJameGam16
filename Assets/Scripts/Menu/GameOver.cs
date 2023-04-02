using MiniJamGame16.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniJamGame16.Menu
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private AudioClip _bgm;

        private void Start()
        {
            AudioSystem.Instance.PlayMusic(_bgm);
        }

        public void LoadMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
