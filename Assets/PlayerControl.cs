using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GameObject playerCube;
    private Vector2 lastMousePos;
    private bool gameIsStart;
    private int count;
    [SerializeField] private float speed;

    private void Awake()
    {
        GetComponentInChildren<Renderer>().material.color = GetRandomColor();
        foreach (var heel in GameObject.FindGameObjectsWithTag("Heels"))
        {
            heel.GetComponentInChildren<Renderer>().material.color = GetComponentInChildren<Renderer>().material.color;
        }
    }

    void Update()
    {
        if (gameIsStart)
        {
            transform.position += Vector3.forward * Time.deltaTime * speed;
        }

        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
            gameIsStart = true;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 currentMousePos = Input.mousePosition;
            Vector2 delta = currentMousePos - lastMousePos;
            lastMousePos = currentMousePos;
            transform.position =
                new Vector3(Mathf.Clamp(transform.position.x + delta.x * Time.deltaTime, -1.9f, 1.9f), 0.0f,
                    transform.position.z);
        }

        if (Input.GetMouseButtonUp(0))
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Heels"))
        {
            if (heels.Contains(other.gameObject)) return;
            foreach (var heel in heels)
            {
                float height = heel.transform.position.y;
                height += other.transform.localScale.y;
                heel.transform.position = new Vector3(transform.position.x, height, transform.position.z);
            }

            other.transform.SetParent(transform);
            other.tag = "Untagged";
            other.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            heels.Add(other.gameObject);
        }

        if (other.CompareTag("obstacle"))
        {
            var item = heels[heels.Count - 1];
            item.transform.SetParent(null);
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Heels"))
        {
            var heelsCount = other.transform.childCount;
            for (int i = 0; i < heelsCount; i++)
            {
                foreach (Transform child in transform)
                {
                    float height = child.transform.position.y;
                    height += other.transform.localScale.y;
                    child.transform.position = new Vector3(transform.position.x, height, transform.position.z);
                }

                other.transform.GetChild(0).tag = "Untagged";
                other.transform.GetChild(0).position = new Vector3(transform.position.x, 0, transform.position.z);
                other.transform.GetChild(0).SetParent(transform);
            }
        }
    }
    private Color GetRandomColor()
    {
        
        return new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }
}