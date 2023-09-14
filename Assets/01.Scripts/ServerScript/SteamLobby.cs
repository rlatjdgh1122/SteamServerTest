using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.UI;
using TMPro;
using System;
public class SteamLobby
{
    public static SteamLobby Instance;
    public List<CSteamID> lobbyIDs = new();

    public ulong CurrentLobbyID;
    private const string HostAddressKey = "HostAddress";
    private CustomNetworkManager _manager;

    public SteamLobby(CustomNetworkManager manager)
    {

        _manager = manager;


        Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
        Callback<LobbyEnter_t>.Create(OnLobbyEntered);

        Callback<LobbyMatchList_t>.Create(OnGetLobbyList);
        Callback<LobbyDataUpdate_t>.Create(OnGetLobbyData);
    }
    public void HostLobby()
    {
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePrivate, _manager.maxConnections);
    }
    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK) { return; }
        Debug.Log("Lobby created Successfully");

        _manager.StartHost();

        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey, SteamUser.GetSteamID().ToString());

        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "name", SteamFriends.GetPersonaName().ToString() + "`s LOBBY");
    }
    private void OnJoinRequest(GameLobbyJoinRequested_t callback)
    {
        Debug.Log("Reque To Join Lobby");
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }
    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        CurrentLobbyID = callback.m_ulSteamIDLobby;

        if (NetworkServer.active) return;
        _manager.networkAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);

        _manager.StartClient();
    }

    public void JoinLobby(CSteamID lobbyID)
    {
        SteamMatchmaking.JoinLobby(lobbyID);
    }

    public void GetLobbiesList()
    {
        if (lobbyIDs.Count > 0) { lobbyIDs.Clear(); }

        SteamMatchmaking.AddRequestLobbyListResultCountFilter(60);
        SteamMatchmaking.RequestLobbyList();
    }

    private void OnGetLobbyList(LobbyMatchList_t result)
    {
        /*if (LobbiesListManager.Instance.listOfLobbies.Count > 0)*/
        for (int i = 0; i < result.m_nLobbiesMatching; i++)
        {
            CSteamID lobbyID = SteamMatchmaking.GetLobbyByIndex(i);
            lobbyIDs.Add(lobbyID);
            SteamMatchmaking.RequestLobbyData(lobbyID);
        }
    }
    private void OnGetLobbyData(LobbyDataUpdate_t result)
    {
        LobbyUIManager.Instance.DisaplayLobbies(lobbyIDs, result);
    }
}
