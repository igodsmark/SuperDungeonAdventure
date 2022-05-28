using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gold))]
public class Pickup : MonoBehaviour
{
    [SerializeField] int goldValue = 5;
    // Start is called before the first frame update

    public void PickUpItem()
    {
        gameObject.SetActive(false);
        if(goldValue > 0)
        {
            GetComponent<Gold>().AddGold(goldValue);
            goldValue = 0;

        }
    }
}
