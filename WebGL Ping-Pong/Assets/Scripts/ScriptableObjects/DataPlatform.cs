using UnityEngine;

[CreateAssetMenu(fileName = "DataPlatform", menuName = "WebGL Ping-Pong/DataPlatform", order = 0)]
public class DataPlatform : ScriptableObject {
    [SerializeField] private float _speed = 1;

    public float Speed => _speed;

    private void OnValidate() {
        if (_speed <= 0) 
        {
            _speed = 1;
        }
    }
}