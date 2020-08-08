using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCoin : MonoBehaviour
{

    public Transform player;

    public float leftBound;
    public float rightBound;
    public float upperBound;
    public float lowerBound;

    float x;
    float y;

    // Update is called once per frame
    void Update()
    {
        x = player.position.x;
        y = player.position.y;
        x = x < leftBound ? leftBound : x;
        x = x > rightBound ? rightBound : x;
        y = y < lowerBound ? lowerBound : y;
        y = y > upperBound ? upperBound : y;
        transform.position = new Vector3(x, y, -10);
    }
}
