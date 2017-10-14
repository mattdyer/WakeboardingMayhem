using UnityEngine;
using System.Collections;

public class Boosts : MonoBehaviour {
	
	public bool isMoving = false;
	public BoostsTypes type;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(isMoving&&!(GameManager.instance.isGameover)&&!(GameManager.instance.isGamePaused))
		{
			transform.Translate(Vector3.back * Time.deltaTime*GameManager.instance.speed);
			
        	if(transform.localPosition.z<-15)
			{
				Destroy(this.gameObject);
			}
    	}
	}
	
	

	
	
}
