using UnityEngine;

public class Player : Platform {

    private void Start()  => Init();
    private void Update()
    
    {
        UpdateDirection();


        if (Input.GetKey(KeyCode.S)) 
        {
            SetDownMove();
        }

        if (Input.GetKey(KeyCode.W)) 
        {
            SetUpMove();
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