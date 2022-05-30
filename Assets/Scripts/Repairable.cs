using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repairable : MonoBehaviour
{
    [SerializeField] GameObject brokenState;
    [SerializeField] GameObject repairedState;
    [SerializeField] bool broken = true;

    private void Awake()
    {
        if (broken)
        {
            brokenState.SetActive(true);
            repairedState.SetActive(false);
        }
    }

    public bool Repair()
    {
        if (broken)
        {
            brokenState.SetActive(false);
            repairedState.SetActive(true);
            broken = false;
            return true;
        }
        return false;

    }

    public void ToggleState()
    {
        if (broken)
        {
            brokenState.SetActive(false);
            repairedState.SetActive(true);
            broken = false;
        }
        else
        {
            brokenState.SetActive(true);
            repairedState.SetActive(false);
            broken = true;
        }
    }
}
