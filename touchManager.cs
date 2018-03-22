//use on touchmanager empty game object
//TouchPhase phases: press down = Began, move = Moved, release press = Ended
//

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour{
  public Text touchCountDebug;
  GameObject objPlane;
  Vector3 m0;
  
  //generate touch ray - send it through the touch position on the screen(Can have up to 10 fingers touching the screen)
  //find the ray from any particular touch on the screen by making this a variable to pass into this function
  Ray GenerateouseRay(Vector3 touchPos){
  
  //calculating a ray from the near plane of the camera through to the far plane of the camera according to the current projection that it's set in
  Vector3 mousePosFar = new Vector3(touchPos.x, touchPos.y, Camera.main.farClipPlane);
  Vector3 mousePosNear = new Vector3(touchpos.x, touchPos.y, Camera.main.nearClipPlane);
  Vector3 mousePosF = Camera.main.ScreenToWorldPoint (mousePosFar);
  Vector3 mousePosN = Camera.main.ScreenToWorldPoint (mousePosNear);
  Ray mouseRay = new Ray (mousePosN, mousePosF - mousePosN);
  return mouseRay;
  
  }
  
  void Update(){
  //report the number of touches on the screen, if >=1 deal with multitouch
    touchCountDebug.text = Input.touchCount.ToString();
    
    //Don't process anything if not using multitouch - touchCount cannot detect how many fingers are down at once
    if(Input.touchCount > 0){
    
    //gets the first touch that has been registered on the screen
        //generating the mouse ray using the position of that finger on the screen 
    //then use the ray and cast it out into the world, grabbing hold of something if it gets hit(the game character)
    //calculate the offset with the touch position
    
      if(Input.GetTouch(0).phase ==TouchPhase.Began){
        Ray mouseRay = GenerateMouseRay(Input.GetTouch(0).position);
        RaycastHit hit;
        
        if(Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit)){
          gObj = hit.transform.gameObject;
          objPlane = new Plane(Camera.main.transform.forward*-1, gObj.transform.position);
          
          //calculate touch offset
          Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
          float rayDistance;
          objPlane.Raycast(mRay, out rayDistance);
          m0 = gObj.transform.position - mRay.GetPoint(rayDistance);
        }
    } //check if have hold of something that can move around
    else if(Input.GetTouch(0).phase == TouchPhase.Moved && gObj){
      //use the touch position to create a ray and move the object around
      Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
      float rayDistance;
      if(objPlane.Raycast(mRay, out rayDistance))
        gObj.transform.position = mRay.GetPoint(rayDistance) + m0;
        }
        else if(Input.GetTouch(0).phase == TouchPhase.Ended && gObj){
        //release the object if touch has ended and got hold of the game object
          gObj = null; 
        }
    }
    }



  }
}
