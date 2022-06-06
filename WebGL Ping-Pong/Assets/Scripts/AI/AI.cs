using UnityEngine;

public class AI : Platform
{
    private Vector3 _oldPositionBall;
    private Ball _ball;


    private void Start() {

        _ball = FindObjectOfType<Ball>();

        _oldPositionBall = _ball.transform.position;

        Init();
    }

    private void Update() {
        
        UpdateDirection();

        if (_oldPositionBall != _ball.transform.position) 
        {

            _oldPositionBall = _ball.transform.position;
        if (_ball.transform.position.y > transform.position.y) 
        {
              SetUpMove();
        }

        else 
        {
             SetDownMove();
        }
        }

        

        UpdateMove();


    }
}