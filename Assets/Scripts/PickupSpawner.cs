using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{

    public GameObject pickUp;
    
    public int spawnCount;
    public double radius;

    private float _x;
    private float _y;
    
    private float _angle;

    // Start is called before the first frame update
    void Start()
    {
        _angle = 0;
        // Divide 360 by SpawnCount, Calculate position based on degrees
        for (int i = 0; i < spawnCount; i++)
        {
            _x = Convert.ToSingle(radius * Math.Sin(Math.PI * 2 * _angle / 360));
            _y = Convert.ToSingle(radius * Math.Cos(Math.PI * 2 * _angle / 360));
            Instantiate(pickUp, new Vector3(_x, 1, _y), Quaternion.identity);
            _angle += 360 / spawnCount;
        }
    }
}
