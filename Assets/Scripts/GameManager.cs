using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Cue cueStick;
    public int score = 0;

    private Ball[] billiardBalls;
    private bool isActive = false;

    // Use this for initialization
    void Start()
    {
        billiardBalls = GetComponentsInChildren<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AllBallsStoppedMoving() &&
           !cueStick.gameObject.activeSelf)
        {
            cueStick.Activate();
        }
    }

    bool AllBallsStoppedMoving()
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
