using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobbyCustomMatch : MonoBehaviourPunCallbacks, ILobbyCallbacks
{

    public static PhotonLobbyCustomMatch lobby;

    public string roomname;
    public int roomsize;
    public GameObject roomlistingprefab;
    public Transform roomspanel;

    public void Awake()
    {
        lobby = this;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player connected to the photon master server");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        RemoveRoomListings();
        foreach(RoomInfo room in roomList)
        {
            ListRoom(room);
        }

    }
    void RemoveRoomListings()
    {
        while(roomspanel.childCount != 0)
        {
            Destroy(roomspanel.GetChild(0).gameObject);
        }
    }

    void ListRoom(RoomInfo room)
    {
        if(room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomlistingprefab, roomspanel);
            RoomButton tempbutton = tempListing.GetComponent<RoomButton>();
            tempbutton.roomname = room.Name;
            tempbutton.roomsize = room.MaxPlayers;
            tempbutton.SetRoom();
        }
    }
    private void OnJoinedLobby()
    {
        print("Joined Lobby");
    }


    public void CreateRoom()
    {
        Debug.Log("Trying to Create Room");
        RoomOptions roomops = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte) roomsize };
        PhotonNetwork.CreateRoom(roomname, roomops);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Could Not Create Room");
    }

    public void OnRoomNameChanged(string nameIn)
    {
        roomname = nameIn;
    }

    public void OnRoomSizeChanged(string sizeIn)
    {
        roomsize = int.Parse(sizeIn);
    }

    public void JoinLobbyOnClick()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }




}
