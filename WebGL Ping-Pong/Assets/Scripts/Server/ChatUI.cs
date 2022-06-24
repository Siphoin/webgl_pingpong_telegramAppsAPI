using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

using System;

public class ChatUI : MonoBehaviour
    {
    public event UnityAction<string> OnChatMessage;

    [SerializeField] private InputField _inputChat;

    [SerializeField] private Transform _content;

    [SerializeField] private Button _buttonStart;

    [SerializeField] private Button _buttonSend;


    [SerializeField] private Text _prefabNewText;
      private void Start()
        {


        if (!_buttonStart)
        {
            throw new NullReferenceException("button start not seted");
        }

        if (!_content)
        {
            throw new NullReferenceException("content not seted");
        }

        if (!_inputChat)
        {
           throw new NullReferenceException("input chat not seted");
        }

        if (!_buttonSend)
        {
            throw new NullReferenceException("button not seted");
        }

        _buttonSend.onClick.AddListener(SendNewMessage);

        _buttonStart.onClick.AddListener(StartGame);

        _buttonStart.interactable = false;
    }

    private void StartGame()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    private void SendNewMessage()
    {

        if (CheckInputChat())
        {
            string message = $"{PhotonNetwork.NickName}: {_inputChat.text}";

            ShowNewMessage(message);

            ClearInputChat();

            OnChatMessage?.Invoke(message);
        }
       
    }

    public void SendNewMessage (string message)
    {
            ShowNewMessage(message);  
    }

    public void SetActiveButtonStartGame(bool status) => _buttonStart.interactable = status;

    private bool CheckInputChat ()
    {
        bool corrent = !string.IsNullOrEmpty(_inputChat.text);

        if (corrent)
        {
            _inputChat.text = _inputChat.text.Trim();
        }

        return corrent;
    }

    private void ShowNewMessage (string message)
    {
        Text _newText = Instantiate(_prefabNewText, _content);
        _newText.text = message;
    }

    private void ClearInputChat () => _inputChat.text = null;





}