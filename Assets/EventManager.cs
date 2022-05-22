using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public event EventHandler Faded;

    protected virtual void OnFaded(EventArgs e)
    {
        EventHandler handler = Faded;
        handler?.Invoke(this, e);
    }


}
