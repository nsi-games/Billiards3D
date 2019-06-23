using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rack : MonoBehaviour
{
    public GameObject ballPrefab;
    private Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBalls();
    }

    void SpawnBalls()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
        for (int i = 1; i < spawnPoints.Length; i++)
        {
            Transform point = spawnPoints[i];
            GameObject clone = Instantiate(ballPrefab, point.position, point.rotation);
            Ball ball = clone.GetComponent<Ball>();
            ball.SetMaterial(i);
        }
    }
}
