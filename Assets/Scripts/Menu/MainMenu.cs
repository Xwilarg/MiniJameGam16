using MiniJamGame16.Translation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniJamGame16.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene("Main");
        }

        public void SetFrench()
        {
            Translate.Instance.CurrentLanguage = "french";
        }

        public void SetEnglish()
        {
            Translate.Instance.CurrentLanguage = "french";
        }
    }
}
