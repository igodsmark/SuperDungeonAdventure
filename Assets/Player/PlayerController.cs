using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1;      
    [SerializeField] Animator animator;


    GameObject repairTarget;
    GameObject openTarget;

    Rigidbody rb;
    GameManager gameManager;
    MovementController moveController;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject start = GameObject.FindWithTag("StartPosition");
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
        rb.position = start.transform.position;
        moveController = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (gameManager.CanMove)
        {
            if(Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0)
            {

                animator.SetTrigger("RunTrigger");
                Vector3 movement = new Vector3 (horizontal, 0, vertical);
                moveController.MoveRB(rb, movement, movementSpeed);
                rb.velocity = Vector3.zero;
            }
            else
            {            
                animator.SetTrigger("IdleTrigger");
            }

        }
        else
        {
            animator.SetTrigger("IdleTrigger");
        }



        //Quaternion target = Quaternion.Euler(0, facing, 0);
        //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * turnSpeed);

        if (Input.GetButtonDown("Fire1"))
        {

            //gameManager.SwitchPlayer();
            if(repairTarget != null && !gameManager.IsLooting)
            {
                repairTarget.GetComponent<Repairable>().ToggleState();
            }
            if(openTarget != null && gameManager.IsLooting)
            {
                openTarget.GetComponent<Interact>().InteractWith();
            }


        }

        if (Input.GetKeyDown(KeyCode.T))
        {
           

            //gameManager.FadeOut();
            
            
            //manager.FadeIn();
        }

    }

    public void TeleportTo(Vector3 newPosition)
    {
        var camera = GameObject.FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        //GameObject teleportTarget = GameObject.FindWithTag("TeleportTarget");
        if (camera != null)
        {
            camera.OnTargetObjectWarped(transform, newPosition - transform.position);
        }
        rb.position = newPosition;
        gameManager.Teleported(newPosition);
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RepairTarget"))
        {
            repairTarget = other.gameObject;
        }

        if (other.gameObject.CompareTag("Openable"))
        {
            openTarget = other.gameObject;
        }

        if (other.gameObject.CompareTag("Pickup"))
        {

            other.GetComponent<Pickup>()?.PickUpItem();
        }

        //Interact interact = other.GetComponent<Interact>();
        //Debug.Log("Collided");
        //if(interact != null)
        //{            
        //    interact.InteractWith();
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("RepairTarget"))
        {
            repairTarget = null;
        }
        if (other.gameObject.CompareTag("Openable"))
        {
            openTarget = null;
        }
    }
}
