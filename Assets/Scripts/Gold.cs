using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public void AddGold(int gold)
    {
        if(gold > 0)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            FindObjectOfType<GameManager>().AddGold(gold);
            gameManager.DisplayText(gameObject.transform.position, $"+{gold} gold");

        }
    }
}
