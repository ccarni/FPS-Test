using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : MonoBehaviour
{
    private TitleScreen titleScreen;
    [SerializeField] private Text roomName;
    
    private void Start() 
    {
        titleScreen = FindObjectOfType<TitleScreen>();
    }

    public void SetRoomName(string name)
    {
        roomName.text = name;
    }

    public void OnClickItem()
    {
        titleScreen.JoinRoom(roomName.text);
    }
}
