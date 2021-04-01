using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float speed = 5;
    [SerializeField] private Transform leftFoot;
    [SerializeField] private Transform rightFoot;
    [SerializeField] private GameObject heels;
    private Vector2 lastMousePos;
    private Rigidbody rb;
    private bool gameIsStart, obstacle;
    private int sayac;
    private float timer;
    private float height;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        heels.transform.position = new Vector3(0, 0.5f, 0);
        timer = 0;
    }

    void Update()
    {
        if (obstacle)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - height,
                    transform.position.z);
                obstacle = false;
                timer = 0;
            }
        }

        if (gameIsStart)
        {
            rb.velocity = Vector3.forward * (Time.deltaTime * speed);
            anim.SetTrigger("Walk");
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
            rb.velocity = new Vector3(delta.x, 0.0f, rb.velocity.z);
        }

        if (Input.GetMouseButtonUp(0))
        {
            rb.velocity = new Vector3(0, 0, rb.velocity.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Heels"))
        {
            Destroy(other.gameObject);
            heels.transform.position = new Vector3(heels.transform.position.x, heels.transform.position.y - 1,
                heels.transform.position.z);
            Instantiate(heels, leftFoot);
            Instantiate(heels, rightFoot);
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            sayac++;
            sayac++;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("obstacle") && obstacle == false)
        {
            obstacle = true;
            height = other.gameObject.transform.localScale.y;
            for (int i = 0; i < height; i++)
            {
                leftFoot.GetChild(sayac / 2 - 1).gameObject.AddComponent<Rigidbody>();
                leftFoot.GetChild(sayac / 2 - 1).SetParent(null);
                rightFoot.GetChild(sayac / 2 - 1).gameObject.AddComponent<Rigidbody>();
                rightFoot.GetChild(sayac / 2 - 1).SetParent(null);
                sayac--;
                sayac--;
            }
        }
    }
}