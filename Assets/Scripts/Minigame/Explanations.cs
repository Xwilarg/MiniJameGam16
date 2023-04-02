using MiniJamGame16.Translation;
using TMPro;
using UnityEngine;

namespace MiniJamGame16.Minigame
{
    public class Explanations : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        public void SetExplanations(string s)
        {
            _text.text = Translate.Instance.Tr(s);
        }
    }
}
