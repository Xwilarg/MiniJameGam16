using MiniJamGame16.Systems;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MiniJamGame16
{
    public class Blink : MonoBehaviour
    {
        [SerializeField] public Image _image;
        [SerializeField] public Sprite _closed, _opened;

        private bool _isOn;

        private void Awake()
        {
            StartCoroutine(BlinkA());
            AudioSystem.Instance.PlayMusic(null);
        }

        private IEnumerator BlinkA()
        {
            while (!_isOn)
            {
                yield return new WaitForSeconds(Random.Range(1f, 4f));
                _image.sprite = _closed;
                yield return new WaitForSeconds(Random.Range(.1f, .2f));
                _image.sprite = _opened;
            }
        }

        public void LetsGo()
        {
            _isOn = true;
        }

        private void Update()
        {
            if (_isOn)
            {
                var g = _image.color.g - Time.deltaTime / 3f;
                if (g < 0f) SceneManager.LoadScene("MainMenu");
                _image.color = new Color(1f, g, g);
            }
        }
    }
}
