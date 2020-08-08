using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThresholdLine : MonoBehaviour
{

    LineRenderer line;
    EdgeCollider2D edge;

    public bool hasPassed;

    Color green;
    Color red;

    Vector2[] points;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        edge = GetComponent<EdgeCollider2D>();
        points = new Vector2[2];
        green = new Color(0, 255, 0);
        red = new Color(255, 0, 0);
        hasPassed = false;
    }

    /// <summary>
    /// Resets threshold line to default values.
    /// </summary>
    /// <remarks>
    /// Called after coin has stopped moving.
    /// </remarks>
    /// <param name="point1">Position of the first static coin</param>
    /// <param name="point2">Position of the second static coin</param>
    public void ResetLine(Vector3 point1, Vector3 point2)
    {
        SetLine(point1, point2);
        line.startColor = red;
        line.endColor = red;
        hasPassed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            hasPassed = true;
            line.startColor = green;
            line.endColor = green;
        }
    }

    /// <summary>
    /// Updates threshold line position.
    /// </summary>
    /// <param name="point1">Position of the first static coin</param>
    /// <param name="point2">Position of the second static coin</param>
    public void SetLine(Vector3 point1, Vector3 point2)
    {
        line.SetPosition(0, point1);
        line.SetPosition(1, point2);
        points[0] = point1;
        points[1] = point2;
        edge.points = points;
    }
}
