using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] [Range(0f, 1f)] float fadeSpeed = 1f; 
    [SerializeField] GameObject fadePanel;
    [SerializeField] GameObject playerKnight;
    [SerializeField] GameObject playerZombie;
    [SerializeField] Cinemachine.CinemachineVirtualCamera vCamera;
    bool zombie = false;
    Material fadeMaterial;

    public event EventHandler Faded;

    protected virtual void OnFaded(EventArgs e)
    {
        EventHandler handler = Faded;
        handler?.Invoke(this, e);
    }

    // Start is called before the first frame update
    void Start()
    {
        fadeMaterial = fadePanel.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeOut()
    {
        StartCoroutine(CoFadeOut());
    }

    public void FadeIn()
    {
        StartCoroutine(CoFadeIn());

    }

    IEnumerator CoFadeOut()
    {
        Color c = fadeMaterial.color;
        
        for (float alpha = 0f; alpha < 1; alpha += (Time.deltaTime / fadeSpeed))
        {

            c.a = alpha * alpha * alpha;
            fadeMaterial.color = c;
            yield return null;
        }
        c.a = 1f;
        fadeMaterial.color = c;
        Faded?.Invoke(this, EventArgs.Empty);
    }
    IEnumerator CoFadeIn()
    {
        Color c = fadeMaterial.color;
        for (float alpha = 1f; alpha >= 0; alpha -= (Time.deltaTime / fadeSpeed))
        {
            c.a = alpha * alpha * alpha;
            fadeMaterial.color = c;
            yield return null;
        }
        c.a = 0f;
        fadeMaterial.color = c;
        Debug.Log("Faded back in");
    }

    internal void SwitchPlayer()
    {
        Debug.Log("Swapping");
        if (zombie)
        {
            Vector3 position = playerZombie.transform.position;
            playerZombie.SetActive(false);
            playerKnight.SetActive(true);
            playerKnight.transform.position = position;
            vCamera.Follow = playerKnight.transform;
            zombie = false;
        }
        else
        {
            Vector3 posistion = playerKnight.transform.position;
            playerZombie.SetActive(true);
            playerKnight.SetActive(false);
            playerZombie.transform.position = posistion;
            vCamera.Follow = playerZombie.transform;
            zombie = true;
        }
    }
}
