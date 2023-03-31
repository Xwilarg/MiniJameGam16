using System.Collections.Generic;
using UnityEngine;

namespace MiniJamGame16.World
{
    public class ConveyorBelt : MonoBehaviour
    {
        [SerializeField]
        private float Speed;

        private readonly List<Rigidbody2D> _items = new();

        private void FixedUpdate()
        {
            foreach (var item in _items)
            {
                item.velocity = new(Speed * Time.deltaTime, item.velocity.y);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Item"))
            {
                _items.Add(collision.gameObject.GetComponent<Rigidbody2D>());
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            _items.RemoveAll(x => x.gameObject.GetInstanceID() == collision.gameObject.GetInstanceID());
        }
    }
}
