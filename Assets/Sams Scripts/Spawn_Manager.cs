using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{

    public Transform[] spawnpoints;
    public GameObject Car_Part;


    void Start()
    {
        spawncarpart();
    }

public void spawncarpart()
    {
        int spawnPI = Random.Range(0, spawnpoints.Length);
        Instantiate(Car_Part, spawnpoints[spawnPI].position, Quaternion.identity);
    }
}
