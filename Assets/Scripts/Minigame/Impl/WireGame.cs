using UnityEngine;
using UnityEngine.InputSystem;

namespace MiniJamGame16.Minigame.Impl
{
    public class WireGame : AMinigame
    {
        private LineRenderer _target;

        public override void Init()
        {
            if (_target != null)
            {
                _target.enabled = false;
            }
            _target = null;
        }

        public void SetLR(LineRenderer lr)
        {
            if (_target != null)
            {
                _target.enabled = false;
            }
            _target = lr;
            _target.enabled = true;
        }

        private Vector3 Convert(Vector2 v)
        {
            return new(v.x, v.y, -1f);
        }

        private void Update()
        {
            if (_target != null)
            {
                _target.SetPositions(new[]
                {
                    Convert(_target.transform.position),
                    Convert(Mouse.current.position.value)
                });
            }
        }
    }
}
