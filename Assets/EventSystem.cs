using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
    public static EventSystem current;


    private void Awake()
    {
        current = this;
    }

    public event Action<int> onGoombaSquished;
    public event Action onMarioKilled;

    public void GoombaSquished(int id)
    {
        if (onGoombaSquished != null)
        {
            onGoombaSquished(id);
        }
    }

    public void MarioKilled()
    {
        if(onMarioKilled != null)
        {
            onMarioKilled();
        }
    }

}
