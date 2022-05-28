using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] [Range(0f, 1f)] float fadeSpeed = 1f; 
    [SerializeField] GameObject fadePanel;
    [SerializeField] GameObject playerKnight;
    [SerializeField] GameObject playerZombie;
    [SerializeField] int gold = 0;
    [SerializeField] Cinemachine.CinemachineVirtualCamera vCamera;
    [SerializeField] TMPro.TMP_Text scoreUI;
    [SerializeField] GameObject textPrefab;
    [SerializeField] int goldThreshold = 50;
    [SerializeField] GameObject menu;
    public bool CanMove { get { return canMove; } }
    bool canMove = true;
    bool isMenuDisplayed = false;

    bool zombie = false;
    Material fadeMaterial;

    public event EventHandler Faded;
    public event EventHandler SwitchGameMode;

    private void Awake()
    {
        UpdateScore();
        menu.SetActive(false);
    }


    private void UpdateScore()
    {
        scoreUI.text = "Gold: " + gold;
    }

    protected virtual void OnFaded(EventArgs e)
    {
        EventHandler handler = Faded;
        handler?.Invoke(this, e);
    }

    protected virtual void OnSwitchGameMode(EventArgs e)
    {
        EventHandler handler = SwitchGameMode;
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
        if (Input.GetButtonDown("Cancel"))
        {
            
            if (isMenuDisplayed)
            {                
                menu.SetActive(false);
                canMove = true;
                isMenuDisplayed = false;
            }
            else
            {                
                menu.SetActive(true);
                canMove=false;
                isMenuDisplayed=true;
            }
        }
    }

    public void FadeOut()
    {
        canMove = false;
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
        canMove = true;
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

    public void AddGold(int goldAmount)
    {
        gold += goldAmount;
        UpdateScore();
        if(gold > goldThreshold)
        {
            Zombify();
        }
    }

    public void DisplayText(Vector3 position, string text)
    {
        GameObject textHolder = Instantiate(textPrefab, position, Quaternion.identity);
        textHolder.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(text);
    }

    public void Zombify()
    {
        SwitchPlayer();
        SwitchGameMode?.Invoke(this, EventArgs.Empty);
    }
}
