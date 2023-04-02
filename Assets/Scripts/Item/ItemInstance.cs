using MiniJamGame16.SO;
using MiniJamGame16.World;
using UnityEngine;

namespace MiniJamGame16.Item
{
    public class ItemInstance : MonoBehaviour
    {
        public ItemInfo Info { set; get; }

        private bool _isUsed;
        public bool IsUsed
        {
            set
            {
                _isUsed = value;
                _rb.velocity = Vector2.zero;
                _rb.bodyType = value ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
                _rb.gravityScale = value ? 0f : 1f;
            }
            get => _isUsed;
        }

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void SetXVelocity(float value)
        {
            _rb.velocity = new(value, _rb.velocity.y);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Burner"))
            {
                var burner = collision.collider.GetComponent<BurningFurnace>();
                burner.TriggerBurningVFX();
                burner.Burn();
                Destroy(gameObject);
            }
        }
    }
}
