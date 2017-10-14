using UnityEngine;
using System.Collections;

public class MovableSpawner : MonoBehaviour {

	public bool isMoving = false;
	public bool isCustom=false;
	public int range=0;
	
	// Update is called once per frame
	void Update () {
		
		
		if(isMoving&&!(GameManager.instance.isGameover)&&!(GameManager.instance.isGamePaused))
		{
			transform.Translate(Vector3.back * Time.deltaTime*GameManager.instance.speed);
			
			if(isCustom)
			{
        		if(transform.localPosition.z<-range)
				{
					
					isMoving=false;
					
					if(transform.localPosition.z<60)
						GameManager.instance.isTunnel=false;
						
						
					transform.localPosition= new Vector3(0,0,150);
				}
			}
			else
			{
				if(transform.localPosition.z<-15)
				{
				Destroy(this.gameObject);
				}
			}
    	}
	}
}
