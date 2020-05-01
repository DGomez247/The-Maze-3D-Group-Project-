using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGMaze : MonoBehaviour
{

    public Transform[] spawnpoints;
    public GameObject Wall;


    void Start()
    {
        spawnWall();
    }

    public void spawnWall()
    {
        int spawnPoint = Random.Range(0, spawnpoints.Length);
        Instantiate(Wall, spawnpoints[spawnPoint].position, spawnpoints[spawnPoint].rotation);
    }
}
