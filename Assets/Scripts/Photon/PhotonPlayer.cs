using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject myAvatar;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        int spawnpicker = Random.Range(0,GameSetup.GS.SpawnPoints.Length);
        if (PV.IsMine)
        {
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"), GameSetup.GS.SpawnPoints[spawnpicker].position, GameSetup.GS.SpawnPoints[spawnpicker].rotation, 0);
            myAvatar.GetComponent<PlayerMovement>().enabled = true;
            //myAvatar.GetComponent<ProjectileShooter>().enabled = true;
            myAvatar.transform.Find("Real Camera").gameObject.SetActive(true);
            myAvatar.transform.Find("Real Camera").gameObject.GetComponent<MouseLook>().enabled = true;
        }
    }

}
