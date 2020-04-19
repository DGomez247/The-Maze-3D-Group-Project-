using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{

    public Transform[] spawnpoints;
    public GameObject Banana;
    // Start is called before the first frame update
    void Start()
    {
        spawnbanana();
    }

public void spawnbanana()
    {
        int spawnPI = Random.Range(0, spawnpoints.Length);
        Instantiate(Banana, spawnpoints[spawnPI].position, Quaternion.identity);
        Debug.Log("Spawned a bannana at" + spawnpoints[spawnPI]);
    }
}
