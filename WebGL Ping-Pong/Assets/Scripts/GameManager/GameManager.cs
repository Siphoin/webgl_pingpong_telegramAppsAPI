using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour {
     private long _scoreFirstPlayer;

     private long _scoreTwoPlayer;

     public event UnityAction<long, long> OnNewRound;

     private Ball _ball;

      Vector3[] _startPositionsObjectsOfArena;

     [SerializeField] private Transform[] _objectsOfArena;

     

    [SerializeField] private Transform _line;
    private void Start() 
    {
        if (!_line)
        {
            throw new System.NullReferenceException("Line not seted");
        }

        _ball = FindObjectOfType<Ball>();

        _ball.OnCollisionWall += NewRound;

        _startPositionsObjectsOfArena = new Vector3[_objectsOfArena.Length];

         for (int i = 0; i < _objectsOfArena.Length; i++)
         {
             _startPositionsObjectsOfArena[i] = _objectsOfArena[i].position;
         }


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
         for (int i = 0; i < _objectsOfArena.Length; i++)
         {
             _objectsOfArena[i].position = _startPositionsObjectsOfArena[i];
         }

         _ball.ResetBall();

         OnNewRound?.Invoke(_scoreFirstPlayer, _scoreTwoPlayer);
         


    }

    private void OnDestroy() => _ball.OnCollisionWall -= NewRound;
}