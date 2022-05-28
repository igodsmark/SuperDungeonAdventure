using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] float goldValue = 3f;
    // Start is called before the first frame update

    public float PickUpItem()
    {
        gameObject.SetActive(false);
        return goldValue;
    }
}
