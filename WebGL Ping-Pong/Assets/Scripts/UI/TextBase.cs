using System.Collections;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public abstract class TextBase : MonoBehaviour
    {
    private Text _someText;

    protected void Init ()
    {
        if (!TryGetComponent(out _someText))
        {
            throw new System.NullReferenceException("text on TextBase component not found");
        }
    }

    protected void SetText(string text) => _someText.text = text;
}