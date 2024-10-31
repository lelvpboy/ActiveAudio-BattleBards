using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatReciever : MonoBehaviour
{
    [SerializeField] private BeatAction[] actions;

    public void Recieve(int _beat)
    {
        foreach(BeatAction action in actions)
        {
            if(action.beat == _beat)
            {
                action.action.Invoke();
            }
        }
    }
}

[System.Serializable]
public class BeatAction
{
    public int beat;
    public UnityEvent action;
}
