using UnityEngine;
using System.Collections;

public class CurveController : MonoBehaviour {

	public Material[] MatObjects;
	
	private float arcX=0;
	private float arcY=0;
	
	private bool isLeft=false;
	private bool isRight=true;
	private bool isUp = false;
	private bool isDown=true;
	
	private int ySpeed;
	private int xSpeed;
	
	private float xLeftLimit=50;
	private float xRightLimit=-50f;
	private float yUpLimit=-20f;
	private float yDownLimit=1;
	
	void Start()
	{
		ySpeed = Random.Range(2,20);
		xSpeed = Random.Range(25,75);
		//StartCoroutine(RandomArc());
	}
	
	
	
	void FixedUpdate()
	{
	//	while(true)
	//	{
		if(!GameManager.instance.isGameover&&!(GameManager.instance.isGamePaused))
		{
			if(isLeft)
				arcX-=(0.1f*Time.deltaTime*xSpeed);
			else if(isRight)
				arcX+=(0.1f*Time.deltaTime*xSpeed);
				
			/*if(isUp)
				arcY+=(0.1f*Time.deltaTime*ySpeed);
			else if(isDown)
				arcY-=(0.1f*Time.deltaTime*ySpeed);
			*/
			if(arcX>xLeftLimit)
			{
				xLeftLimit=Random.Range(30,55);
				isLeft=true;
				isRight=false;
				xSpeed = Random.Range(25,75);
			
			}
			else if(arcX<xRightLimit)
			{
			xRightLimit = Random.Range(-30,-55);
				isRight=true;
				isLeft=false;
			}
			
		/*	if(arcY>yDownLimit)
			{
			
				isDown=true;
				isUp=false;
			}
			else if(arcY<yUpLimit)
			{
			yUpLimit= Random.Range(10,23);
				isDown=false;
				isUp=true;
				ySpeed = Random.Range(2,20);
			}*/
			
			foreach(Material mat in MatObjects)
			{
				if(mat){
					mat.SetVector("_QOffset",new Vector4(arcX,0,0,0));
				}
			}
		//	yield return new WaitForEndOfFrame();
		}
	}
	//	yield return null;
	}

