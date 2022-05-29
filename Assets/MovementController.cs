using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    public void MoveRB(Rigidbody rb, Vector3 direction, float speed)
    {
        rb.rotation = Quaternion.LookRotation(direction);
        rb.MovePosition(transform.position + direction * Time.deltaTime * speed * gameManager.MovementFactor);
    }
}
