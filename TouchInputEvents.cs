using UnityEngine;
using System.Collections;

public class TouchInputEvents : MonoBehaviour{
  void Update(){
      Touch playerTouch = Input.GetTouch(0);
      Touch[] playerTouches = Input.touches;
      
      for(int i = 0, i<Input.touchCount; i++){
      //trigger with touches
      }
      
      }
}
