using MiniJamGame16.Item;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace MiniJamGame16.World
{
    public class BurningFurnace : MonoBehaviour
    {
        [SerializeField]
        private Material _fireMat;

        [SerializeField]
        [ColorUsage(true, true)]
        private Color _fireMatBurstColor;
        [SerializeField]
        [ColorUsage(true, true)]
        private Color _fireMatColor;
       
        [SerializeField]
        private ParticleSystem _smokeVFX;

        public void TriggerBurningVFX()
        {
            // TODO:
            // StartCoroutine(GlowFireColor());
            _smokeVFX.Play();
        }

        // private IEnumerator GlowFireColor()
        // {
            // Color originalColor = _fireMat.GetColor("_GlowColor");
            
            // _fireMat.SetColor("_GlowColor", _fireMatBurstColor);
            // yield return new WaitForSeconds();
        // }
    }
}
