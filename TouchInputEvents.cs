//use the Unity Input class to receive accurate multitouch inputs from the user
//fingers: touch gestures

using UnityEngine;
using System.Collections;

public class TouchInputEvents : MonoBehaviour{
  public float speed = 0.1F;
  
  void Update(){
      Touch playerTouch = Input.GetTouch(0);
    
    //store inputs as an array, since the user may be using multiple touches at once
      Touch[] playerTouches = Input.touches;
    
    //use Input.GetTouch to retrieve the status of a specific touch
    if (Input.touchCount > 0 && playerTouch(0).phase == TouchPhase.Moved)
        {
            // Get movement of the finger since last frame
            Vector2 touchDeltaPosition = playerTouch(0).deltaPosition;

            // Move object across XY plane
            transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
        }
    
      for(int i = 0, i<Input.touchCount; i++){
      //trigger with touches
      }
    
    //accelerometer input
    transform.Translate(Input.acceleration.x, 0, -Input.acceleration.z);
      
      }
}
