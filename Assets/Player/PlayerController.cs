using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1;      
    [SerializeField] Animator animator;


    GameObject repairTarget;
    GameObject openTarget;

    Rigidbody rb;
    GameManager gameManager;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject start = GameObject.FindWithTag("StartPosition");
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
        rb.position = start.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0)
        {
            animator.SetTrigger("RunTrigger");
            Vector3 movement = new Vector3 (horizontal, 0, vertical) * Time.deltaTime * movementSpeed;
            rb.rotation = Quaternion.LookRotation(movement);
            rb.MovePosition(transform.position + movement);
            rb.velocity = Vector3.zero;
        }
        else
        {
            Debug.Log("Idle");
            animator.SetTrigger("IdleTrigger");
        }



        //Quaternion target = Quaternion.Euler(0, facing, 0);
        //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * turnSpeed);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
        {

            //gameManager.SwitchPlayer();
            if(repairTarget != null)
            {
                repairTarget.GetComponent<Repairable>().ToggleState();
            }
            if(openTarget != null)
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
