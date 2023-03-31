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

        private GameObject _initor;

        private void Awake()
        {
            Instance = this;
            foreach (var game in _minigames)
            {
                game.Minigame.OnDone += (_sender, _e) =>
                {
                    IsActive = false;
                    game.Minigame.gameObject.SetActive(false);
                    Destroy(_initor);
                    _initor = null;
                };
            }
        }

        public void Enable(MinigameType type, GameObject initor)
        {
            _initor = initor;
            var target = _minigames.FirstOrDefault(x => x.Type == type);
            Assert.IsNotNull(target);
            target.Minigame.gameObject.SetActive(true);
            target.Minigame.Init();
            IsActive = true;
        }
    }
}
