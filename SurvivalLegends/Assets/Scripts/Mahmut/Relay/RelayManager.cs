using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Services.Authentication;
public class RelayManager : MonoBehaviour
{
    private string PlayerID;
    public TMP_InputField joinInputField;
    private RelayHostData _hostData;
    private RelayJoinData _joinData;
    public TextMeshProUGUI playerText;
    public TextMeshProUGUI joinCodeText;
    public TextMeshProUGUI connected;

    private NetworkManager NetworkManager;

    public TMP_Dropdown playerCount;

    public string Test;



    async void Start()
    {
        await UnityServices.InitializeAsync();
        Debug.Log("Unity Service İntilaze");
        singIn();

    }

    async void singIn()
    {

        Debug.Log("Sign In");
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        PlayerID = AuthenticationService.Instance.PlayerId;
        Debug.Log("Giriş Yapıldı " + PlayerID);
        playerText.text = PlayerID;
    }

    public async void OnHostClick()
    {

        int maxPlayerCount = Convert.ToInt32(playerCount.options[playerCount.value].text);
        // Debug.Log(maxPlayerCount);
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxPlayerCount);
        _hostData = new RelayHostData()
        {
            IPv4Address = allocation.RelayServer.IpV4,
            Port = (ushort)allocation.RelayServer.Port,
            AllocationID = allocation.AllocationId,
            AllocationIDBytes = allocation.AllocationIdBytes,
            ConnectionData = allocation.ConnectionData,
            Key = allocation.Key
        };


        _hostData.JoinCode = await RelayService.Instance.GetJoinCodeAsync(_hostData.AllocationID);
        joinCodeText.text = _hostData.JoinCode;
        Debug.Log("Allocated Complate" + _hostData.AllocationID);
        Debug.LogWarning("JoinCode " + _hostData.JoinCode);

        UnityTransport transport = NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>();
        transport.SetRelayServerData(_hostData.IPv4Address, _hostData.Port, _hostData.AllocationIDBytes, _hostData.Key, _hostData.ConnectionData);
        NetworkManager.Singleton.StartHost();
    }

    public async void OnJoinClick()
    {

        try
        {

            // Debug.Log("Join Code: " + joinInputField.text);
            string joinCode = joinInputField.text;
            JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            _joinData = new RelayJoinData()
            {
                IPv4Address = allocation.RelayServer.IpV4,
                Port = (ushort)allocation.RelayServer.Port,
                AllocationID = allocation.AllocationId,
                AllocationIDBytes = allocation.AllocationIdBytes,
                ConnectionData = allocation.ConnectionData,
                HostConnectionData = allocation.HostConnectionData,
                Key = allocation.Key
            };

            Debug.Log("Join Success: " + _joinData.AllocationID);
            connected.text = "Connected";
            UnityTransport transport = NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>();
            transport.SetRelayServerData(_joinData.IPv4Address, _joinData.Port, _joinData.AllocationIDBytes, _joinData.Key, _joinData.ConnectionData, _joinData.HostConnectionData);
            NetworkManager.Singleton.StartClient();
        }
        catch (Exception ex)
        {
            Debug.Log("Bir Hata Oluştu");
            Debug.LogError(ex);
            connected.text = "NotConnect";

        }

    }


}

public struct RelayHostData
{

    public string JoinCode;
    public string IPv4Address;
    public ushort Port;

    public Guid AllocationID;

    public byte[] AllocationIDBytes;
    public byte[] ConnectionData;
    public byte[] Key;





}

public struct RelayJoinData
{


    public string IPv4Address;
    public ushort Port;

    public Guid AllocationID;

    public byte[] AllocationIDBytes;
    public byte[] ConnectionData;
    public byte[] HostConnectionData;
    public byte[] Key;





}
