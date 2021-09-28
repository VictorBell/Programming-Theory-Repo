using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]  float speed = 10f;
    [SerializeField]  float rotationSpeed;
    float horizontalInput;
    float verticalInput;
    [SerializeField] float m_magnitude;

    private float magnitude
    {
        get { return m_magnitude; }
        set
        {
            if (value > 1)
            {
                m_magnitude = 1;
            }
            else
            {
                m_magnitude = value;
            }
        }
    }
    float speedWhileTurning;

    bool isTurning;
    
    Rigidbody playerRb;
    Animator anim;
    [SerializeField] GameObject player;

    Quaternion toRotate;

    // Start is called before the first frame update
    void Start()
    {
        anim = player.GetComponent<Animator>();
        playerRb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Attack();
        Move();
    }

    void Move()
    {
        //Change later to mobile controls
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        magnitude = movementDirection.magnitude;
        

        //Rotate while moving 
        if (movementDirection != Vector3.zero)
        {
            movementDirection.Normalize();
            toRotate = Quaternion.LookRotation(movementDirection, Vector3.up);
            player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
            if (toRotate != player.transform.rotation)
            {
                isTurning = true;
            }
            else isTurning = false;


            if (isTurning)
            {
                if (speedWhileTurning > 0.5)
                {
                    speedWhileTurning -= Time.deltaTime;
                }
            }
            else
            {
                if (speedWhileTurning < 1)
                {
                    speedWhileTurning += Time.deltaTime;
                }

            }
            playerRb.transform.Translate(movementDirection * speed * Time.deltaTime * magnitude * speedWhileTurning, Space.World);
        }
    }
    void Attack()
    {
        if (Input.GetKey(KeyCode.Space) && !anim.GetBool("isAttacking") && anim.GetBool("canAttack"))
        {
            anim.SetBool("isAttacking", true);
        }
    }

    
}
