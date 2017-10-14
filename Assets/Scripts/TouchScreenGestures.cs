using UnityEngine;
using System.Collections;

public class TouchScreenGestures : MonoBehaviour{

	#region Gesture event registration/unregistration

void OnEnable()
{
  //  Debug.Log( "Registering finger gesture events from C# script" );

    // register input events
   
    FingerGestures.OnFingerDragMove += FingerGestures_OnFingerDragMove;
    
}
  
    void OnDisable()
    {
        // unregister finger gesture events
      
        FingerGestures.OnFingerDragMove -= FingerGestures_OnFingerDragMove;
     
    }
	
	

    #endregion

    #region Reaction to gesture events
	
	
    void FingerGestures_OnFingerDragMove( int fingerIndex, Vector2 fingerPos, Vector2 delta )
    {
       if(!GameManager.instance.isGameover&&!(GameManager.instance.isGamePaused))
		{
		GameManager.player.isOnceInput=true;
		if(delta.y>(Screen.width/30)&&delta.x<(Screen.width/30)&&delta.x>(-(Screen.width/30)))
		{
				
				if(!GameManager.player.isJumping&&!GameManager.player.isDucking)
					{
					
					
						StartCoroutine(GameManager.player.Jump());
					
					}
			
		}
		else if(delta.y<(-(Screen.width/30))&&delta.x<(Screen.width/30)&&delta.x>(-(Screen.width/30)))
		{
				
				if(!GameManager.player.isDucking&&!GameManager.player.isJumping)
					{
					
					
						StartCoroutine(GameManager.player.Duck());
					
					}
			
		}
		
		if(delta.x>(Screen.width/30)&&delta.y<(Screen.width/30)&&!GameManager.instance.isSidesLocked)
		{
			if(!GameManager.player.isMovingSide && GameManager.player.Character.localPosition.x!=1.5f )
					{
					
						
					
						StartCoroutine(GameManager.player.MoveSide(1.5f));
					
					}
		}
		else if(delta.x<(-(Screen.width/30))&&delta.y<(Screen.width/30)&&!GameManager.instance.isSidesLocked)
		{
			if(!GameManager.player.isMovingSide && GameManager.player.Character.localPosition.x!=-1.5f )
				{
					
						
					
						StartCoroutine(GameManager.player.MoveSide(-1.5f));
					
				}
		}
	}
	}
	
	

    #endregion

  

 
}
