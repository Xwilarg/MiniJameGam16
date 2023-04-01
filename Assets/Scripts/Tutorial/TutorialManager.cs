using TMPro;
using UnityEngine;

namespace MiniJamGame16.Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        public static TutorialManager Instance { private set; get; }

        [SerializeField]
        private TMP_Text[] _tutorial;

        private int _index;

        private void Awake()
        {
            Instance = this;
        }

        public void AdvanceTutorial(TutorialAdvancement id)
        {
            if ((int)id == _index)
            {
                _tutorial[_index].gameObject.SetActive(false);
                _index++;
                if (_index < _tutorial.Length)
                {
                    _tutorial[_index].gameObject.SetActive(true);
                }
            }
        }
    }
}
