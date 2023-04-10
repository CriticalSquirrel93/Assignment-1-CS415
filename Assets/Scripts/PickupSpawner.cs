using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{

    public GameObject pickUp;
    
    public int SpawnCount;
    public double radius;

    private float x;
    private float y;
    
    private float _angle;

    // Start is called before the first frame update
    void Start()
    {
        _angle = 0;
        // Divide 360 by SpawnCount, Calculate position based on degrees
        for (int i = 0; i < SpawnCount; i++)
        {
            x = Convert.ToSingle(radius * Math.Sin(Math.PI * 2 * _angle / 360));
            y = Convert.ToSingle(radius * Math.Cos(Math.PI * 2 * _angle / 360));
            Instantiate(pickUp, new Vector3(x, 1, y), Quaternion.identity);
            _angle += 360 / SpawnCount;
        }
    }
}
