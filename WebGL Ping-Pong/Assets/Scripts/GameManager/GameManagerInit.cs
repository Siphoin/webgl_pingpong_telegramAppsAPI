
using UnityEngine;
using Photon.Pun;
using System;

    public class GameManagerInit : GameManagerBase, IPunInstantiateMagicCallback
{

    [SerializeField] private GameManager _gameManager;

    [SerializeField] protected  Transform[] _positionsOfPlayersForInit;

    [SerializeField] private GameObject _textScoreForInit;


    private void Start()
        {


        if (!_gameManager)
        {
            throw new NullReferenceException("gameManager reference not seted on GameManagerInit");
        }

        if (!_textScoreForInit)
        {
            throw new NullReferenceException("textScore reference not seted on GameManagerInit");
        }

        if (PhotonNetwork.IsMasterClient)
        {
         GameManager gameManager = PhotonNetwork.InstantiateRoomObject(_gameManager.name, Vector3.zero, Quaternion.identity).GetComponent<GameManager>();
            gameManager.SetArrayPositionOfPlayers(_positionsOfPlayersForInit);
            gameManager.SetTextScore(_textScoreForInit);
        }
        }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        Debug.Log(3443);

        GameManager gameManager = null;

        if (info.photonView.gameObject.TryGetComponent(out gameManager))
        {
            gameManager.SetTextScore(_textScoreForInit);

            Destroy(this);
        }
    }
}

   