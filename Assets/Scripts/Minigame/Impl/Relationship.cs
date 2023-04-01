using Assets.Scripts.Minigame.Impl;
using MiniJamGame16.Translation;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MiniJamGame16.Minigame.Impl
{
    public class Relationship : AMinigame
    {
        [SerializeField]
        private RelationshipCursor _cursor;

        [SerializeField]
        private Button _button;
        private TMP_Text _bText;

        private int _pumpCount;

        private void Awake()
        {
            _bText = _button.GetComponentInChildren<TMP_Text>();
        }

        public override void Init()
        {
            _pumpCount = 5;
            _bText.text = $"{Translate.Instance.Tr("PUMP").ToUpperInvariant()}\n{_pumpCount} {Translate.Instance.Tr("LEFT").ToUpperInvariant()}";
            _cursor.ResetCursor();
        }

        public void Pump()
        {
            if (_cursor.IsPosOk())
            {
                _pumpCount--;
                _bText.text = $"{Translate.Instance.Tr("PUMP").ToUpperInvariant()}\n{_pumpCount} {Translate.Instance.Tr("LEFT").ToUpperInvariant()}";
                if (_pumpCount == 0)
                {
                    Complete();
                }
            }
            else
            {
                _button.interactable = false;
                StartCoroutine(Reload());
            }
        }

        private IEnumerator Reload()
        {
            yield return new WaitForSeconds(1f);
            _button.interactable = true;
        }
    }
}
