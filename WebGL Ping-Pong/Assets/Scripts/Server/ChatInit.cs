using Photon.Pun;
using System.Collections;
using UnityEngine;
using System;
    public class ChatInit : MonoBehaviourPunCallbacks
    {

    [SerializeField] private Chat _chatComponent;
        void Start()
        {
           if (!_chatComponent)
           {
            throw new NullReferenceException("chat Component not seted on ChatInit");
           }


           if (PhotonNetwork.IsMasterClient)
           {
            PhotonNetwork.InstantiateRoomObject(_chatComponent.name, Vector3.zero, Quaternion.identity);

           }

        Destroy(this);
    }
    }