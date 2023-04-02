using MiniJamGame16.Systems;
using MiniJamGame16.Translation;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniJamGame16.Menu
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private AudioClip _bgm;
        [SerializeField] private TMP_Text _score;

        private void Start()
        {
            AudioSystem.Instance.PlayMusic(_bgm);
            _score.text = $"{Translate.Instance.Tr("SCORE").ToUpperInvariant()} {GlobalOptions.Instance.Score}";
        }

        public void LoadMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
