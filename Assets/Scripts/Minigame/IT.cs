using System.Collections;
using TMPro;
using UnityEngine;

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
        private string _text;

        private void Awake()
        {
            _text = _textAsset.text.Replace("\r", "");
            _referenceText.text = _text;
        }

        public void OnTextEdit()
        {
            var length = _input.text.Length;
            if (_text[length - 1] != _input.text[length - 1])
            {
                _input.text = _input.text[..^1];
            }
            else
            {
                while (length < _text.Length && _text[length] == '\n')
                {
                    length++;
                    _input.text = _text[..length];
                    while (length < _text.Length && (_text[length] == ' ' || _text[length] == '\t'))
                    {
                        length++;
                        _input.text = _text[..length];
                    }
                }
                IEnumerator UpdateCaret()
                {
                    yield return new WaitForEndOfFrame();
                    _input.caretPosition = _input.text.Length;
                    _input.ForceLabelUpdate();
                }
                StartCoroutine(UpdateCaret());
            }
            if (_text == _input.text)
            {
                Debug.Log("Minigame done");
            }
        }
    }
}
