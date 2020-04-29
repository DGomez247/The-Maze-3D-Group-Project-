using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSwitch : MonoBehaviour
{
    public SceneSwitcher SS;

    public void Start()
    {
        SS = GameObject.Find("Win Transport").GetComponent<SceneSwitcher>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SS.ChangeScene(2);
        }
    }


}
