using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool istouched;

    private void OnCollisionEnter(Collision other)
    {
        if (istouched) return;
        istouched = true;
        other.transform.SetParent(null);
    }
}