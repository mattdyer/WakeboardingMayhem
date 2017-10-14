using UnityEngine;
using System.Collections;

public class Boat : MonoBehaviour {
	
	
	private Transform Character;
	
	public bool isJumping=false;
	
	public bool isMovingSide=false;
	
	public bool isJumpStarted=false;
	
	private bool isJumpEnded = false;
	
	public GameObject WaterSplash;
	
	private float weight=0f;
	
	void Start()
	{
		Character= this.transform;
	}
	
	/// <summary>
	/// Moves the side.
	/// </summary>
	/// <returns>
	/// The side.
	/// </returns>
	/// <param name='direction'>
	/// Direction.
	/// </param>
/*	public IEnumerator MoveSide(float direction)
	{
		float endPos=transform.localPosition.x+direction;
		float startPos=transform.localPosition.x;
		isMovingSide=true;
		bool moving=true;
		
		while(moving)
		{
			if(transform.localPosition.x!=endPos)
				{
					weight += Time.deltaTime * 6; //amount
				//transform.localRotation = Quaternion.Lerp (Quaternion.Euler(0,0,0),Quaternion.Euler(0,0,(-10*direction)), weight);
         		   transform.position = Vector3.Lerp(new Vector3(startPos,transform.position.y,transform.position.z),
						new Vector3(endPos,transform.position.y,transform.position.z), weight);
				}
			else
				moving=false;
			
		
			
			yield return null;
		}
		
		
		isMovingSide=false;
		weight=0;
		
	
		
	}*/
	
	public IEnumerator MoveAround()
	{
		float direction=0f;
		if(Character.transform.localPosition.x==0)
		{
			float[] leftRight = new float[2]{3f,-3f};
					direction=leftRight[Random.Range(0,2)];
			//=3f;
		}
		else if(Character.transform.localPosition.x==-1.5f)
		{
			direction=-1.5f;
		}
		else if(Character.transform.localPosition.x==1.5f)
		{
			direction=1.5f;
		}
		
		float endPos=transform.localPosition.x+direction;
		float startPos=transform.localPosition.x;
		isMovingSide=true;
		bool moving=true;
		
		while(moving)
		{
			if(transform.localPosition.x!=endPos)
				{
					weight += Time.deltaTime * 3; //amount
					transform.localRotation = Quaternion.Lerp (Quaternion.Euler(0,0,0),Quaternion.Euler(0,0,(-10*direction)), weight);
         		   transform.position = Vector3.Lerp(new Vector3(startPos,transform.position.y,transform.position.z),
						new Vector3(endPos,transform.position.y,transform.position.z), weight);
				}
			else
				moving=false;
			
		
			
			yield return null;
		}
		
		moving=true;
		weight=0;
		yield return new WaitForSeconds(.5f);
		
		
		while(moving)
		{
			if(transform.localPosition.x!=startPos)
				{
					
					weight += Time.deltaTime * 3; //amount
				transform.localRotation = Quaternion.Lerp (Quaternion.Euler(0,0,(-10*direction)),Quaternion.Euler(0,0,0), weight);
         		   transform.position = Vector3.Lerp(new Vector3(endPos,transform.position.y,transform.position.z),
						new Vector3(startPos,transform.position.y,transform.position.z), weight);
				}
			else
				moving=false;
			
		
			
			yield return null;
		}
		
		isMovingSide=false;
		weight=0;
		
	
		
	}
	
	public void AlignCenter()
	{
		
		if(this.transform.localPosition.x==0)
		{
			return;
		}
		
//		Debug.Log("boat align");
		
	//	StartCoroutine(MoveSide((this.transform.localPosition.x*-1)));
			
		
		
	}
	
	/// <summary>
	/// Jump this instance.
	/// </summary>
	public IEnumerator Jump()
	{
		isJumping=true;
		isJumpStarted=true;
//		WaterSplash.GetComponent<ParticleEmitter>().emit=false;
		while(isJumping)
		{
			if(isJumpStarted)
			{
			
				if(Character.localPosition.y<2f)
				{
					Character.Translate(Vector3.up * Time.deltaTime*4.5f);
				
					
				}
				else
				{
					isJumpStarted=false;
					isJumpEnded=true;
					
				}
			}
			else if(isJumpEnded)
			{
			
				if(Character.localPosition.y>0)
				{
					Character.Translate(Vector3.down* Time.deltaTime*6);
				
				}
				else
				{
					Character.localPosition=new Vector3(Character.localPosition.x,0f,Character.localPosition.z);
				
					isJumpEnded=false;
					isJumping=false;
				}
			}
			
			yield return null;
		}
		//WaterSplash.GetComponent<ParticleEmitter>().emit=true;
	}
}
