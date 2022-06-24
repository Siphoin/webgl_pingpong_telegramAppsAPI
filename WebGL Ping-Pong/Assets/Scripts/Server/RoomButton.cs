using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(Button))]
public class RoomButton : MonoBehaviour 
{
     public event UnityAction<string> OnSelected;

     private Button _someButton;

     private RoomInfo _currentRoomInfo;

    [SerializeField] private Text _textButton;
    private void Start() 
    {
        if (!TryGetComponent(out _someButton)) 
        {
             throw new System.NullReferenceException("button reference not seted on RoomButton");
        }

        if (!_textButton) 
        {
              throw new System.NullReferenceException("text button not seted on RoomButton");
        }

        _someButton.onClick.AddListener(Select);
    }

    public void SetRoomInfo (RoomInfo roomInfo) 
    {
          if (roomInfo == null) 
          {
               throw new System.ArgumentNullException("roominfo is null");
          }

          _currentRoomInfo = roomInfo;

          _textButton.text = _currentRoomInfo.Name;
    }

    private void Select () 
    {
            if (_currentRoomInfo == null) 
            {
                  throw new System.NullReferenceException("current room info not seted");
            }

            OnSelected?.Invoke(_currentRoomInfo.Name);


    }


}