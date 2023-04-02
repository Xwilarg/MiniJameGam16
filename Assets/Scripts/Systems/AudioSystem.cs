using UnityEngine;

namespace MiniJamGame16.Systems
{
    public class AudioSystem : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource;

        private static AudioSystem _instance;
        public static AudioSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("Audio System", typeof(AudioSystem), typeof(AudioSource)).GetComponent<AudioSystem>();
                    _instance._musicSource = _instance.GetComponent<AudioSource>();
                    _instance._musicSource.loop = true;
                }
                return _instance;
            }
        }

        public void PlayMusic(AudioClip clip) {
            _musicSource.clip = clip;
            _musicSource.Play();
        }

        public void PlaySound(AudioClip clip, float vol = 1) {
            _musicSource.PlayOneShot(clip, vol);
        }
    }
}