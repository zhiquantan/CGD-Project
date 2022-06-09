using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;
using System.IO;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;

    public GameObject playerNumberNoticeText;
    //public GameObject CharacterSelectionUISpawn;
    public static GameObject RoomUIRefer;
    public GameObject RoomUI;
    //public GameObject CharacterSelectionUI;
    //public GameObject CharacterSelectionUI1;
    public bool connected = false;
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] Transform playerListContent;

    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] GameObject startGameButton;

    // Start is called before the first frame update

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (!connected)
        {
            Debug.Log("Connecting to Master");
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("haha");
            MenuManager.Instance.OpenMenu("title");
        }

        RoomUIRefer = RoomUI;

    }

    public override void OnConnectedToMaster()
    {

        if (!connected)
        {
            Debug.Log("Connected to Master");
            PhotonNetwork.JoinLobby();
            PhotonNetwork.AutomaticallySyncScene = true;
        }
    }

    public override void OnJoinedLobby()
    {

        if (!connected)
        {
            MenuManager.Instance.OpenMenu("title");
            Debug.Log("Joined lobby");
            PhotonNetwork.NickName = PlayerPrefs.GetString("username");
            connected = true;
        }
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
            //PhotonNetwork.Instantiate(Path.Combine("ObjectPrefabs", "PlayerListItem"), playerListContent.position, playerListContent.rotation).GetComponent<PlayerListItem>().SetUp(players[i]);
            //PlayerList = GameObject.FindGameObjectsWithTag("PlayList")[i];
            //PlayerList.transform.parent = playerListContent;

        }

        startGameButton.SetActive(PhotonNetwork.IsMasterClient);

    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed" + message;
        MenuManager.Instance.OpenMenu("error");
    }

    public void StartGame()
    {
        if (playerListContent.childCount > 3)
        {
            StartCoroutine(PlayerNumberNoticeText());
        }
        else
        {
            PhotonNetwork.LoadLevel(2);
        }

    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");
    }
    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }
    public void GoCharacterSelectionUI()
    {
        SpawnCharacterUI.CharacterSelectionUI.SetActive(true);
        RoomUI.SetActive(false);
        //CharacterSelectionUI.SetActive(true);
        //CharacterSelectionUI1.SetActive(true);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    IEnumerator PlayerNumberNoticeText()
    {
        playerNumberNoticeText.SetActive(true);
        yield return new WaitForSeconds(3);
        playerNumberNoticeText.SetActive(false);

    }


}
