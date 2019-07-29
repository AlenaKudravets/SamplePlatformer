using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceScore : MonoBehaviour {

    [SerializeField] private float actualPosition;
    [SerializeField] private float newPosition;
    [SerializeField] private int distanceScore = 1;

    void Start () {
        actualPosition = transform.position.x;
    }
	
	void Update () {
        newPosition = transform.position.x;

        if (newPosition - actualPosition >= 1)
        {
            PlatformerStats.AddScore(distanceScore);
            actualPosition = newPosition;
        }
    }
}
