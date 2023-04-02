using MiniJamGame16.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private AudioClip _startAudio;

    private void Start()
    {
        AudioSystem.Instance.PlayMusic(_startAudio);
    }
}
