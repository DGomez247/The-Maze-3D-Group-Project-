using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeBonus : MonoBehaviour
{



    public GameObject objWithScript;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        objWithScript.GetComponent<countdown1>().time += 30;
        Destroy(gameObject);
    }
}
