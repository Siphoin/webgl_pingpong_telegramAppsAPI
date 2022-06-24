using UnityEngine;
using Photon.Pun;
public class Player : Platform {

    private PhotonView _view;

    private void Start() 
    {
        Init();

        if (!TryGetComponent(out _view)) 
        {
           throw new System.NullReferenceException("view component not found on Player!");
        }

        

        enabled = _view.IsMine;
    }
    private void Update()
    
    {
        UpdateDirection();


        if (Input.GetKey(KeyCode.W)) 
        {
             SetUpMove();
        }

        if (Input.GetKey(KeyCode.S)) 
        {
            SetDownMove();
        }


        if (Input.touchCount == 1) 
        {
           Touch touch = Input.GetTouch(0);

           Vector3 positionTouch = Camera.main.ScreenToWorldPoint(touch.position);

           if (positionTouch.y > 0) 
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