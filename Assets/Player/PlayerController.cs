using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject start = GameObject.FindWithTag("StartPosition");
        transform.position = start.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (horizontal, 0, vertical) * Time.deltaTime * movementSpeed;

        transform.position = transform.position + movement;
        if(horizontal < 0)
        {
            Quaternion target = Quaternion.Euler(0, horizontal * 180f, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5f);
        }
    }
}
