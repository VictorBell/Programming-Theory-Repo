using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float rotationSpeed;
    float horizontalInput;
    float verticalInput;
    [SerializeField]float whenTurning;

    bool isTurning;
    
    Rigidbody playerRb;
    Animator anim;

    Quaternion toRotate;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Attack();
        
    }
    void Move()
    {
        //Change later
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = movementDirection.magnitude;
        movementDirection.Normalize();
        
        //Rotate while moving 
        if (movementDirection != Vector3.zero)
        {
            toRotate = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
            if (toRotate != transform.rotation)
            {
                isTurning = true;
            }
            else isTurning = false;
        }

        if(isTurning) {
            if (whenTurning > 0)
            {
            whenTurning -= 0.1f;
            }
        }
        else
        {
            if(whenTurning < 1)
            {
                whenTurning += 0.1f;
            }
            
        }
        playerRb.transform.Translate(movementDirection * speed * Time.deltaTime * magnitude * whenTurning, Space.World);
    }

    void Attack()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("isAttacking", true);
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    }
}
