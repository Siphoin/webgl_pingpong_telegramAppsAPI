using UnityEngine;

[CreateAssetMenu(fileName = "DataBall", menuName = "WebGL Ping-Pong/DataBall", order = 0)]
public class DataBall : ScriptableObject 
{
    [SerializeField] private float _speed = 1;

    public float Speed => _speed;

        private void OnValidate() {
        if (_speed <= 0) 
        {
            _speed = 1;
        }
    }
}