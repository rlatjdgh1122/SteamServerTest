using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScreen : MonoBehaviour
{
    public static MainMenuScreen Instance;

    [SerializeField] private VisualTreeAsset _lobbyTemplate;
    [SerializeField] private CustomNetworkManager _customNetworkManager;
    private VisualElement root;
    private VisualElement _popupPanel;
    private UIDocument _uiDocument;

    private void Awake()
    {
        _uiDocument = (UIDocument)GetComponent("UIDocument");


    }
    private void OnEnable()
    {
        if (Instance == null) Instance = this;

        root = _uiDocument.rootVisualElement;
        _popupPanel = root.Q<VisualElement>("popup-panel");


        root.Q<Button>("btn-create-room").RegisterCallback<ClickEvent>(OnCreateRoom);
        root.Q<Button>("btn-lobby").RegisterCallback<ClickEvent>(OnShowLobby);

    }
    private void Start()
    {
        SteamLobby.Instance = new(_customNetworkManager);
        LobbyUIManager.Instance = new(_lobbyTemplate, root, _popupPanel);
    }
    private void OnCreateRoom(ClickEvent evt)
    {
        SteamLobby.Instance.HostLobby();
    }
    private void OnShowLobby(ClickEvent evt)
    {
        _popupPanel.AddToClassList("on");

        SteamLobby.Instance.GetLobbiesList();
    }
}
