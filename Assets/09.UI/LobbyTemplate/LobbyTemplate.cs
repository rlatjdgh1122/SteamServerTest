using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LobbyTemplate
{
    private Label _roomName;
    private Label _roomCount;

    public LobbyTemplate(VisualElement root, LobbyData lobbyData)
    {
        _roomName = root.Q<Label>("room-name");
        _roomName.text = lobbyData._lobbyName;
    }
}
