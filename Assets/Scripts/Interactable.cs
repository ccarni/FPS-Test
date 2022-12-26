using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public void Interact(GameObject player)
    {
        GameSetup gs = FindObjectOfType<GameSetup>();
        if(player.GetComponent<PlayerHealth>().myTeam == 1){
            gs.TeamWon(1);
        } else {
            gs.TeamWon(2);
        }
    }
}
