using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    public static LobbyController Instance;
    public TextMeshProUGUI LobbyNameText;

    public GameObject PlayerListViewContect;
    public GameObject PlayerListItemPrefab;
    public GameObject LocalPlayerObject;

    public ulong CurrentLobbyID;
    public bool PlayerItemCreated = false;
    private List<PlayerListItem> playerListItems = new();
    public PlayerObjectContoller LocalPalyerController;

    private CustomNetworkManager manager;
    private CustomNetworkManager Manager
    {
        get
        {
            if (manager != null)
            {
                return manager;
            }
            return manager = CustomNetworkManager.singleton as CustomNetworkManager;
        }
    }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public void UpdateLobbyName()
    {
        CurrentLobbyID = Manager.GetComponent<SteamLobby>().CurrentLobbyID;
        LobbyNameText.text = SteamMatchmaking.GetLobbyData(new CSteamID(CurrentLobbyID), "name");
    }
    public void UpdatePlayerList()
    {
        if (!PlayerItemCreated) CreateHostPlayerItem();
        if (playerListItems.Count < manager.GamePlayers.Count) CreateClientPlayerItem();
        if (playerListItems.Count > manager.GamePlayers.Count) RemovePlayerItem();
        if (playerListItems.Count == Manager.GamePlayers.Count) UpdatePlayerItem();
    }

    public void FindLocalPlayer()
    {
        LocalPlayerObject = GameObject.Find("LocalGamePlayer");
        LocalPalyerController = LocalPlayerObject.GetComponent<PlayerObjectContoller>();
    }
    public void CreateHostPlayerItem()
    {
        foreach (PlayerObjectContoller player in Manager.GamePlayers)
        {
            GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab) as GameObject;
            PlayerListItem NewPlayerItemScript = NewPlayerItem.GetComponent<PlayerListItem>();

            NewPlayerItemScript.PlayerName = player.PlayerName;
            NewPlayerItemScript.ConnectionID = player.ConnectionID;
            NewPlayerItemScript.PlayerSteamID = player.PlayerSteamID;
            NewPlayerItemScript.SetPlayerValues();

            NewPlayerItem.transform.SetParent(PlayerListViewContect.transform);
            NewPlayerItem.transform.localScale = Vector3.one;

            playerListItems.Add(NewPlayerItemScript);
        }
        PlayerItemCreated = true;
    }
    public void CreateClientPlayerItem()
    {
        foreach (PlayerObjectContoller player in Manager.GamePlayers)
        {
            if (!playerListItems.Any(b => b.ConnectionID == player.ConnectionID))
            {
                GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab) as GameObject;
                PlayerListItem NewPlayerItemScript = NewPlayerItem.GetComponent<PlayerListItem>();

                NewPlayerItemScript.PlayerName = player.PlayerName;
                NewPlayerItemScript.ConnectionID = player.ConnectionID;
                NewPlayerItemScript.PlayerSteamID = player.PlayerSteamID;
                NewPlayerItemScript.SetPlayerValues();

                NewPlayerItem.transform.SetParent(PlayerListViewContect.transform);
                NewPlayerItem.transform.localScale = Vector3.one;

                playerListItems.Add(NewPlayerItemScript);
            }
        }
    }
    public void UpdatePlayerItem()
    {
        foreach (PlayerObjectContoller player in Manager.GamePlayers)
        {
            foreach (PlayerListItem playerListItemScript in playerListItems)
            {
                if (playerListItemScript.ConnectionID == player.ConnectionID)
                {
                    playerListItemScript.PlayerName = player.PlayerName;
                    playerListItemScript.SetPlayerValues();
                }
            }
        }
    }
    public void RemovePlayerItem()
    {
        List<PlayerListItem> playerListItemToRemove = new();

        foreach (PlayerListItem playerListItem in playerListItems)
        {
            if (!Manager.GamePlayers.Any(b => b.ConnectionID == playerListItem.ConnectionID))
            {
                playerListItemToRemove.Add(playerListItem);

            }
        }
        if (playerListItemToRemove.Count > 0)
        {
            foreach (PlayerListItem item in playerListItemToRemove)
            {
                GameObject ObjectToRemove = item.gameObject;
                playerListItems.Remove(item);
                Destroy(ObjectToRemove);
                ObjectToRemove = null;
            }
        }
    }
}
