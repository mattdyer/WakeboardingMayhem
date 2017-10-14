using UnityEngine;
using System.Collections;

public class Ramp : MovableSpawner {

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag=="Player")
		GameManager.player.StartSuperJump();
		else if(other.gameObject.tag=="Boat")
		{
			
		/*		float val = 0;
				if(this.transform.localPosition.x==0)
				{
					float[] leftRight = new float[2]{1.5f,-1.5f};
					val=leftRight[Random.Range(0,2)];
				}
				else if(this.transform.localPosition.x==1.5f)
				{
					val=-1.5f;
				}
				else if(this.transform.localPosition.x==-1.5f)
				{
					val=1.5f;
				}*/
			if(!other.GetComponent<Boat>().isMovingSide)
				StartCoroutine(other.GetComponent<Boat>().MoveAround());	
//				StartCoroutine(other.GetComponent<Boat>().MoveSide(val));	
			
		}
	}
	
}
