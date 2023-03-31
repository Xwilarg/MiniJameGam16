using System;
using UnityEngine;

namespace MiniJamGame16.Minigame
{
    public abstract class AMinigame : MonoBehaviour
    {
        public abstract void Init();

        protected void Complete()
        {
            OnDone.Invoke(this, new());
        }

        public event EventHandler OnDone;
    }
}
