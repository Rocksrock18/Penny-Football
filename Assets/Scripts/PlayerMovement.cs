using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;

    Vector2 oldPos;
    Vector2 offset;

    Vector3 initCoin1Pos;
    Vector3 initCoin2Pos;
    Vector3 initCoin3Pos;

    Camera main;

    TravelDirection indicator;
    
    public bool stopped = true;
    bool isCoin2Next = true;

    public Transform coin2;
    public Transform coin3;

    public ThresholdLine threshold;

    public GoalLine goal;

    public float moveSpeed = 100.0f;

    Quaternion initRotation = new Quaternion(0, 0, 0, 1);

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        threshold.SetLine(coin2.position, coin3.position);
        main = Camera.main;
        initCoin1Pos = body.position;
        initCoin2Pos = coin2.position;
        initCoin3Pos = coin3.position;
        indicator = GetComponent<TravelDirection>();
    }

    void Update()
    {
        if (!stopped)
            return;

        if (Input.GetMouseButtonUp(0))
        {
            oldPos = body.position;
            InitializeOffset();
            Shoot();
            StartCoroutine(HandleCoinTravel());
        }
    }

    /// <summary>
    /// Determines the vector from the mouse to the coin.
    /// </summary>
    void InitializeOffset()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = main.ScreenToWorldPoint(screenPosition);
        offset = oldPos - worldPosition;
    }

    /// <summary>
    /// Shoots the coin in the designated direction based on the offset vector.
    /// </summary>
    private void Shoot()
    {
        indicator.HideIndicator();
        body.AddForce(offset*moveSpeed);
        stopped = false;
    }

    /// <summary>
    /// Handles all interactions while the coin is moving.
    /// </summary>
    /// <returns></returns>
    private IEnumerator HandleCoinTravel()
    {
        yield return new WaitForSeconds(.025f);
        while (body.velocity.magnitude > 0.001)
        {
            threshold.SetLine(coin2.position, coin3.position);
            yield return new WaitForSeconds(.025f);
        }
        stopped = true;
        if(WentThrough())
        {
            HandleNextTurn();
        }
        else
        {
            Debug.Log("Failed to cross threshold!");
            MasterReset();
        }
        yield break;
    }

    void HandleNextTurn()
    {
        if(goal.validGoal)
        {
            Debug.Log("GOAL!!!");
            MasterReset();
        }
        else if(goal.hasCrossed)
        {
            Debug.Log("Illegal goal!");
            MasterReset();
        }
        else
        {
            SwapCoin();
            indicator.ResetIndicator(transform.position);
        }
    }

    public void MasterReset()
    {
        ResetCoins();
        threshold.ResetLine(coin2.position, coin3.position);
        goal.ResetLine();
        indicator.ResetIndicator(body.position);
    }

    /// <summary>
    /// Determines if the coin passed through the threshold line.
    /// </summary>
    /// <returns></returns>
    private bool WentThrough()
    {
        return threshold.hasPassed;
    }

    /// <summary>
    /// Swaps position and location of 2 coins.
    /// </summary>
    /// <remarks>
    /// Gives the illusion of shooting different coins.
    /// </remarks>
    private void SwapCoin()
    {
        Vector3 coin1Pos = transform.position;
        Quaternion coin1Rot = transform.rotation;
        if(isCoin2Next)
        {
            transform.position = coin2.position;
            transform.rotation = coin2.rotation;
            coin2.position = coin1Pos;
            coin2.rotation = coin1Rot;
        }
        else
        {
            transform.position = coin3.position;
            transform.rotation = coin3.rotation;
            coin3.position = coin1Pos;
            coin3.rotation = coin1Rot;
        }
        isCoin2Next = !isCoin2Next;
        threshold.ResetLine(coin2.position, coin3.position);

    }

    /// <summary>
    /// Resets coins to their initial positions.
    /// </summary>
    public void ResetCoins()
    {
        body.position = initCoin1Pos;
        body.rotation = 0;
        coin2.position = initCoin2Pos;
        coin2.rotation = initRotation;
        coin3.position = initCoin3Pos;
        coin3.rotation = initRotation;
        isCoin2Next = true;
    }
}
