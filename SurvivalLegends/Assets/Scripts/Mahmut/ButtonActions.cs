using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;


public class ButtonActions : MonoBehaviour
{

    private NetworkManager NetworkManager;

    public TextMeshProUGUI text;
    void Start()
    {
        NetworkManager = GetComponentInParent<NetworkManager>();
    }


    public void startHost()
    {


        NetworkManager.StartHost();
        InitMovementText();
    }

    public void startClient()
    {


        NetworkManager.StartClient();
        InitMovementText();

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
