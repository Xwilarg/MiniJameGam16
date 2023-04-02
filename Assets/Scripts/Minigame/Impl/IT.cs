using MiniJamGame16.Menu;
using System.Collections;
using TMPro;
using UnityEngine;

namespace MiniJamGame16.Minigame.Impl
{
    public class IT : AMinigame
    {
        [SerializeField]
        private TMP_Text _referenceText;

        [SerializeField]
        private TMP_InputField _input;

        [SerializeField]
        private TextAsset[] _textAsset;
        [SerializeField]
        private TextAsset[] _textAssetHard;
        private string _text;

        public override void Init()
        {
            var target = GlobalOptions.Instance.DifficultyHard ? _textAssetHard : _textAsset;
            _text = target[Random.Range(0, target.Length)].text.Replace("\r", "");
            _referenceText.text = _text;
            _input.text = string.Empty;
            _input.Select(); // TODO: Only works the first time?
        }

        public void OnTextEdit()
        {
            var length = _input.text.Length;
            if (length == 0)
            {
                return;
            }
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
                Complete();
            }
        }
    }
}
