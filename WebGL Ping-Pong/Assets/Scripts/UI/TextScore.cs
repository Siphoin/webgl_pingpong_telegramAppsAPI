using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class TextScore : TextBase {

    private void Start() => Init();

    public void RefreshText (long scoreFirstPlayer, long scoreTwoPlayer) => SetText($"{scoreFirstPlayer} | {scoreTwoPlayer}");
}