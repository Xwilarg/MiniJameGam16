using MiniJamGame16.SO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MiniJamGame16.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerInfo _info;

        private Rigidbody2D _rb;
        private ItemDetector _detector;
        private Vector2 _mov;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _detector = GetComponentInChildren<ItemDetector>();
        }

        private void FixedUpdate()
        {
            _rb.velocity = new(_mov.x * Time.fixedDeltaTime * _info.Speed, _rb.velocity.y);
        }

        public void OnMove(InputAction.CallbackContext value)
        {
            if (_mov.magnitude != 0f && _detector.Item != null)
            {
                _detector.Item.IsUsed = false; // We moved, is we were using an object we aren't anymore
            }
            _mov = value.ReadValue<Vector2>().normalized;
        }

        public void OnAction(InputAction.CallbackContext value)
        {
            if (value.performed && _detector.Item != null)
            {
                _detector.Item.IsUsed = !_detector.Item.IsUsed;
                if (_detector.Item.IsUsed)
                {
                    _detector.Item.transform.position = transform.position;
                    _mov = Vector2.zero;
                }
            }
        }
    }
}
