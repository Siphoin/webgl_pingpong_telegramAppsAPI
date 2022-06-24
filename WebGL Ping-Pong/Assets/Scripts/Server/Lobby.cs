using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class Lobby : MonoBehaviourPunCallbacks, ILobbyCallbacks
{

    private string _nameSelectedRoom = null;


    [SerializeField] private InputField _nicknameField;

    [SerializeField] private Button _buttonJoinRoom;

    [SerializeField] private Button _buttonCreateRoom;

    [SerializeField] private Transform _contentRooms;

    [SerializeField] private RoomButton _prefabRoomButton;

    private List<RoomButton> _roomsButtons;

    private void Start() 
    {

        if (!_prefabRoomButton) 
        {
            throw new System.NullReferenceException("prefab room button not seted!");
        }

        _buttonCreateRoom.onClick.AddListener(CreateRoom);
        _buttonJoinRoom.onClick.AddListener(JoinRoom);

        _nicknameField.onValueChanged.AddListener(SetNickName);

        _nicknameField.text = PhotonNetwork.NickName;

        _roomsButtons = new List<RoomButton>();

        SetButtoninteractable(_buttonJoinRoom, false);


        PhotonNetwork.JoinLobby(TypedLobby.Default);


  }

    private void JoinRoom () 
    {
        PhotonNetwork.JoinRoom(_nameSelectedRoom);
        SetButtoninteractable(_buttonJoinRoom, false);

    }

    public override void OnJoinedLobby()
    {
        SetButtoninteractable(_buttonCreateRoom, true);
    }

    private void CreateRoom()
    {

        SetButtoninteractable(_buttonCreateRoom, false);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.JoinOrCreateRoom($"{PhotonNetwork.NickName}", roomOptions, TypedLobby.Default);
    }


    

    public override void OnDisconnected (DisconnectCause cause) => SceneManager.LoadScene("JoinToServer");

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Chat");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {

        UpdateRoomList(roomList);
    }

    private void SelectRoom (string name) 
    {
           _nameSelectedRoom = name;

           SetButtoninteractable(_buttonJoinRoom, true);


    }

    private void UpdateRoomList (List<RoomInfo> roomList)
    {
          

          for (int i = 0; i < _roomsButtons.Count; i++)
          {
            Debug.Log(_roomsButtons[i]);

  _roomsButtons[i].OnSelected -= SelectRoom;

            Destroy(_roomsButtons[i].gameObject);

            

              
          }

        _roomsButtons.Clear();

          for (int i = 0; i < roomList.Count; i++)
          {
            if (roomList[i].PlayerCount > 0)
            {
            RoomButton button = Instantiate(_prefabRoomButton, _contentRooms);

            button.OnSelected += SelectRoom;

            button.SetRoomInfo(roomList[i]);

            _roomsButtons.Add(button);
            }

          }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        SetButtoninteractable(_buttonCreateRoom, true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        SetButtoninteractable(_buttonJoinRoom, true);
    }



    private void SetButtoninteractable (Button button, bool status) => button.interactable = status;

    private void SetNickName (string nickName)
    {
        if (!string.IsNullOrEmpty(_nicknameField.text))
        {
            PhotonNetwork.NickName = _nicknameField.text.Trim();
        }
    }
}