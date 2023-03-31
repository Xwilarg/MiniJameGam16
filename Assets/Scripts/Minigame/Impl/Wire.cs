using UnityEngine;

namespace MiniJamGame16.Minigame.Impl
{
    public class Wire : MonoBehaviour
    {
        [SerializeField]
        private WireGame _manager;

        private LineRenderer _lr;

        private void Awake()
        {
            _lr = GetComponent<LineRenderer>();
        }

        public void Enable()
        {
            _manager.SetLR(_lr);
        }
    }
}
