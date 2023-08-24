using Mirror;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyDataEntry : MonoBehaviour
{
    public CSteamID lobbyID;
    public string lobbyName;
    public TextMeshProUGUI lobbyNameText;

    public void SetLobbyData()
    {
        if (lobbyName == "")
        {
            lobbyNameText.text = "Empty";

        }
        else
        {
            lobbyNameText.text = lobbyName;

        }
    }
    public void JoinLobby()
    {
        SteamLobby.Instance.JoinLobby(lobbyID);
    }

}
