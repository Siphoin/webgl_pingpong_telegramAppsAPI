using System.Collections;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using System;
[RequireComponent(typeof(PhotonView))]
    public class Chat : MonoBehaviourPunCallbacks
    {

        private PhotonView _view;

        private ChatUI _chatUI;
       private void Start()
        {
            if (!TryGetComponent(out _view))
            {
                throw new NullReferenceException("chat not have component PhotonView");
            }

           _chatUI = FindObjectOfType<ChatUI>();

          _chatUI.OnChatMessage += SendRPCNewMessage;
    }


       private void SendRPCNewMessage (string message)
       {
        _view.RPC(nameof(ShowNewMessage), RpcTarget.Others, message);
       }

       [PunRPC]
       private void ShowNewMessage (string message)
       {
        _chatUI.SendNewMessage(message);

       }

    private void OnDestroy()
    {
        _chatUI.OnChatMessage -= SendRPCNewMessage;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("JoinToServer");
    }


    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if (newPlayer != PhotonNetwork.LocalPlayer)
        {

            RefreshActiveButtonStart();
        }


        _view.RPC(nameof(ShowNewMessage), RpcTarget.All, $"{newPlayer.NickName} join on room.");

        RefreshVisibleRoom();

    }

    private  void RefreshVisibleRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsVisible = PhotonNetwork.CurrentRoom.PlayerCount < 2;
        }
    }



    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        RefreshActiveButtonStart();

        RefreshVisibleRoom();

        SendSystemMessage($"{otherPlayer.NickName} left on room.");
    }

    private void RefreshActiveButtonStart() => _chatUI.SetActiveButtonStartGame(PhotonNetwork.CurrentRoom.PlayerCount > 1 && PhotonNetwork.IsMasterClient);

    private void SendSystemMessage(string message) => _view.RPC(nameof(ShowNewMessage), RpcTarget.All, message);
}