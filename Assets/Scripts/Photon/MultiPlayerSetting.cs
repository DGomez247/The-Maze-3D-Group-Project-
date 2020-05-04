using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerSetting : MonoBehaviour
{
    public static MultiPlayerSetting multiplayerSetting;

    public bool delaystart;
    public int maxplayers;

    public int menuscene;
    public int multiplayerScene;

    public void Awake()
    {
        if(MultiPlayerSetting.multiplayerSetting == null)
        {
            MultiPlayerSetting.multiplayerSetting = this;
        }
        else
        {
            if(MultiPlayerSetting.multiplayerSetting != this)
            {
                Destroy(this.gameObject);
            }

        }
        DontDestroyOnLoad(this.gameObject);
    }
}
