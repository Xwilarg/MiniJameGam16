using UnityEngine;
using System.Collections;

namespace MiniJamGame16.World
{
    public class BurningFurnace : MonoBehaviour
    {
        [SerializeField]
        private Material _fireMat;

        [SerializeField]
        private ParticleSystem _smokeVFX;

        [SerializeField]
        [ColorUsage(true, true)]
        private Color _fireMatColor, _fireMatBurstColor;

        [SerializeField]
        private float _burstDuration = 0.4f;

        private void Awake()
        {
            _fireMat.SetColor("_GlowColor", _fireMatColor);
        }

        public void TriggerBurningVFX()
    
        {
            StartCoroutine(GlowColorBurst());
            _smokeVFX.Play();
        }

        private IEnumerator GlowColorBurst()
        {
            float step = _burstDuration / 10;

            for (float t = 0f; t <= _burstDuration/2; t += step)
            {
                _fireMat.SetColor("_GlowColor", Color.Lerp(_fireMatColor, _fireMatBurstColor, t));
                yield return new WaitForSeconds(step);
            }
            yield return new WaitForSeconds(_burstDuration/2);
            
            for (float t = _burstDuration/2; t >= 0; t -= step)
            {
                _fireMat.SetColor("_GlowColor", Color.Lerp(_fireMatColor, _fireMatBurstColor, t));
                yield return new WaitForSeconds(step);
            }
            yield return new WaitForSeconds(_burstDuration/2);

            _fireMat.SetColor("_GlowColor", _fireMatColor);
            yield return null;
        }
    }
}
