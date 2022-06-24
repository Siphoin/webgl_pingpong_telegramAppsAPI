using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using System;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text _textConnecting;

    [SerializeField] private ServerSettings _serverSettings;
    private void Start()
    {
        if (!_textConnecting)
        {
            throw new NullReferenceException("text connecting not seted");
        }

        if (!_serverSettings)
        {
            throw new NullReferenceException("server settings not seted");
        }


        Connect();

    }

    private  void Connect()
    {
        PhotonNetwork.GameVersion = Application.version;
        PhotonNetwork.AutomaticallySyncScene = true;
        _serverSettings.AppSettings.FixedRegion = "ru";

        _serverSettings.AppSettings.UseNameServer = true;

        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster () 
    {
        PhotonNetwork.NickName = $"Player_{Random.Range(1, 10001)}";
        SceneManager.LoadScene("Lobby");

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _textConnecting.text = "Reconnecting...";
        Connect();
    }





}