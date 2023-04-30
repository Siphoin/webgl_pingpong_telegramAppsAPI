using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using System;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(PhotonView))]
public class GameManager : GameManagerBase, IPunInstantiateMagicCallback
{
    private long _scoreFirstPlayer;

    private long _scoreTwoPlayer;

    private List<Vector3> _startPositionsObjectsOfArena = new List<Vector3>();

    private List<Transform> _objectsOfArena = new List<Transform>();

    public event UnityAction OnNewRound;



     [SerializeField]  private readonly Ball _ballPrefab;

     [SerializeField] private readonly Platform _platformPrefab;


      private Ball _ball;

      private PhotonView _view;

     

    private void Start()
    {

        if (!TryGetComponent(out _view))
        {
            throw new NullReferenceException("photon view component not found on GameManager");
        }

        StartGame();


    }

    private void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _ball = PhotonNetwork.InstantiateRoomObject(_ballPrefab.name, Vector3.zero, Quaternion.identity).GetComponent<Ball>();

            _ball.OnCollisionWall += NewRound;


            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                Photon.Realtime.Player player = PhotonNetwork.PlayerList[i];

                Vector3 position = _positionsOfPlayers[i].position;

                _view.RPC(nameof(CreatePlatform), player, position);

            }

           
        }
        

        SetPlayerNameUI("PlayerNameUI1", PhotonNetwork.PlayerList[0].NickName);
        SetPlayerNameUI("PlayerNameUI2", PhotonNetwork.PlayerList[1].NickName);
    }

    private void NewRound ()
    {

        if (_ball.transform.position.x < 0)
        {
            _scoreTwoPlayer++;
        }

        else
        {
            _scoreFirstPlayer++;
        }

        _view.RPC(nameof(CalculateScores), RpcTarget.Others, _scoreFirstPlayer, _scoreTwoPlayer);


        for (int i = 0; i < _objectsOfArena.Count; i++)
        {
            _objectsOfArena[i].position = _startPositionsObjectsOfArena[i];
        }

        _ball.ResetBall();

        OnNewRound?.Invoke();

        FindObjectOfType<TextScore>().RefreshText(_scoreFirstPlayer, _scoreTwoPlayer);



    }

    private void SetPlayerNameUI (string tag, string nickName)
    {
        GameObject.FindGameObjectWithTag(tag).GetComponent<Text>().text = nickName;
    }
    
    [PunRPC]
    private void CalculateScores(long scoreFirstPlayer, long scoreThirdPlayer)
    {
       
        _scoreFirstPlayer = scoreFirstPlayer;
        _scoreTwoPlayer = scoreThirdPlayer;

        FindObjectOfType<TextScore>().RefreshText(_scoreFirstPlayer, _scoreTwoPlayer);


    }

    [PunRPC]
    private void CreatePlatform(Vector3 position) => PhotonNetwork.Instantiate(_platformPrefab.name, position, Quaternion.identity);

    

    private void OnDestroy() => _ball.OnCollisionWall -= NewRound;

    public override void OnDisconnected(DisconnectCause cause) => SceneManager.LoadScene("JoinToServer");

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        GameObject gameObject = info.photonView.gameObject;


        if (!PhotonNetwork.IsMasterClient)
        {
            if (!_ball)
            {
                 gameObject.TryGetComponent(out _ball);
            }
        }

        _startPositionsObjectsOfArena.Add(gameObject.transform.position);

        _objectsOfArena.Add(gameObject.transform);


    }


}
