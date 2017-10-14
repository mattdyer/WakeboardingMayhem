using UnityEngine;
using System.Collections;

public class Enemy : MovableSpawner {
	
	
	
	
	public int enemyID;
	
//	public bool isMoving = false;
	public bool CandyTested=false;
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
				 GameManager.instance.coin.Despawn(this.transform); 
				//Destroy(this.gameObject);
			}
    	}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag=="Player")
		{
			
			if(!GameManager.player.isShieldActive)
			{
				StartCoroutine(GameManager.player.TakeDamage());
	
				GameManager.player.ImpactSoundEffect();
	
				GameManager.instance.isGameover=true;
				GameManager.player.Character.GetComponent<Animation>().Play("crash");
				GameManager.instance.gameOverPanel.SetActive(true);
//				Chartboost.CBBinding.showInterstitial( "default" );
			}
	
		}
		else if(other.gameObject.tag=="Boat")
		{
			/*if(enemyID==0)
			{
				float val = 0;
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
				}
				
				StartCoroutine(other.GetComponent<Boat>().MoveSide(val));	
			}
			else*/
			if(!other.GetComponent<Boat>().isMovingSide)
				StartCoroutine(other.GetComponent<Boat>().MoveAround());	
		}
	
	}
	
	
}
