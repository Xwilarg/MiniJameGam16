using UnityEngine;

namespace MiniJamGame16.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/SpawnerInfo", fileName = "SpawnerInfo")]
    public class SpawnerInfo : ScriptableObject
    {
        [Tooltip("Time in seconds between 2 object spawn")]
        public float TimeBetweenSpawn;

        public ItemInfo[] Items;
    }
}