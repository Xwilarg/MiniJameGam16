using UnityEngine;

namespace MiniJamGame16.Menu
{
    public class GlobalOptions : MonoBehaviour
    {
        public static GlobalOptions Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (Instance.gameObject.GetInstanceID() != gameObject.GetInstanceID())
                {
                    Destroy(gameObject);
                }
            }
        }

        public float Volume { set; get; } = .5f;
        public bool DifficultyHard { set; get; }

        public int MineweeperSize => DifficultyHard ? 10 : 5;
        public int MineweeperCount => DifficultyHard ? 10 : 3;
        public int WiresMax => DifficultyHard ? 20 : 10;
        public int RelationshipMin => DifficultyHard ? 755 : 255;
        public int RelationshipBeatCount => DifficultyHard ? 10 : 5;

        public int Score { set; get; }
    }
}
