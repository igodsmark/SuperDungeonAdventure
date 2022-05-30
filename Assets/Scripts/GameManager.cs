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
    [SerializeField] GameObject necromancer;
    [SerializeField] float necromancerDelay = 10f;
    [SerializeField] int gold = 0;
    [SerializeField] Cinemachine.CinemachineVirtualCamera vCamera;
    [SerializeField] TMPro.TMP_Text scoreUI;
    [SerializeField] GameObject textPrefab;
    [SerializeField] int goldThreshold = 50;
    [SerializeField] GameObject menu;
    [SerializeField] Dialogue dialogue;
    [SerializeField] SFXPlayer sfxPlayer;
    [SerializeField] EventManager eventManager;
    [SerializeField] GameObject winScreen;

    [SerializeField] float movementFactor = 1f;

    bool teleporting = false;
    public bool Teleporting { get { return teleporting; } }
    public void SetTeleporting(bool teleport)
    {
        teleporting = teleport;
    }
    int jobsLeft = 0;

    bool isLooting = true;
    public bool IsLooting { get { return isLooting; } }
    public float MovementFactor { get { return movementFactor; }  }

    Necromancer necroBrain;
    public bool CanMove { get { return canMove; } }
    bool canMove = true;
    bool isMenuDisplayed = false;


    bool zombie = false;
    Material fadeMaterial;
    bool necroActive = false;

    public event EventHandler Faded;
    //public event EventHandler SwitchGameMode;

    

    public void Teleported(Vector3 destination)
    {
        Debug.Log("Teleported player to: " + destination);
        necroBrain.SetLastKnown(destination);
        canMove = true;
        teleporting = false;
        movementFactor = 1f;
    }


    private void Awake()
    {
        canMove = false;
        UpdateScore();
        menu.SetActive(false);
        necroBrain = necromancer.GetComponent<Necromancer>();
        eventManager.FinishedText += Dialogue_FinishedText;
        DisplayIntroText();
    }

    private void DisplayIntroText()
    {
        List<string> text = new List<string>() { "Find the treasure!", "Avoid the Necromancer!" };
        DisplayDialogue(text);
    }

    private void Dialogue_FinishedText(object sender, EventArgs e)
    {
        
        canMove = true;
        movementFactor = 1f;


    }

    private void UpdateScore()
    {
        if (IsLooting)
        {
            scoreUI.text = "Gold: " + gold;
        }
    }

    public void SetTasks(int tasks)
    {
        scoreUI.text = "Jobs left: " + tasks;
        if(tasks <= 0)
        {
            winScreen.SetActive(true);
        }
    }


    protected virtual void OnFaded(EventArgs e)
    {
        EventHandler handler = Faded;
        handler?.Invoke(this, e);
    }

    //protected virtual void OnSwitchGameMode(EventArgs e)
    //{
    //    EventHandler handler = SwitchGameMode;
    //    handler?.Invoke(this, e);
    //}

    internal void TriggerPlayerSwitch()
    {
        eventManager.FinishedText += PlayerSwitch;
    }

    private void PlayerSwitch(object sender, EventArgs e)
    {
        eventManager.FinishedText -= PlayerSwitch;
        SwitchPlayer();
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
        movementFactor = 0f;
        
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
        OnFaded(EventArgs.Empty);
        //Faded?.Invoke(this, EventArgs.Empty);
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
        teleporting = false;
    }

    internal void SwitchPlayer()
    {
        
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
        isLooting = !isLooting;
        eventManager.SwitchedGameMode();
        
    }

    public void AddGold(int goldAmount)
    {
        gold += goldAmount;
        UpdateScore();
        sfxPlayer.PickupCoins();
        //if(gold > goldThreshold)
        //{
        //    Zombify();
        //}
    }

    public void DisplayText(Vector3 position, string text)
    {
        GameObject textHolder = Instantiate(textPrefab, position, Quaternion.identity);
        textHolder.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(text);
    }

    public void Zombify()
    {
        SwitchPlayer();
        eventManager.SwitchedGameMode();
    }

    public void DisplayDialogue(List<string> messages)
    {
        movementFactor = 0f;
        dialogue.DisplayDialogue(messages);
    }

    public void DisplayDialogue(string message)
    {
        movementFactor = 0f;
        dialogue.DisplayDialogue(new List<string> { message });
    }

    
}
