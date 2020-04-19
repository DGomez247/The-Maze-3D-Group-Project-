using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMeu : MonoBehaviour
{
    static bool isgamepaused = false;
    public GameObject pausemenu;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pausemenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isgamepaused)
            {
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    public void Resume()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
        isgamepaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Pause() 
    {
        pausemenu.SetActive(true);
        Time.timeScale = 0f;
        isgamepaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

}
