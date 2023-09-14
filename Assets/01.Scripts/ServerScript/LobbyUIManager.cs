using Mirror;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LobbyUIManager
{
    public static LobbyUIManager Instance;

    private VisualTreeAsset _lobbyTemplate;
    private VisualElement _root;
    private VisualElement _popupPanel;
    private ScrollView _lobbyContainer;
    public LobbyUIManager(VisualTreeAsset lobbyTemplate, VisualElement root, VisualElement popupPanel)
    {
        _root = root;
        _lobbyTemplate = lobbyTemplate;
        _popupPanel = popupPanel;

        _lobbyContainer = root.Q<ScrollView>("lobby-container");

        root.Q<Button>("btn-cancel").RegisterCallback<ClickEvent>(OnLobbyCanel);
        root.Q<Button>("btn-refresh").RegisterCallback<ClickEvent>(OnLobbyRefresh);

        SteamLobby.Instance.GetLobbiesList();
    }

    private void OnLobbyRefresh(ClickEvent evt)
    {
        SteamLobby.Instance.GetLobbiesList();
    }

    public void OnLobbyCanel(ClickEvent evt)
    {
        Debug.Log("OnLobbyCanel");

        _popupPanel.RemoveFromClassList("on");
        _lobbyContainer.Clear();
    }
    public void DisaplayLobbies(List<CSteamID> lobbyIDs, LobbyDataUpdate_t result)
    {
        _lobbyContainer.Clear();

        for (int i = 0; i < lobbyIDs.Count; i++)
        {
            if (lobbyIDs[i].m_SteamID == result.m_ulSteamIDLobby)
            {
                LobbyData lobbyData;

                lobbyData._lobbyID = (CSteamID)lobbyIDs[i].m_SteamID;
                lobbyData._lobbyName = SteamMatchmaking.GetLobbyData((CSteamID)lobbyIDs[i].m_SteamID, "name");

                VisualElement template = _lobbyTemplate.Instantiate().Q<VisualElement>("lobby-template");
                _lobbyContainer.Add(template);

                LobbyTemplate lobby = new LobbyTemplate(template, lobbyData);
            }
        }
    }
}
