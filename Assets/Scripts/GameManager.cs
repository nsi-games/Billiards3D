using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score = 0;

    private Ball[] billiardBalls;

    // Use this for initialization
    void Start()
    {
        billiardBalls = GetComponentsInChildren<Ball>();
    }
    
    public bool AllBallsStoppedMoving()
    {
        for (int i = 0; i < billiardBalls.Length; i++)
        {
            Ball currentBall = billiardBalls[i];
            if (currentBall != null &&
               !currentBall.IsStopped())
            {
                return false;
            }
        }
        return true;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
}
