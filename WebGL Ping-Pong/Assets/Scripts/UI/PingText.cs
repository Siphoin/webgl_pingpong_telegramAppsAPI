using Photon.Pun;
using System.Collections;
using UnityEngine;

    public class PingText : TextBase
    {


    private void Start() => Init();

    private void Update() => SetText($"Ping: {PhotonNetwork.GetPing()}");

}