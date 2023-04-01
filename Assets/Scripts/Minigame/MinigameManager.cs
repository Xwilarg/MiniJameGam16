using MiniJamGame16.Translation;
using MiniJamGame16.Tutorial;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace MiniJamGame16.Minigame
{
    public class MinigameManager : MonoBehaviour
    {
        public static MinigameManager Instance { get; private set; }

        [SerializeField]
        private MinigameAssociation[] _minigames;

        [SerializeField]
        private GameObject _floxbutton;

        [SerializeField]
        private TMP_Text _inspectionText;
        private int _inspectionPercent;

        public bool IsInspectionOn { private set; get; }

        public bool IsActive { private set; get; }

        public Action _onDone;

        private void Awake()
        {
            Instance = this;
            _inspectionText.text = $"{Translate.Instance.Tr("INSPECTIONCHANCES").ToUpperInvariant()} 0%";
            _inspectionText.color = Color.white;
            foreach (var game in _minigames)
            {
                game.Minigame.OnDone += (_sender, _e) =>
                {
                    IsActive = false;
                    game.Minigame.gameObject.SetActive(false);
                    _floxbutton.SetActive(false);
                    TutorialManager.Instance.AdvanceTutorial(TutorialAdvancement.GAME_END);
                    _onDone();
                    _inspectionPercent += 10;
                    if (_inspectionPercent > 100) _inspectionPercent = 100;
                    _inspectionText.text = $"{Translate.Instance.Tr("INSPECTIONCHANCES").ToUpperInvariant()} {_inspectionPercent}%";
                    _inspectionText.color = Color.white;
                };
            }
        }

        public void Enable(MinigameType type, Action onDone)
        {
            _onDone = onDone;
            var target = _minigames.FirstOrDefault(x => x.Type == type);
            Assert.IsNotNull(target);
            target.Minigame.gameObject.SetActive(true);
            target.Minigame.Init();
            IsInspectionOn = UnityEngine.Random.Range(0, 100) < _inspectionPercent;
            _floxbutton.SetActive(!IsInspectionOn);
            if (IsInspectionOn)
            {
                _inspectionText.text = Translate.Instance.Tr("INSPECTIONON");
                _inspectionText.color = Color.red;
            }
            IsActive = true;
        }

        public void UseFlox()
        {
            _floxbutton.SetActive(false);
            IsActive = false;
            foreach (var game in _minigames)
            {
                game.Minigame.gameObject.SetActive(false);
            }
            TutorialManager.Instance.AdvanceTutorial(TutorialAdvancement.GAME_END);
            _onDone();
            _inspectionPercent -= 50;
            if (_inspectionPercent < 0) _inspectionPercent = 0;
            _inspectionText.text = $"{Translate.Instance.Tr("INSPECTIONCHANCES").ToUpperInvariant()} {_inspectionPercent}%";
            _inspectionText.color = Color.white;
        }
    }
}
