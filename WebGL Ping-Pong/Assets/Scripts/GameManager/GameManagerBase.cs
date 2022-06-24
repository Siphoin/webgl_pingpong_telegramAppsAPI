using UnityEngine;
using Photon.Pun;

    public abstract class GameManagerBase : MonoBehaviourPunCallbacks
    {

     protected Transform[] _positionsOfPlayers;

     protected GameObject _textScore;

    public void SetArrayPositionOfPlayers(Transform[] positions)
    {
        _positionsOfPlayers = positions;

    }

    public void SetTextScore (GameObject textScore) => _textScore = textScore;


    }