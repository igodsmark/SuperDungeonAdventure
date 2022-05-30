using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tidying : MonoBehaviour
{
    GameManager gameManager;
    int numberToTidy = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        var tidying = GameObject.FindGameObjectsWithTag("RepairTarget");
        numberToTidy = tidying.Length;
        gameManager.SetTasks(numberToTidy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Tidied()
    {
        numberToTidy--;
        gameManager.SetTasks(numberToTidy);
    }
}
