using MiniJamGame16.Item;
using System.Collections.Generic;
using UnityEngine;

namespace MiniJamGame16.World
{
    public class ConveyorBelt : MonoBehaviour
    {
        [SerializeField]
        private float Speed;

        private readonly List<ItemInstance> _items = new();

        private void FixedUpdate()
        {
            foreach (var item in _items)
            {
                if (!item.IsUsed)
                {
                    item.SetXVelocity(Speed * Time.fixedDeltaTime);
                }
            }
        }

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
