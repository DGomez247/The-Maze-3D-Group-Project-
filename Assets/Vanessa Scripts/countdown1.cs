using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;
public class countdown1 : MonoBehaviour
{

    public Text timerText;
    public float time = 350;
    public float addTime = 30;
    public bool bonus = false;

    public Collider orange;
    

    void Start()
    {
        StartCoundownTimer();
    }

    void StartCoundownTimer()
    {
        if (timerText != null)
        {
            time = 350;
            timerText.text = "Time Left: 10:00:000";
            InvokeRepeating("UpdateTimer", 0.0f, 0.01667f);
;        }
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