using UnityEngine;
using System;
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract  class Platform : MonoBehaviour 
{
     
    
    private const string PATH_DATA_PLATFORM = "DataPlatform";

    private Vector2 _direction;

    private DataPlatform _dataPlatform;
    
    private BoxCollider2D _colider;

    private Rigidbody2D _body;


    protected void Init () 
    {
        if (!TryGetComponent<BoxCollider2D>(out _colider))
        {
            throw new NullReferenceException("platform must have component Box Colider 2D");
        }

        if (!TryGetComponent<Rigidbody2D>(out _body))
        {
            throw new NullReferenceException("Rigidbody2D not seted on platform");
        }

        _dataPlatform = Resources.Load<DataPlatform>(PATH_DATA_PLATFORM);
    }

    private void SetVelocity (Vector2 velocity) => _direction = velocity;

    protected void SetUpMove () => SetVelocity(Vector2.up);

    protected void SetDownMove () => SetVelocity(Vector2.down);

    protected void UpdateDirection () => _direction = Vector2.zero;

    protected void UpdateMove () => _body.velocity = _direction * _dataPlatform.Speed  * Time.deltaTime;

    
}