using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundarie : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(
                transform.position.x, -1.9f, 1.9f), 
            transform.position.y,
            transform.position.z);
    }
}
