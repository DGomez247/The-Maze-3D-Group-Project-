using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

using UnityEngine.UI;

public class RoomButton : MonoBehaviour
{

    public Text nameText;
    public Text sizetext;

    public string roomname;
    public int roomsize;

    public void SetRoom()
    {
        nameText.text = roomname;
        sizetext.text = roomsize.ToString();
    }

    public void JoinRoomOnClick()
    {
        PhotonNetwork.JoinRoom(roomname);
    }
}
