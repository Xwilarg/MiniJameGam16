using MiniJamGame16.Minigame;
using UnityEngine;

namespace MiniJamGame16.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/ItemInfo", fileName = "ItemInfo")]
    public class ItemInfo : ScriptableObject
    {
        public GameObject Prefab;
        public MinigameType Minigame;
    }
}