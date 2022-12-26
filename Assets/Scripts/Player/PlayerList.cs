using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerList : MonoBehaviour
{
    [SerializeField] private Canvas playerList;
    [SerializeField] private GameObject playerPanel;

    [SerializeField] private KeyCode playerListKey = KeyCode.Tab;
    private Canvas plist;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(playerListKey)){
            plist = Instantiate(playerList, Vector3.zero, Quaternion.identity);
            foreach(Photon.Realtime.Player player in PhotonNetwork.PlayerList){
                GameObject ppanel = Instantiate(playerPanel, Vector3.zero, Quaternion.identity);
                ppanel.transform.SetParent(plist.transform.Find("Panel").transform.Find("ViewPort").transform.Find("Grid"), false);

                if(player.NickName.Length > 0) ppanel.GetComponentInChildren<Text>().text = player.NickName;
                else ppanel.GetComponentInChildren<Text>().text = "[NoName]";                
            }
        }

        if(Input.GetKeyUp(playerListKey)){
            Destroy(plist.gameObject);
        }
    }
}
