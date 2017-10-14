using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public Transform Character;
	
	public Transform Rope;
	
	public bool isJumping=false;
	
	public bool isDucking=false;
	
	
	public bool isMovingSide=false;
	
	public bool isOnceInput=false;
	
	public bool isThrowingGift = false;
	
	public bool isJumpStarted=false;
	
	private bool isJumpEnded = false;
	
	public bool isShieldActive=false;
	
	public GameObject WaterSplash;
	
	public GameObject Shield;
	
	//public ParticleEmitter spark;
	
	private float weight=0f;
	
	public Material[] boardMats;
	
	public Transform board;

    private Transform myTransform;
	
	
	
	// Use this for initialization
	void Start () {
			myTransform = transform; 
		//	Projectile = GameManager.instance.giftPrefab.transform as Transform;
			GameManager.player=this;
		int val = PlayerPrefs.GetInt("currentBoard",1);
		
		board.GetComponent<Renderer>().material=boardMats[--val];
		
		foreach(AnimationState animSt in Character.GetComponent<Animation>())
		{
			
			animSt.speed=1.5f;
			
		}
	}
	
	/// <summary>
	/// Fixeds the update.
	/// </summary>
	void FixedUpdate()
	{
		if((!Character.GetComponent<Animation>().isPlaying)&&!(Character.GetComponent<Animation>()["idle"].enabled))
		{
 			
			Character.GetComponent<Animation>().Play("idle");
		}
		
		if(this.transform.localPosition.y<-3f)
		{

				GameManager.instance.gameOverPanel.SetActive(true);
		}
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
	public IEnumerator MoveSide(float direction)
	{
		float endPos=transform.localPosition.x+direction;
		float startPos=transform.localPosition.x;
		isMovingSide=true;
		bool moving=true;
		
		if(direction==-1.5f)
		{
			Character.GetComponent<Animation>().Play("turnLeft");
		}
		else
		{
			Character.GetComponent<Animation>().Play("turnRight");
		}
		
		while(moving)
		{
			if(transform.localPosition.x!=endPos)
				{
					weight += Time.deltaTime * 6; //amount
         		   transform.position = Vector3.Lerp(new Vector3(startPos,transform.position.y,transform.position.z),
						new Vector3(endPos,transform.position.y,transform.position.z), weight);
				}
			else
				moving=false;
			
		
			
			yield return null;
		}
		
		isMovingSide=false;
		weight=0;
		
	
		
	}
	
	public IEnumerator Duck()
	{
		isDucking=true;
		Character.GetComponent<Animation>().Play("duck");
		
		Character.GetComponent<BoxCollider>().center=new Vector3(0,-.8f,0);
		Character.GetComponent<BoxCollider>().size=new Vector3(1,.57f,1);
		
		
		yield return new WaitForSeconds(.5f);
		
		
		Character.GetComponent<BoxCollider>().center=new Vector3(0,-.37f,0);
		Character.GetComponent<BoxCollider>().size=new Vector3(1,1.44f,1);
		
		isDucking=false;
	}
	
	
	
	/// <summary>
	/// Jump this instance.
	/// </summary>
	public IEnumerator Jump()
	{
		isJumping=true;
		isJumpStarted=true;
		//WaterSplash.GetComponent<ParticleEmitter>().emit=false;
		WaterSplash.GetComponent<ParticleSystem>().Pause(true);
		WaterSplash.GetComponent<ParticleSystem>().Clear();
		while(isJumping)
		{
			if(isJumpStarted)
			{
				Character.GetComponent<Animation>().Play("jumpStart");
				if(Character.localPosition.y<2f)
				{
					Character.Translate(Vector3.up * Time.deltaTime*4.5f);
				//	Rope.Rotate(Vector3.right * Time.deltaTime * 14, Space.Self);
					
				}
				else
				{
					isJumpStarted=false;
					isJumpEnded=true;
					
				}
			}
			else if(isJumpEnded)
			{
			//	Character.animation.Play("jumpEnd");
				if(Character.localPosition.y>0)
				{
					Character.Translate(Vector3.down* Time.deltaTime*6);
				//	Rope.Rotate(Vector3.left * Time.deltaTime * 20, Space.Self);
				}
				else
				{
					Character.localPosition=new Vector3(Character.localPosition.x,0f,Character.localPosition.z);
				//	Rope.localEulerAngles=new Vector3(0,Rope.localEulerAngles.y,Rope.localEulerAngles.z);
					isJumpEnded=false;
					isJumping=false;
				}
			}
			
			yield return null;
		}
		//WaterSplash.GetComponent<ParticleEmitter>().emit=true;
		WaterSplash.GetComponent<ParticleSystem>().Play(true);
	}
	
	public IEnumerator SuperJump()
	{
		isJumping=true;
		isJumpStarted=true;
		//WaterSplash.GetComponent<ParticleEmitter>().emit=false;
		WaterSplash.GetComponent<ParticleSystem>().Pause(true);
		WaterSplash.GetComponent<ParticleSystem>().Clear();
		while(isJumping)
		{
			if(isJumpStarted)
			{
				Character.GetComponent<Animation>().Play("jumpStunt");
				if(Character.localPosition.y<3f)
				{
					Character.Translate(Vector3.up * Time.deltaTime*10f);
				
					
				}
				else
				{
					isJumpStarted=false;
					isJumpEnded=true;
					
				}
			}
			else if(isJumpEnded)
			{
			//	Character.animation.Play("jumpEnd");
				if(Character.localPosition.y>0)
				{
					Character.Translate(Vector3.down* Time.deltaTime*6);
				//	Rope.Rotate(Vector3.left * Time.deltaTime * 20, Space.Self);
				}
				else
				{
					Character.localPosition=new Vector3(Character.localPosition.x,0f,Character.localPosition.z);
				//	Rope.localEulerAngles=new Vector3(0,Rope.localEulerAngles.y,Rope.localEulerAngles.z);
					isJumpEnded=false;
					isJumping=false;
				}
			}
			
			yield return null;
		}
		//WaterSplash.GetComponent<ParticleEmitter>().emit=true;
		WaterSplash.GetComponent<ParticleSystem>().Play(true);
	}
	
	public void StartSuperJump()
	{
		StartCoroutine(SuperJump());
	}
	
	
	public void FellDown()
	{
		if(!isShieldActive)
		{
		GameManager.instance.isGameover=true;
		this.GetComponent<Rigidbody>().useGravity=true;
		this.GetComponent<Rigidbody>().isKinematic=false;
		}
	}
	
	
	/// <summary>
	/// Takes the damage.
	/// </summary>
	/// <returns>
	/// The damage.
	/// </returns>
	public IEnumerator TakeDamage()
	{
		//spark.emit=true;
		yield return new WaitForSeconds(.2f);
		//spark.emit=false;
		//Character.animation.Play("crash");
		
	}
	
	public IEnumerator Crash()
	{
		Character.GetComponent<Animation>().Play("crash");
		yield return new WaitForSeconds(.5f);
		
	}
	
	

	
	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()

    {

        myTransform = transform;      

    }
	
	
	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name='other'>
	/// Other.
	/// </param>
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag=="Boost")
		{
			ConsumeBoost(other.gameObject.GetComponent<Boosts>().type);
		//	ObjectPoolManager.DestroyPooled(other.gameObject);
			Destroy(other.gameObject);
			StartCoroutine(TakeDamage());
		}
		else if(other.gameObject.tag=="Candy")
		{
			GetComponent<AudioSource>().PlayOneShot(GameManager.instance.candyCaneEffect[0]);
			//ObjectPoolManager.DestroyPooled(other.gameObject);
			 Transform instance = other.transform;
            GameManager.instance.coin.Despawn(instance); 
			StartCoroutine(TakeDamage());
			GameManager.instance.numOfCandies++;
			GameManager.instance.score+=10;
			
		}
		
		
	}
	
	
	/// <summary>
	/// Consumes the boost.
	/// </summary>
	/// <param name='types'>
	/// Types.
	/// </param>
	
	public void ConsumeBoost(BoostsTypes types)
	{
		switch(types)
		{
		
		
		case BoostsTypes.ShieldBoost:
			if(!isShieldActive)
			{
				GetComponent<AudioSource>().PlayOneShot(GameManager.instance.shieldConsume);
				GameManager.instance.BoostWarn(types);
			isShieldActive=true;
			StartCoroutine(StartShieldCounterDown());
			}
			break;
			
		}
		
	}
	
	public void ImpactSoundEffect()
	{
		
		GetComponent<AudioSource>().PlayOneShot(GameManager.instance.charImpact[Random.Range(0,2)]);
	}
	
	public IEnumerator StartShieldCounterDown()
	{
		Shield.SetActive(true);
		int counter=15;
		if(PlayerPrefs.GetInt("ShieldUpgrade",0)==1)
		{
			counter+=15;
		}
		
		while(isShieldActive)
		{
			
			yield return new WaitForSeconds(1);
			counter--;
			if(counter==0)
				isShieldActive=false;
		}
		Shield.SetActive(false);
	}
}



   

    
 
