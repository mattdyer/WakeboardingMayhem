using UnityEngine;
using System.Collections;

public class WaterMovement : MonoBehaviour {

	
// speed of water
public float speed= 3f;
// transparency of water.  
// 0 is transparent 
// 1 is opaque
public float alpha= 1f;

// size of wave texture
public float waveScale = 10;
	
	private float moveWater=0;

void Update () 
{
	//var theTime = Time.time;
	moveWater +=Time.deltaTime * speed;// * 0.15f;
	if(moveWater>100)
			moveWater=0;
	this.gameObject.GetComponent<Renderer>().material.mainTextureOffset =new Vector2( moveWater, moveWater );	
	//this.gameObject.renderer.material.color.a = alpha;
	this.gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(waveScale, waveScale);
}
}
