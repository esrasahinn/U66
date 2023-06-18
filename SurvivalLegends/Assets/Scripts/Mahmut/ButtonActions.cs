using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Unity.Netcode.Transports.UTP;
using TMPro;

public class ButtonActions : MonoBehaviour
{
    public TMP_InputField ipInputField;
    public TextMeshProUGUI ipText;
    private NetworkManager NetworkManager;
    private UnityTransport unityTransport;

    public TextMeshProUGUI text;

    void Start()
    {
        NetworkManager = GetComponentInParent<NetworkManager>();
        unityTransport = NetworkManager.NetworkConfig.NetworkTransport as UnityTransport;
    }

    public void startHost()
    {
        unityTransport.ConnectionData.Address = ipInputField.text;
        NetworkManager.StartHost();

        ipText.text = ipInputField.text;

        InitMovementText();
    }

    public void startClient()
    {
        unityTransport.ConnectionData.Address = ipInputField.text;
        ipText.text = ipInputField.text;

        NetworkManager.StartClient();

        InitMovementText();
    }

    public void Disconnect()
    {
    NetworkManager.Shutdown();
    }


    public void SubmitNewPosition()
    {
        var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
        var player = playerObject.GetComponent<PlayerMove>();
        player.Move();
    }

    private void InitMovementText()
    {
        if (NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsHost)
        {
            text.text = "MOVE";
        }
        else if (NetworkManager.Singleton.IsClient)
        {
            text.text = "Request Move";
        }
    }
}
