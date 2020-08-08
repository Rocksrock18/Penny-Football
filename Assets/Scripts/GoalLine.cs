using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLine : MonoBehaviour
{
    LineRenderer line;
    Color green;
    Color red;

    public GameObject player;

    public bool validGoal;
    public bool hasCrossed;

    public ThresholdLine threshold;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        green = new Color(0, 255, 0);
        red = new Color(255, 0, 0);
        validGoal = false;
    }

    //FIX GOAL BUGS: On player enter, valid or invalid, game will reset and break. On SC enter, game freezes until pause menu brought up.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hasCrossed = true;
        if (collision.tag.Equals("Player"))
        {
            Debug.Log("Player has crossed the goal line!");
            line.startColor = green;
            line.endColor = green;
            validGoal = threshold.hasPassed;
            if(validGoal)
            {
                StartCoroutine(PlayGoalAnimation());
            }
        }
    }

    private IEnumerator PlayGoalAnimation()
    {
        Time.timeScale = .5f;
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 1f;
        yield break;
    }

    public void ResetLine()
    {
        validGoal = false;
        hasCrossed = false;
        line.startColor = red;
        line.endColor = red;
    }
}
