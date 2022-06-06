using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class TextScore : MonoBehaviour {
    private Text _someText;

    private GameManager _gameManager;
    private void Start() 
    {
        if (!TryGetComponent<Text>(out _someText))
        {
            throw new System.NullReferenceException("Text component not seted");
        }

        _gameManager = FindObjectOfType<GameManager>();

        _gameManager.OnNewRound += RefreshText;
    }

    private void RefreshText (long scoreFirstPlayer, long scoreTwoPlayer) => _someText.text = $"{scoreFirstPlayer} | {scoreTwoPlayer}";

    private void OnDestroy() 
    {
        _gameManager.OnNewRound -= RefreshText;
    }
}