using System.Collections;
using UnityEngine;
using Photon.Pun;
using System;
[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(Rigidbody2D))]
public class SyncRigidBody2D : MonoBehaviourPunCallbacks, IPunObservable
    {

    private Rigidbody2D _rigidbody;


    private void Start() => Init();

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (!_rigidbody)
        {
            Init();
        }


        if (stream.IsWriting)
        {
            stream.SendNext(_rigidbody.position);
            stream.SendNext(_rigidbody.rotation);
            stream.SendNext(_rigidbody.velocity);
        }
        else
        {
            _rigidbody.position = (Vector2)stream.ReceiveNext();
            _rigidbody.rotation = (float)stream.ReceiveNext();
            _rigidbody.velocity = (Vector2)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            _rigidbody.position += _rigidbody.velocity * lag;
        }
    }

    private void Init ()
    {
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 60;

        if (!TryGetComponent(out _rigidbody))
            {
            throw new NullReferenceException("rigidbody2D not seted on SyncRigidBody2D");
            }
        
    }
}