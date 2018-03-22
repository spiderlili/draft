//use the Unity Input class to receive accurate multitouch inputs from the user
//fingers: touch gestures

using UnityEngine;
using System.Collections;

public class TouchInputEvents : MonoBehaviour{
  public float speed = 0.1F;
  
  void Update(){
    //get the first touch in the array
      Touch playerTouch = Input.GetTouch(0);
    
    //store inputs as an array and retrieve all inputs, since the user may be using multiple touches at once
      Touch[] playerTouches = Input.touches;
    
    //playerTouch.fingerId is an unique identifier to that specific touch for its lifetime - can be used to track the action of that specific touch
    //once that touch is no longer in the array, the touch is finished and other touches can happen
    //playerTouch.position is the position of the touch on the screen
    //playerTouch.deltaPosition is the difference between the touch position from the last frame to the current frame - useful for telling touch direcion;
    //playerTouch.deltaTime it the amount of time that has passed since the last recorded change in touch values.
    //generally touches are detected in the update loop so playerTouch.deltaTime would be the same as Time.deltaTime
   
    playerTouch.phaseBegan = TouchPhase.Began; //returned on the first frame of the touch
    //when the device can't handle the input of the screen - more touches than the device supports, a large surface presses on the screen
    playerTouch.phaseCanceled = TouchPhase.Canceled; 
    playerTouch.phaseEnded = TouchPhase.Ended; //returned on the last frame of the touch
    playerTouch.phaseMoved = TouchPhase.Moved; //when the move has changed position on the screen
    playerTouch.phaseStationary = TouchPhase.Stationary; //when the touch isn't moving
    
    //use Input.GetTouch to retrieve the status of a specific touch
    if (Input.touchCount > 0 && playerTouch(0).phase == TouchPhase.Moved)
        {
            // Get movement of the finger since last frame
            Vector2 touchDeltaPosition = playerTouch(0).deltaPosition;

            // Move object across XY plane
            transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
        }
    
    //each touch has a tapCount property which measures how many taps are made in quick succession: playerTouch.tapCount;
    //each time a tap is recorded: the tapId would be updated
    //if multiple fingers tapped this would be treated as the same finger
    //iterate through the touches array in order to treat each touch individually
    
      for(int i = 0, i<Input.touchCount; i++){
      //trigger with touches
      }
    
    //accelerometer input
    transform.Translate(Input.acceleration.x, 0, -Input.acceleration.z);
      
      }

}
