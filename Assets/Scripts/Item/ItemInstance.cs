using MiniJamGame16.SO;
using UnityEngine;
using static UnityEditor.Progress;

namespace MiniJamGame16.Item
{
    public class ItemInstance : MonoBehaviour
    {
        public ItemInfo Info { set; get; }
        public bool IsUsed { set; get; }

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void SetXVelocity(float value)
        {
            _rb.velocity = new(value, _rb.velocity.y);
        }
    }
}
