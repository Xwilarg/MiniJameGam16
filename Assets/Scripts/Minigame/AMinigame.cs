using MiniJamGame16.Player;
using MiniJamGame16.SO;
using System;
using UnityEngine;

namespace MiniJamGame16.Minigame
{
    public abstract class AMinigame : MonoBehaviour
    {
        public abstract void Init();

        protected void Complete(int miniGameScoreValue)
        {
            //The issue is that the value passed through to here is always zero. You'll see a step back that the game has a score, that score is set above 0.
            //This means right now, miniGameScoreValue has been, our real value (Example: 200) should be 200.
            //But it is not. It is zero in this method right now.
            //No idea why this is so. Something to be investigated in the future.

            OnDone.Invoke(this, new());
            Debug.Log("Mini-Game Completed.\nThe score should increase by " +miniGameScoreValue);
            
            PlayerController _playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            _playerInfo.IncreasePlayerScore(miniGameScoreValue); //The real value should be 200, as per example. But 0 will be passed through. It starts with the function parameter.
        }

        public event EventHandler OnDone;
    }
}
