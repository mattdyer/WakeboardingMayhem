using UnityEngine;
using System.Collections;

public class SingleLaneTunnel : MonoBehaviour {

	
	public bool isEntry=false;
	
	public bool isExit=false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	
	void OnTriggerEnter(Collider col)
	{
		
		if(col.gameObject.tag=="Player")
		{
			if(isEntry)
			{
				GameManager.instance.isSidesLocked=true;
				
			}
			else if(isExit)
			{
				GameManager.instance.isSidesLocked=false;
				
			}
			
		}
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
