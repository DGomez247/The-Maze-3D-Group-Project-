using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhotonRoomcustommatch : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static PhotonRoomcustommatch room;
    private PhotonView PV;

    public bool IsGameLoaded;
    public int currentscene;
    //public bool isGameLoaded;
    public int currentScene;
    public int multiplayScene;

    Player[] photonPlayers;
    public int playersInRoom;
    public int myNumberInRoom;
    public int playerInGame;

    private bool readyToCount;
    private bool readyToStart;
    public float startingTime;
    private float lessThanMaxPlayers;
    private float atMaxPlayer;
    private float timeToStart;


    public GameObject playerListingPrefab;
    public GameObject lobbyGO;
    public GameObject roomGO;
    public Transform playersPanel;
    public GameObject startbutton;

    public void Awake()
    {
        if(PhotonRoomcustommatch.room == null)
        {
            PhotonRoomcustommatch.room = this;
        }
        else
        {
            if(PhotonRoomcustommatch.room != this)
            {
                Destroy(PhotonRoomcustommatch.room.gameObject);
                PhotonRoomcustommatch.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        PV = GetComponent<PhotonView>();
        readyToCount = false;
        readyToStart = false;
        lessThanMaxPlayers = startingTime;
        timeToStart = startingTime;
    }

    void Update()
    {
        if (MultiPlayerSetting.multiplayerSetting.delaystart)
        {
            if(playersInRoom == 1)
            {
                RestartTimer();
            }
            if (!IsGameLoaded)
            {
                if (readyToStart)
                {
                    atMaxPlayer -= Time.deltaTime;
                    lessThanMaxPlayers = atMaxPlayer;
                    timeToStart = atMaxPlayer;
                }else if (readyToCount)
                {
                    lessThanMaxPlayers -= Time.deltaTime;
                    timeToStart = lessThanMaxPlayers;
                }
                if(timeToStart <= 0)
                {

                    StartGame();
                }

            }
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("We are now in a room");

        lobbyGO.SetActive(false);
        roomGO.SetActive(true);
        if (PhotonNetwork.IsMasterClient)
        {
            startbutton.SetActive(true);
        }

        ClearPlayerListings();
        ListPlayers();



        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
       
        if (MultiPlayerSetting.multiplayerSetting.delaystart)
        {
            Debug.Log(playersInRoom + " : " + MultiPlayerSetting.multiplayerSetting.maxplayers );

            if(playersInRoom > 1)
            {
                readyToCount = true;
            }
            if(playersInRoom == MultiPlayerSetting.multiplayerSetting.maxplayers)
            {
                readyToStart = true;
                if (PhotonNetwork.IsMasterClient)
                {
                    return;
                   // startbutton.SetActive(true);
                }
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
        //else
        //{
        //    StartGame();
        //}

        //StartGame();

    }
    void ClearPlayerListings()
    {
        for(int i = playersPanel.childCount - 1; i >= 0; i--)
        {
            Destroy(playersPanel.GetChild(i).gameObject);
        }
    }

    void ListPlayers()
    {
        if (PhotonNetwork.InRoom)
        {
            foreach(Player player in PhotonNetwork.PlayerList)
            {
                GameObject templisting = Instantiate(playerListingPrefab, playersPanel);
                Text temptext = templisting.transform.GetChild(0).GetComponent<Text>();
                temptext.text = player.NickName;
            }
        }
    }

    public void StartGame()
    {
        IsGameLoaded = true;
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        if (MultiPlayerSetting.multiplayerSetting.delaystart)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
        PhotonNetwork.LoadLevel(MultiPlayerSetting.multiplayerSetting.multiplayerScene);
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if(currentScene == MultiPlayerSetting.multiplayerSetting.multiplayerScene)
        {
            IsGameLoaded = true;
            if (MultiPlayerSetting.multiplayerSetting.delaystart)
            {
                PhotonNetwork.AutomaticallySyncScene = true;
                PV.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
            }
            else {
                RPC_CreatePlayer();
            }

        }
    }

    [PunRPC]
    private void RPC_LoadedGameScene() {
        playerInGame++;
        if(playerInGame == PhotonNetwork.PlayerList.Length)
        {
            PV.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        Vector3 pos = transform.position;
        pos.x = 7.42788f;
        pos.y = 5.887448f;
        pos.z = 2.378031f;
        transform.position = pos;
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), transform.position, Quaternion.identity, 0);
    }  
   



    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log(otherPlayer.NickName + "has left the game");
        playersInRoom--;
        ClearPlayerListings();
        ListPlayers();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        ClearPlayerListings();
        ListPlayers();
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom++;
        if (MultiPlayerSetting.multiplayerSetting.delaystart)
        {
            if(playersInRoom > 1)
            {
                readyToCount = true;
            }
            if(playersInRoom == MultiPlayerSetting.multiplayerSetting.maxplayers)
            {
                readyToStart = true;
                if (!PhotonNetwork.IsMasterClient)
                {
                    return;
                }
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
    }
    void RestartTimer()
    {
        lessThanMaxPlayers = startingTime;
        timeToStart = startingTime;
        atMaxPlayer = 4;
        readyToCount = false;
        readyToStart = false;
    }


}
