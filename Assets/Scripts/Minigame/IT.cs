using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MiniJamGame16.Minigame
{
    public class IT : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _referenceText;

        [SerializeField]
        private TMP_InputField _input;

        [SerializeField]
        private TextAsset _textAsset;

        private void Awake()
        {
            _referenceText.text = _textAsset.text;
        }

        public void OnTextEdit()
        {
            var length = _input.text.Length;
            if (_textAsset.text[length - 1] != _input.text[length - 1])
            {
                Debug.Log("Fixing");
                _input.text = _input.text[..^1];
            }
            else
            {
                while (length < _textAsset.text.Length && new[] { ' ', '\t', '\n', '\r' }.Contains(_textAsset.text[length]))
                {
                    length++;
                    _input.text = _textAsset.text[..length];
                    Debug.Log("|" + _textAsset.text[..length] + "|");
                }
                IEnumerator UpdateCaret()
                {
                    yield return new WaitForEndOfFrame();
                    _input.caretPosition = _input.text.Length;
                    _input.ForceLabelUpdate();
                }
                StartCoroutine(UpdateCaret());
            }
            if (_textAsset.text == _input.text)
            {
                Debug.Log("Minigame done");
            }
            Debug.Log("|" + _textAsset.text[..length] + "|");
        }
    }
}
