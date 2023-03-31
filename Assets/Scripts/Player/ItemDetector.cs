using MiniJamGame16.Item;
using MiniJamGame16.SO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MiniJamGame16.Player
{
    public class ItemDetector : MonoBehaviour
    {
        private readonly List<ItemInstance> _items = new();

        public ItemInstance Item
            => _items.Any() ? _items[0] : null;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Item"))
            {
                _items.Add(collision.gameObject.GetComponent<ItemInstance>());
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            _items.RemoveAll(x => x.gameObject.GetInstanceID() == collision.gameObject.GetInstanceID());
        }
    }
}
