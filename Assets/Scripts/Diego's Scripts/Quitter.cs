using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quitter : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("You have quit the game!");
        Application.Quit();
    }
}
