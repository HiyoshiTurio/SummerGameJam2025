using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool isPlayer1 = true; 

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h, v;

        if (isPlayer1)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }
        else
        {
            h = Input.GetAxisRaw("Horizontal2");
            v = Input.GetAxisRaw("Vertical2");
        }
        

        Vector3 move = new Vector3(h, 0f, v).normalized * moveSpeed;

        if (move != Vector3.zero)
        {
            
            transform.forward = move;
        }
        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);

       
    }
}