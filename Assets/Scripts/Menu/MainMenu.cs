using MiniJamGame16.Systems;
using MiniJamGame16.Translation;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MiniJamGame16.Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private AudioClip _menuAudio;

        private void Start()
        {
            AudioSystem.Instance.PlayMusic(_menuAudio);
            _slider.value = GlobalOptions.Instance.Volume;
            _difficulty.text = $"{Translate.Instance.Tr("HARDMODE")} {Translate.Instance.Tr(GlobalOptions.Instance.DifficultyHard ? "ON" : "OFF")}";
        }

        public void Play()
        {
            SceneManager.LoadScene("Main");
        }

        public void EasterEgg()
        {
            SceneManager.LoadScene("Eye");
        }

        public void SetFrench()
        {
            Translate.Instance.CurrentLanguage = "french";
        }

        public void SetEnglish()
        {
            Translate.Instance.CurrentLanguage = "english";
        }

        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private TMP_Text _difficulty;

        public void OnVolumeChange(float value)
        {
            GlobalOptions.Instance.Volume = value;
            AudioSystem.Instance.SetVolume(value);
        }

        public void ToggleDifficulty()
        {
            GlobalOptions.Instance.DifficultyHard = !GlobalOptions.Instance.DifficultyHard;
            _difficulty.text = $"{Translate.Instance.Tr("HARDMODE")} {Translate.Instance.Tr(GlobalOptions.Instance.DifficultyHard ? "ON" : "OFF")}";
        }
    }
}
