using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public PickupSpawner pickupSpawner;


    private Rigidbody _rb;
    private int count;
    private int maxJumpCount;
    private int jumpCount;

    private float _movementX;
    private float _movementY;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        count = 0;
        jumpCount = 0;
        maxJumpCount = 2;
        
        SetCountText();
        
        winTextObject.SetActive(false);
    }
    
    void Update ()
    {
        if (Input.GetKeyDown ("space") && (jumpCount < maxJumpCount)) {
            Vector3 jump = new Vector3 (0.0f, 400.0f, 0.0f);

            GetComponent<Rigidbody>().AddForce(jump);
            jumpCount++;
        }
    }



    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        _movementX = movementVector.x;
        _movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(_movementX, 0.0f, _movementY);

        _rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            if (jumpCount > 0)
                jumpCount--;

            
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        Debug.Log("Count: " + count + ", Max: " + pickupSpawner.SpawnCount);
        if (count >= pickupSpawner.SpawnCount)
        {
            winTextObject.SetActive(true);
        }
    }
}
