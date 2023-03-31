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
            var go = Instantiate(_info.Items[Random.Range(0, _info.Items.Length)].Prefab, _spawnPoint);
            go.transform.position = _spawnPoint.position;
            yield return Spawn();
        }
    }
}
