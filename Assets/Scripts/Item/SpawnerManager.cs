using MiniJamGame16.Item;
using MiniJamGame16.SO;
using System.Collections;
using UnityEngine;

namespace MiniJamGame16.Player
{
    public class SpawnerManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _spawnPoint;

        [SerializeField]
        private SpawnerInfo _info;

        private void Awake()
        {
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            yield return new WaitForSeconds(_info.TimeBetweenSpawn);
            var item = _info.Items[Random.Range(0, _info.Items.Length)];
            var go = Instantiate(item.Prefab, _spawnPoint);
            go.transform.position = _spawnPoint.position;
            go.GetComponent<ItemInstance>().Info = item;
            yield return Spawn();
        }
    }
}
