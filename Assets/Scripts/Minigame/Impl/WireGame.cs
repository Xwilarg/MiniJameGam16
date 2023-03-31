using UnityEngine;
using UnityEngine.InputSystem;

namespace MiniJamGame16.Minigame.Impl
{
    public class WireGame : AMinigame
    {
        private CustomLineRenderer _target;

        public override void Init()
        {
            if (_target != null)
            {
                _target.enabled = false;
            }
            _target = null;
        }

        public void SetLR(CustomLineRenderer lr)
        {
            if (_target != null)
            {
                _target.enabled = false;
            }
            _target = lr;
            _target.enabled = true;
        }

        private void Update()
        {
            if (_target != null)
            {
                var container = (RectTransform)_target.transform.parent.parent.transform;
                _target.SetPositions(new[]
                {
                    Vector3.zero,
                    (Vector3)Mouse.current.position.value - transform.position
                    + new Vector3(container.sizeDelta.x / 2f, -container.sizeDelta.y / 2f),
                });
            }
        }
    }
}
