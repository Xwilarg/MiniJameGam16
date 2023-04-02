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

        public float Volume { set; get; } = 1f;
        public bool DifficultyHard { set; get; }
    }
}
