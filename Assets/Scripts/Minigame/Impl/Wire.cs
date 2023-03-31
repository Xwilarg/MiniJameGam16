using UnityEngine;

namespace MiniJamGame16.Minigame.Impl
{
    public class Wire : MonoBehaviour
    {
        [SerializeField]
        private WireGame _manager;

        private CustomLineRenderer _lr;

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
