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

            _wires = _wires.OrderBy(_ => Random.value).ToArray();
            for (int i = 0; i < _wires.Length; i++)
            {
                var target = i + 1;
                var a = Random.Range(3, 10);
                var b = Random.Range(3, 10);
                var c = (a * b) - target;
                _wires[i].SetMath($"{a} x {b} - {c}", target);
            }
        }

        public void SetLR(Wire w, CustomLineRenderer lr)
        {
            w.SetDest(-1);
            if (_target != null)
            {
                _target.enabled = false;
            }
            _target = lr;
            _target.enabled = true;
        }

        public void EndLR(WireOut w)
        {
            if (_target != null)
            {
                _target.transform.parent.GetComponent<Wire>().SetDest(w.ID); // TODO: Clean
                var button = (RectTransform)_target.transform.parent.transform;
                _target.SetPositions(new[]
                {
                    Vector3.zero,
                    w.transform.position - transform.position
                    - button.localPosition + Vector3.up * 15f,
                });
                _target = null;
                foreach (var wire in _wires)
                {
                    if (!wire.IsValid)
                    {
                        return;
                    }
                }
                Complete();
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
                    - button.localPosition + Vector3.up * 15f,
                });
            }
        }
    }
}
