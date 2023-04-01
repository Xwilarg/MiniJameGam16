using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MiniJamGame16.Minigame.Impl
{
    public class WireGame : AMinigame
    {
        private CustomLineRenderer _target;

        [SerializeField]
        private Wire[] _wires;

        public override void Init()
        {
            if (_target != null)
            {
                _target.enabled = false;
            }
            _target = null;

            _wires.OrderBy(_ => Random.value);
            for (int i = 0; i < _wires.Length; i++)
            {
                var target = i + 1;
                var a = Random.Range(3, 20);
                var b = Random.Range(3, 20);
                var c = (a * b) - target;
                _wires[i].SetMath($"{a} * {b} - {c}", target);
            }
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

        public void EndLR(Transform t)
        {
            var button = (RectTransform)_target.transform.parent.transform;
            if (_target != null)
            {
                _target.SetPositions(new[]
                {
                    Vector3.zero,
                    t.position - transform.position
                    - button.localPosition,
                });
                _target = null;
            }
        }

        private void Update()
        {
            if (_target != null)
            {
                var button = (RectTransform)_target.transform.parent.transform;
                _target.SetPositions(new[]
                {
                    Vector3.zero,
                    (Vector3)Mouse.current.position.value - transform.position
                    - button.localPosition,
                });
            }
        }
    }
}
