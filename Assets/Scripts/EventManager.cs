using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public event EventHandler Faded;
    public event EventHandler SwitchGameMode;
    public event EventHandler FinishedText;
    public event EventHandler StartChasing;

    protected virtual void OnStartChasing(EventArgs e)
    {
        EventHandler handler = StartChasing;
        handler?.Invoke(this, e);
    }
    public void StartChase()
    {
        OnStartChasing(EventArgs.Empty);
    }

    protected virtual void OnFinishedText(EventArgs e)
    {
        EventHandler eventHandler = FinishedText;
        eventHandler?.Invoke(this, e);
    }

    public void FinishText()
    {
        OnFinishedText(EventArgs.Empty);
    }

    protected virtual void OnSwitchGameMode(EventArgs e)
    {
        Debug.Log("Switched game mode - event");
        EventHandler eventHandler = SwitchGameMode;
        eventHandler?.Invoke(this, e);
    }
    public void SwitchedGameMode()
    {
        OnSwitchGameMode(EventArgs.Empty);
    }

    protected virtual void OnFaded(EventArgs e)
    {
        EventHandler handler = Faded;
        handler?.Invoke(this, e);
    }

    public void FadedOut()
    {
        OnFaded(EventArgs.Empty);
    }




}
