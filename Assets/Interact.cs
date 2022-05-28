using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gold))]
public class Interact : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] int gold = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteractWith()
    {
        GetComponent<Gold>().AddGold(gold);
        gold = 0;
        animator.SetBool("ChestOpen", true);
        int childCount = transform.childCount;
        for(int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if(child.gameObject.name == "Sparkles")
            {
                child.gameObject.GetComponent<ParticleSystem>().Stop();
                Debug.Log("Stopped particle");
            }
        }
    }
}
