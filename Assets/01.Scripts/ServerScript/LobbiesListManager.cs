using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbiesListManager : MonoBehaviour
{
    public static LobbiesListManager Instance;

    public GameObject titleText;
    public GameObject lobbiesMenu;
    public GameObject lobbyDataItemPrefab;
    public GameObject lobbyListContent;

    public GameObject lobbiesButton, hostButton;

    public List<GameObject> listOfLobbies = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }

    public void GetListOfLobbies()
    {
        titleText.SetActive(false);
        lobbiesButton.SetActive(false);
        hostButton.SetActive(false);

        lobbiesMenu.SetActive(true);

        SteamLobby.Instance.GetLobbiesList();
    }
    public void BackListOfLobbies()
    {
        titleText.SetActive(true);
        lobbiesButton.SetActive(true);
        hostButton.SetActive(true);

        lobbiesMenu.SetActive(false);
    }
    public void DisaplayLobbies(List<CSteamID> lobbyIDs, LobbyDataUpdate_t result)
    {
        for (int i = 0; i < lobbyIDs.Count; i++)
        {
            if (lobbyIDs[i].m_SteamID == result.m_ulSteamIDLobby)
            {
                GameObject createdItem = Instantiate(lobbyDataItemPrefab);

                createdItem.GetComponent<LobbyDataEntry>().lobbyID = (CSteamID)lobbyIDs[i].m_SteamID;

                createdItem.GetComponent<LobbyDataEntry>().lobbyName =
                    SteamMatchmaking.GetLobbyData((CSteamID)lobbyIDs[i].m_SteamID, "name");

                createdItem.GetComponent<LobbyDataEntry>().SetLobbyData();
                createdItem.transform.SetParent(lobbyListContent.transform);

                createdItem.transform.localScale = Vector3.one;

                listOfLobbies.Add(createdItem);

            }
        }
    }

    public void DestroyLobbies()
    {
        foreach (GameObject item in listOfLobbies)
        {
            Destroy(item);
        }
        listOfLobbies.Clear();
    }
}
