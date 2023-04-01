using MiniJamGame16.Minigame;
using MiniJamGame16.SO;
using MiniJamGame16.Tutorial;
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
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _detector = GetComponentInChildren<ItemDetector>();
            _animator = GetComponentInChildren<Animator>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            _rb.velocity = new(_mov.x * Time.fixedDeltaTime * _info.Speed, _rb.velocity.y);
            _animator.SetBool("IsWalking", _mov.x != 0f);
            if (_mov.x < 0) {
                _spriteRenderer.flipX = true;
            } else if (_mov.x > 0) {
                _spriteRenderer.flipX = false;
            }
        }

        public void OnMove(InputAction.CallbackContext value)
        {
            if (MinigameManager.Instance.IsActive)
            {
                return;
            }
            if (_mov.magnitude != 0f && _detector.Item != null)
            {
                _detector.Item.IsUsed = false; // We moved, is we were using an object we aren't anymore
                _animator.SetBool("IsGrabbing", false);
            }
            _mov = value.ReadValue<Vector2>().normalized;
        }

        public void OnAction(InputAction.CallbackContext value)
        {
            if (value.performed && _detector.Item != null && !MinigameManager.Instance.IsActive)
            {
                _detector.Item.IsUsed = !_detector.Item.IsUsed;
                if (_detector.Item.IsUsed)
                {
                    TutorialManager.Instance.AdvanceTutorial(TutorialAdvancement.GRAB_OBJECT);
                    _animator.SetBool("IsGrabbing", true);
                    _detector.Item.transform.position = transform.position;
                    _mov = Vector2.zero;
                    MinigameManager.Instance.Enable(_detector.Item.Info.Minigame, () =>
                    {
                        _animator.SetBool("IsGrabbing", false);
                        _detector.Item.IsUsed = false;
                        Destroy(_detector.Item.gameObject);
                    });
                } else {
                    _animator.SetBool("IsGrabbing", false);
                }
            }
        }
    }
}
