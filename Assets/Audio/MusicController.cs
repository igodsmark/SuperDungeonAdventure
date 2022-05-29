using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] AudioClip explorationMusic;
    [SerializeField] AudioClip actionMusic;
    [SerializeField] AudioClip spookyMusic;
    [SerializeField] GameManager gameManager;
    [SerializeField] EventManager eventManager;
    AudioSource musicSource;

    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = explorationMusic;
        musicSource.Play();
        eventManager.SwitchGameMode += GameManager_SwitchGameMode;
        eventManager.StartChasing += EventManager_StartChasing;
    }

    private void EventManager_StartChasing(object sender, System.EventArgs e)
    {
        musicSource.Stop();
        musicSource.clip = actionMusic;
        musicSource.Play();
    }

    private void GameManager_SwitchGameMode(object sender, System.EventArgs e)
    {
        Debug.Log("Music change!");
        musicSource.Stop();
        musicSource.clip = spookyMusic;
        musicSource.Play();
    }

}
