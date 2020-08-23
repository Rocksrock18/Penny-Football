using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayDisplayManager : MonoBehaviour
{

    public Text player1Score;
    public Text player2Score;
    public Text playerTurn;

    int p1Score = 0;
    int p2Score = 0;
    bool isPlayer1Turn = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleTurnChange(bool wasP1Turn, bool scored)
    {
        if(scored)
        {
            AddGoal(wasP1Turn);
        }
        SwapTurns();
    }

    void SwapTurns()
    {
        isPlayer1Turn = !isPlayer1Turn;
        if(isPlayer1Turn)
        {
            playerTurn.text = "Player 1's turn";
        }
        else
        {
            playerTurn.text = "Player 2's turn";
        }
    }

    void AddGoal(bool isPlayer1)
    {
        if(isPlayer1)
        {
            player1Score.text = "Player 1: " + ++p1Score;
        }
        else
        {
            player2Score.text = "Player 2: " + ++p2Score;
        }
    }
}
