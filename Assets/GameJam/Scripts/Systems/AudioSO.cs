using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create new AudioData", fileName = "New AudioData")]
public class AudioSO : ScriptableObject
{
    public AudioClip clip;
    public float bpm;
}
