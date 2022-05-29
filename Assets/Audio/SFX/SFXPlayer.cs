using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] AudioClip coinPickup;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PickupCoins()
    {
        audioSource.Stop();
        audioSource.clip = coinPickup;
        audioSource.loop = false;
        audioSource.Play();
    }
}
