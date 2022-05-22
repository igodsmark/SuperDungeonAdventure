using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] GameObject destination;

    GameManager gameManager;

    GameObject teleportee;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collided");
        if (collision.transform.CompareTag("Player"))
        {
            
            //Teleport player (screenfade)
            gameManager.FadeOut();
            gameManager.Faded += Faded;
            teleportee = collision.gameObject;

        }
        else
        {
            //teleport NPC

        }
    }

    void Faded(object sender, EventArgs args)
    {
        teleportee.GetComponent<PlayerController>().TeleportTo(destination.transform.position);
        gameManager.FadeIn();
    }


}
