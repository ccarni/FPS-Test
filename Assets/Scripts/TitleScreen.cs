using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class TitleScreen : MonoBehaviourPunCallbacks
{

    [SerializeField] private Vector3[] cameraPositions;
    [SerializeField] private Vector3[] cameraRotations;
    [SerializeField] private Camera cam;

    [SerializeField] private Text chooseNameInput;
    [SerializeField] private Text createRoomInput;

    [SerializeField] private Canvas chooseName, mainCanvas, lobbyCanvas;

    [SerializeField] private RoomPanel roomPanelPrefab;
    [SerializeField] private Transform content;
    private List<RoomPanel> roomPanelsList = new List<RoomPanel>();

    // Start is called before the first frame update
    void Start()
    {
        mainCanvas.enabled = true;
        chooseName.enabled = false;
        lobbyCanvas.enabled = false;

        int index;
        if(cameraPositions.Length == cameraRotations.Length)
            index = Random.Range(0, cameraPositions.Length);
        else
            index = 0;
        cam.GetComponent<Transform>().position = cameraPositions[index];
        cam.GetComponent<Transform>().rotation = Quaternion.Euler(cameraRotations[index]);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        lobbyCanvas.enabled = true;
        mainCanvas.enabled = false;
        chooseName.enabled = false;
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("WaitingRoom");
    }

    public override void OnCreatedRoom()
    {
        lobbyCanvas.enabled = false;
        PhotonNetwork.LoadLevel("WaitingRoom");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateRoomList(roomList);
    }

    private void UpdateRoomList(List<RoomInfo> list)
    {
        //clear old panels
        foreach(RoomPanel panel in roomPanelsList){
            Destroy(panel.gameObject);
        }
        roomPanelsList.Clear();

        //create new panels
        foreach(RoomInfo room in list){
            RoomPanel newPanel = Instantiate(roomPanelPrefab, content);
            newPanel.SetRoomName(room.Name);
            roomPanelsList.Add(newPanel);
        }
    }

    public void ConnectButton()
    {
        if (chooseNameInput.text.Length >= 1){
            PhotonNetwork.NickName = chooseNameInput.text;
            PhotonNetwork.ConnectUsingSettings();
            chooseName.enabled = false;
        }
    }

    public void MultiplayerButton()
    {
        mainCanvas.enabled = false;
        chooseName.enabled = true;
        lobbyCanvas.enabled = false;
    }

    public void CreateRoom()
    {
        if(createRoomInput.text.Length >= 1){
            PhotonNetwork.CreateRoom(createRoomInput.text);
        }
    }

    public void JoinRoom(string RoomName)
    {
        PhotonNetwork.JoinRoom(RoomName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
