using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimScript : MonoBehaviour
{
    Animator anim;

    Quaternion lastRotation;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    void CancelAtackAnim()
    {

        anim.SetBool("isAttacking", false);
    }
    
    void SaveRotation()
    {
        lastRotation = transform.rotation;
        anim.SetBool("canAttack", false);
    }

    void LoadRotation()
    {
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
    }
    void CanAttack()
    {
        anim.SetBool("canAttack", true);
    }
}
