using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Extensions.GO;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Ball : MonoBehaviour
{
    private const string PATH_DATA_BALL = "DataBall";

    private Vector2 _direction;

    public event UnityAction OnCollisionWall;

    private CircleCollider2D _colider;

    private DataBall _dataBall;

    private Rigidbody2D _body;
   private void Start()
    {
        if (!TryGetComponent<CircleCollider2D>(out _colider))
        {
            throw new System.NullReferenceException("platform must have component Circle Colider 2D");
        }

        if (!TryGetComponent<Rigidbody2D>(out _body))
        {
            throw new System.NullReferenceException("platform must have component Rigidbody2D");
        }

        _dataBall = Resources.Load<DataBall>(PATH_DATA_BALL);

        ResetBall();
    }

    public void ResetBall()

    
    {

         _body.velocity = Vector2.zero;

        transform.position = Vector3.zero;


        float rand = Random.Range(0, 2);
        if (rand < 1)
        {
            _body.AddForce(new Vector2(150, 0));
        }
        else
        {
            _body.AddForce(new Vector2(-150, 0));
        }

        _direction = _body.velocity;
    }

    private void OnCollisionEnter2D(Collision2D objectCollision) 
    {
            Platform platform = null;

            Wall wall = null;

           if (objectCollision.gameObject.TryGetComponent<Platform>(out platform))
        {
            AddForceBall(objectCollision);
        }

        if (objectCollision.gameObject.TryGetComponent<Wall>(out wall)) 
           {
               OnCollisionWall?.Invoke();
           }
    }

}
