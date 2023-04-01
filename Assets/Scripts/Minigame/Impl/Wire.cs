using TMPro;
using UnityEngine;

namespace MiniJamGame16.Minigame.Impl
{
    public class Wire : MonoBehaviour
    {
        [SerializeField]
        private WireGame _manager;

        [SerializeField]
        private TMP_Text _math;

        private CustomLineRenderer _lr;

        private int _target;
        private int _dest;

        public void SetMath(string eq, int target)
        {
            _math.text = eq;
            _target = target;
            _dest = -1;
            _lr.SetPositions(new[] { Vector3.zero, Vector3.zero });
        }

        public void SetDest(int value)
        {
            _dest = value;
        }

        public bool IsValid => _target == _dest;

        private void Awake()
        {
            _lr = GetComponentInChildren<CustomLineRenderer>();
        }

        public void Enable()
        {
            _manager.SetLR(_lr);
        }
    }
}
