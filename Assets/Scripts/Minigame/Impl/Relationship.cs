using Assets.Scripts.Minigame.Impl;
using UnityEngine;

namespace MiniJamGame16.Minigame.Impl
{
    public class Relationship : AMinigame
    {
        [SerializeField]
        private RelationshipCursor _cursor;

        private int _pumpCount;

        public override void Init()
        {
            _pumpCount = 10;
            _cursor.ResetCursor();
        }

        public void Pump()
        {
            if (_cursor.IsPosOk())
            {
                _pumpCount--;
                if (_pumpCount == 0)
                {
                    Complete();
                }
            }
        }
    }
}
