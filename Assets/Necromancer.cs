using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class Necromancer : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float walkSpeed = 0.5f;
    [SerializeField] float accelerator = 0.2f;
    [SerializeField] float personalSpace = 1f;
    [SerializeField] Animator animator;
    [SerializeField] Vector3 lastKnownDestination;
    [SerializeField] float waitTime = 10f;
    [SerializeField] GameObject model;
    [SerializeField] GameManager gameManager;
    [SerializeField] EventManager eventManager;

    bool isActive = false;
    bool chasing = false;
    float chaseTimer = 0f;
    MovementController moveController;
    bool displayedChase = false;
    bool hasSpawned = false;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveController = GetComponent<MovementController>();
        eventManager.SwitchGameMode += GameManager_SwitchGameMode;
    }

    private void GameManager_SwitchGameMode(object sender, System.EventArgs e)
    {
        
        gameManager.DisplayDialogue(new List<string>() { "Now...", "GET TIDYING!" });
        eventManager.FinishedText += EventManager_FinishedText;
        isActive = false;
        //gameObject.SetActive(false);
    }

    private void EventManager_FinishedText(object sender, System.EventArgs e)
    {
        eventManager.FinishedText -= EventManager_FinishedText;
        gameObject.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (chasing)
            {
                if(Vector3.Distance(transform.position, player.transform.position) > personalSpace)
                {
                    animator.SetTrigger("RunTrigger");
                    Vector3 playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);   
                    Vector3 direction = Vector3.Normalize(playerPos - transform.position);
                    moveController.MoveRB(rb, direction, walkSpeed);
                    //rb.rotation = Quaternion.LookRotation(direction);
                    //rb.MovePosition(transform.position + direction * Time.deltaTime * walkSpeed);
                }
                else
                {
                    animator.ResetTrigger("RunTrigger");
                    animator.SetTrigger("IdleTrigger");
                    chasing = false;
                    isActive = false;
                    gameManager.DisplayDialogue(new List<string>() { "I've had it with adventurers messing up my dungeon!", "So now I've got a job for you..." });
                    gameManager.TriggerPlayerSwitch();
                }

            }
            else
            {
                chaseTimer += Time.deltaTime;
                
                if (chaseTimer > waitTime)
                {
                    if (!displayedChase)
                    {
                        gameManager.DisplayDialogue(new List<string>() { "The Necromancer is after you!" });
                        displayedChase = true;
                        eventManager.StartChase();
                    }
                    transform.position = lastKnownDestination;
                    chasing = true;
                    chaseTimer = 0f;
                    model.SetActive(true);
                }
                //animator.SetTrigger("IdleTrigger");
            }

        }
        
    }

    public void SetLastKnown(Vector3 position)
    {
        model.SetActive(false);
        chaseTimer = 0f;
        isActive = true;
        //Stop chasing
        chasing = false;

        //Update position
        lastKnownDestination = position;

        //Start chasing after delay


        //Increase speed & reduce wait time
        if (displayedChase)
        {
            walkSpeed += accelerator;
            waitTime -= accelerator;
        }
    }



}
