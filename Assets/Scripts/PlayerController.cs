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
    public float jumpStrength = 0;


    private Rigidbody _rb;
    private int _count;
    private int _remainingJumps;

    private float _movementX;
    private float _movementY;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _count = 0;
        _remainingJumps = 2;
        
        SetCountText();
        
        winTextObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _remainingJumps > 0)
        {
            _rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
            _remainingJumps--;
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
            _count += 1;

            SetCountText();
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            _remainingJumps = 2;
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + _count.ToString();
        Debug.Log("Count: " + _count + ", Max: " + pickupSpawner.spawnCount);
        if (_count >= pickupSpawner.spawnCount)
        {
            winTextObject.SetActive(true);
        }
    }
}
