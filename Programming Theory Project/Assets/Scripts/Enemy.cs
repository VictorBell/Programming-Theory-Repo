using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]GameObject enemy;
    GameObject player;

    bool chasingForPlayer = false;

    [SerializeField]float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      if (chasingForPlayer)
        {

            transform.LookAt (new Vector3 (player.transform.position.x, transform.position.y, player.transform.position.z));
            Vector3 lookDirection = (player.transform.position - enemy.transform.position).normalized;
            transform.Translate(lookDirection * speed * Time.deltaTime);
        }   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            chasingForPlayer = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            chasingForPlayer = true;
        }
    }
}
