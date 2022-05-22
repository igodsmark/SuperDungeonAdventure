using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1;
    [SerializeField] float turnSpeed = 5;

    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject start = GameObject.FindWithTag("StartPosition");
        transform.position = start.transform.position;
        animator.SetTrigger("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (horizontal, 0, vertical) * Time.deltaTime * movementSpeed;

        transform.position = transform.position + movement;
        float facing = 0f;
        
        if(horizontal < 0)
        {
            facing = 180f;
        }
        else
        {
            facing = 0f;
        }
        Debug.Log(facing);
        Quaternion target = Quaternion.Euler(0, facing, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * turnSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.Play("Attack");

        }

    }
}
