using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXPlayer : MonoBehaviour
{
    public AudioClip[] Choices;
    public AudioSource source;
    public float playTime;

    float time;

    public void PlayClip()
    {
        if(time <= 0)
        {
            source.clip = Choices[Random.Range(0, Choices.Length)];
            source.Play();
            time = playTime;
        }
    }

    private void Update()
    {
        time -= Time.deltaTime;
    }
}
