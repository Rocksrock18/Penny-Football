using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TravelDirection : MonoBehaviour
{
    public GameObject indicator;
    PlayerMovement pm;
    Rigidbody2D body;
    LineRenderer indicatorLine;
    Camera main;
    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        body = GetComponent<Rigidbody2D>();
        indicatorLine = indicator.GetComponent<LineRenderer>();
        main = Camera.main;
        indicatorLine.SetPosition(0, body.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pm.stopped)
            return;

        if(Input.GetMouseButton(0))
        {
            UpdateDirectionIndicator();
        }
    }

    void UpdateDirectionIndicator()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = main.ScreenToWorldPoint(screenPosition);
        Vector2 nextPoint = 2*body.position - worldPosition;
        indicatorLine.SetPosition(1, nextPoint);
    }

    public void HideIndicator()
    {
        indicatorLine.enabled = false;
    }

    public void ResetIndicator(Vector3 newPos)
    {
        indicatorLine.enabled = true;
        indicatorLine.SetPosition(0, newPos);
        indicatorLine.SetPosition(1, newPos);
    }
}
