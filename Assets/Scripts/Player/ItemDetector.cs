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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Item"))
            {
                _items.Add(collision.gameObject.GetComponent<ItemInstance>());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _items.RemoveAll(x => x.gameObject.GetInstanceID() == collision.gameObject.GetInstanceID());
        }
    }
}
