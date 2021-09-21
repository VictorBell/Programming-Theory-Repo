using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = (player.transform.position + offset);
    }
}
