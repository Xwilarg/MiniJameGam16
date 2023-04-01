using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace MiniJamGame16.Translation
{
    public class TMP_TextTranslate : MonoBehaviour
    {
        [SerializeField]
        private bool _capitalize;

        private string _original;

        private void Start()
        {
            _original = GetComponent<TMP_Text>().text;
            UpdateText();
        }

        private string FixFrench(string str)
        {
            return str
                .Replace('à', 'a')
                .Replace('è', 'e')
                .Replace('é', 'e');
        }

        public void UpdateText()
        {
            var sentence = _original;
            foreach (var match in Regex.Matches(_original, "{([^}]+)}").Cast<Match>())
            {
                sentence = sentence.Replace(match.Value, Translate.Instance.Tr(match.Groups[1].Value));
            }
            var s = FixFrench(sentence);
            if (_capitalize)
            {
                s = s.ToUpperInvariant();
            }
            GetComponent<TMP_Text>().text = s;
        }
    }
}