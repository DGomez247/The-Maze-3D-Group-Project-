using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;
public class countdown : MonoBehaviour
{
  
    public Text timerText;
    private float time = 1200;

    void Start()
    {
        StartCoundownTimer();
    }

    void StartCoundownTimer()
    {
        if (timerText != null)
        {
            time = 1200;
            timerText.text = "Time Left: 10:00:000";
            InvokeRepeating("UpdateTimer", 0.0f, 0.01667f);
        }
    }


    void UpdateTimer()
    {
        if (timerText != null)
        {
            time -= Time.deltaTime;
            string minutes = Mathf.Floor(time / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            string fraction = ((time * 100) % 100).ToString("000");
            timerText.text = "Time Left: " + minutes + ":" + seconds + ":" + fraction;


             
            if (time < 0)
            {
                SceneManager.LoadScene(1);
            }
        }
       
    }
}