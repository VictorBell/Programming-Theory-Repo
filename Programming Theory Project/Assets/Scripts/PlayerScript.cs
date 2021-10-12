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
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
        Attack();

        }

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        //Change later to mobile controls
        
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
            playerRb.MovePosition(player.transform.position + (movementDirection * speed * Time.deltaTime * magnitude * speedWhileTurning));
        }
    }
    void Attack()
    {
        if (!anim.GetBool("isAttacking") && anim.GetBool("canAttack"))
        {
            anim.SetBool("isAttacking", true);
        }
    }

    
}
