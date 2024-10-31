using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicManager : MonoBehaviour
{
    public int beat;

    [SerializeField] private float bpm;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Intervals[] intervals;
    [SerializeField] private AudioSO[] audios;

    private void Start()
    {
        SetAudio(0);
    }

    public void SetAudio(int index)
    {
        beat = 0;
        bpm = audios[index].bpm;
        audioSource.clip = audios[index].clip;
        audioSource.Play();
    }

    private void Update()
    {
        //Debug:

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    float tiem = (audioSource.timeSamples / (audioSource.clip.frequency * intervals[0].GetIntervalLength(bpm)));
        //    Debug.Log(WithinBeatRange(.35f) + ", [" + (tiem - Mathf.FloorToInt(tiem)).ToString() + "]");
        //}

        foreach(Intervals interval in intervals)
        {
            float sampledTime = (audioSource.timeSamples / (audioSource.clip.frequency * interval.GetIntervalLength(bpm)));
            interval.CheckForNewInterval(sampledTime);
        }
    }

    public bool WithinBeatRange(float range, int interval = 0)
    {
        bool onBeat = false;

        float halfRange = range / 2;

        float sampledTime = (audioSource.timeSamples / (audioSource.clip.frequency * intervals[interval].GetIntervalLength(bpm)));
        float timeDecimal = sampledTime - Mathf.FloorToInt(sampledTime);

        onBeat = (timeDecimal >= 1 - halfRange||timeDecimal <= halfRange);

        return onBeat;
    }

    public void NextBeat()
    {
        beat++;
        if (beat > 4)
        {
            beat = 1;
        }

        InvokeActions();
    }

    public void InvokeActions()
    {
        BeatReciever[] recievers = FindObjectsOfType<BeatReciever>();
        foreach(BeatReciever reciever in recievers)
        {
            reciever.Recieve(beat);
        }
    }
}

[System.Serializable]
public class Intervals
{
    [SerializeField] private float steps;
    [SerializeField] private UnityEvent trigger;
    private int lastInterval = -1;

    public float GetIntervalLength(float bpm)
    {
        return 60f/(bpm*steps);
    }

    public void CheckForNewInterval(float interval)
    {
        if(Mathf.FloorToInt(interval) != lastInterval)
        {
            lastInterval = Mathf.FloorToInt(interval);
            trigger.Invoke();
        }
    }
}
