using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MenuScript : MonoBehaviour
{
    public static MenuScript Instance { set; get; }

    [SerializeField] private GameObject mainMenu, serverMenu, connectMenu, serverPrefab, clientPrefab;

    private void Start()
    {
        Instance = this;
        serverMenu.SetActive(false);
        connectMenu.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }
    public void HostButton()
    {
        try
        {
            ServerScript s = Instantiate(serverPrefab).GetComponent<ServerScript>();
            s.Init();

            ClientScript c = Instantiate(clientPrefab).GetComponent<ClientScript>();
            c.clientName = "Host";
            c.ConnectToServer("127.0.0.1", 6321);
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);

        }

        mainMenu.SetActive(false);
        serverMenu.SetActive(true);
    }
    public void JoinButton()
    {
        mainMenu.SetActive(false);
        connectMenu.SetActive(true);
    }
    public void ConnectButton()
    {
        string hostAddress = GameObject.Find("HostInput").GetComponent<InputField>().text;
        if (hostAddress == "")
            hostAddress = "127.0.0.1";
        try
        {
            ClientScript c = Instantiate(clientPrefab).GetComponent<ClientScript>();
            c.clientName = "Client";
            c.ConnectToServer(hostAddress, 6321);
            connectMenu.SetActive(false);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public void BackButton()
    {
        mainMenu.SetActive(true);
        connectMenu.SetActive(false);
        serverMenu.SetActive(false);

        ServerScript s = FindObjectOfType<ServerScript>();
        if (s != null)
            Destroy(s.gameObject);

        ClientScript c = FindObjectOfType<ClientScript>();
        if (c != null)
            Destroy(c.gameObject);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

}
