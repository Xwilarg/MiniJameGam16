using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace MiniJamGame16.Minigame
{
    public class MinigameManager : MonoBehaviour
    {
        public static MinigameManager Instance { get; private set; }

        [SerializeField]
        private MinigameAssociation[] _minigames;

        public bool IsActive { private set; get; }

        public Action _onDone;

        private void Awake()
        {
            Instance = this;
            foreach (var game in _minigames)
            {
                game.Minigame.OnDone += (_sender, _e) =>
                {
                    IsActive = false;
                    game.Minigame.gameObject.SetActive(false);
                    _onDone();
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
            IsActive = true;
        }
    }
}
