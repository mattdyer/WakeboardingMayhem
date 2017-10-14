using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//All Button Enumeration Are saved here..
public enum ButtonType{
	None=0,
	Restart=1,
	ThrowGiftLeft=2,
	ThrowGiftRight=3,
	Continue=4,
	End=5,
	Pause=6,
	PlayLevel=7,
	Exit=14,
	SoundToggle=15,
	LoadLevelList=16,
	candyContinue=17,
	StoreOpen=18,
	StoreClose=19,
	BuyRiver=20,
	BuySwamp=21,
	BuyJungle=22,
	BuySleigh=23,
	GiftClick=24,
	DialogClose=25,
	Settings=26,
	ToggleTutorial=28,
	OpenCoinPurchase=29,
	CoinPackage1=30,
	OpenPopup=31,
	SwitchLevels=32,
	SwitchBoards=33,
	CloseLevelList=34,
	MusicToggle=35,
	IncreaseLevel=36,
	DecreaseLevel=37,
	CoinPackage2=38,
	CoinPackage3=39,
	Buylake=40,
	SelectRight=41,
	SelectLeft=42,
	BuyBoard=43,
	FacebookShare=44,
	DisableAds=45
}

//Gift Delievered Type enumerations
public enum GiftHitType{
	Missed=0,
	Hit=1,
	ChimneyHit=2
}


//Boosts Type Enumerations
public enum BoostsTypes{
	HealthBoost=0,
	ShieldBoost=1,
	GrinchAwayBoost=2
}


//Level Trophy in The End
public enum LevelMedal{
	None=0,
	Bronze=1,
	Silver=2,
	Gold=3
	
}

//Game Manager Script
public class GameManager : MonoBehaviour {
	
	//self instance to control and spread logic..
	public static GameManager instance;
	
	//Player Script Instance...
	public static Player player;
	
	
	
	//Enemies Script Instance
//	public Enemy currentEnemy;
	
	//In Game Music Object to control Music..
	public GameObject MusicBox;
	
	//Grinch Music  Box...
	public GameObject GrinchMusicBox;
	
	//if Game is Over ...
	public bool isGameover=false;
	
	//If Game is Paused...
	public bool isGamePaused=false;
	

	//Player Prefab attachs here so that payer may be respawned at any moment
	public GameObject PlayerPrefab;
	
	//Grinch Prefab on which player would be followed by grinch who would try to atack Santa
	public GameObject GrinchPrefab;
	
	public GameObject[] animePrefab;
	
	public GameObject[] islandPrefab;
	
	//Enemy Prefab to handle Enemy Spawn on the runner field 
	public GameObject[] EnemyPrefab;
	
	public float[] enemyPositions;
	
	//Game Over panel to show and hide during gameplay
	public GameObject gameOverPanel;
	
	public GameObject TimeLabel;
	
	
	public Transform Ramp;
	
	public GameObject parentWarning;

	
	public GameObject ShieldWarning;
	
	public GameObject HealthWarning;
	
	public GameObject FartWarning;

	public GameObject pauseGamePanelPrefab;
	
	private GameObject PauseGamePanel;
	
	
	public GameObject Scoreslabels;
	
	//All three types of Boosts...
	
	//Health Boost Type..
	public GameObject[] BoostTypes;
	
	public Transform currentEnemy;
	
	//Flag for checking if grinch is chasing..
//	public bool isGrinchChasing=false;
	

	//Speed to set the game movement speed..
	public float speed=1f;
	

	public GameObject healthBar;
	

	private GameObject boostWarn;
	
	public bool isGiftThrownOnce=false;
	
	public int totalTime=180;
	private int totalTimeSec=0;
	private int totalTimeMin=3;
	
	private int houseSpeedCount=0;
	
	public GameObject levelLabel;
	
	
	public long numOfCandies=0;
	
	public int candyCaneCount=0;
	public int totalCandyCount=0;
	public int currentCandyPosition;
	
	public int enemyCount=0;
	public int totalEnemies=0;
	//public int enemiesPosition;
	
	public Transform currentCandy;
	public GameObject candyLabel;
	
	public Transform CandyCanes;
	public bool isCandyCanes;
	
	public int numOfHouses;
	
	public GameObject timerProgress;

	#region Sounds
	//***** SOUNDS *******//
	public AudioClip[] candyCaneEffect;
	
	public AudioClip[] charImpact;
	
	public AudioClip houseMiss;
	
	public AudioClip shieldConsume;
	
	public AudioClip houseHit;
	
	public AudioClip health;
	
	public AudioClip grinchGetAway;
	
	public AudioClip[] LevelMusics;
	
	
	
	//************//
	#endregion
	
	public float speedTimer;
	public float scoreIncTimer;

	public long score;

	public int currentLevel;
	
	public GameObject[] Roads;
	
	#region POOL
	public string poolName;
	
	public SpawnPool coin;
	
	
	public bool isSidesLocked=false;
	
	public bool isTunnel=false;
	
	#endregion
	
	public Boat thisBoat;
	// Use this for initialization
	void Start () {
		
		//Instance to set static variable  to self singleton Instance..
		
		
		instance = this;
		//currentLevel = PlayerPrefs.GetInt("currentLevel",1);
	//	speed+=currentLevel*2;
	//	levelLabel.GetComponent<UILabel>().text=LevelListManager.GetLevelname(currentLevel);
		//Enemy Position predefined...
		enemyPositions = new float[3] {-1.5f,0,1.5f};
		
		coin = PoolManager.Pools[this.poolName];
		
		 MusicSetup();
		SpawnRandomEnemy();
		
//		if(PlayerPrefs.GetInt("HealthUpgrade",0)>0)
//		{
//			player.IncreaseHealth(PlayerPrefs.GetInt("HealthUpgrade",0));
//			PlayerPrefs.SetInt("HealthUpgrade",0);
	//	}
		//PlayerPrefs.SetInt("TutorMode",1);
//		if(PlayerPrefs.GetInt("TutorMode",1)==1&&currentLevel==1)
	//	{
	//		isTutorialMode=true;
	//		TutorialObject.GetComponent<TutorialManager>().enabled=true;
	//		PlayerPrefs.SetInt("TutorMode",0);
	//	}
		
		
	}
	
	public void MusicSetup()
	{
		
		if(PlayerPrefs.GetInt("isMusic",1)==1)
		{
			MusicBox.SetActive(true);
		}
		else
		{
			MusicBox.SetActive(false);
		}
		
		
	}
	
	public IEnumerator RestartEnemySequence()
	{
		
		
		yield return new WaitForSeconds(4f);
		
		currentEnemy=null;
		
	}
	
	public void InitiateEnemySequence()
	{
	//	yield return new WaitForSeconds(4f);
		
		
		enemyCount = Random.Range(3,8);
		
		
		totalEnemies = enemyCount;
		
	//	currentEnemyPosition = Random.Range(0,3);
		
		SpawnRandomEnemy();
		
		
	//	GenerateCandyCane(currentCandyPosition);
	}
	
	
	/// <summary>
	/// Initiates the candy canes.
	/// </summary>
	public void InitiateCandyCanes()
	{
		candyCaneCount = Random.Range(6,13);
		totalCandyCount = candyCaneCount;
		
		currentCandyPosition = Random.Range(0,3);
		
		if(isTunnel)
			currentCandyPosition=1;
		
		GenerateCandyCane(currentCandyPosition);
	}
	
	/// <summary>
	/// Generates the candy cane.
	/// </summary>
	/// <param name='val'>
	/// Value.
	/// </param>
	public void GenerateCandyCane(int val)
	{
		
			
			currentCandy = this.coin.Spawn(this.CandyCanes);
		//,new Vector3(enemyPositions[val],0.5f,50f),Quaternion.identity);//ObjectPoolManager.CreatePooled(CandyCanes,new Vector3(enemyPositions[val],0.5f,50f),Quaternion.identity) as GameObject;
			currentCandy.localPosition=new Vector3(enemyPositions[val],0.5f,90f);
		
		
			if(currentEnemy.GetComponent<Enemy>().enemyID>0&&currentEnemy.localPosition.z>86f&&currentEnemy.localPosition.z<90f)
			{
				currentCandy.localPosition=new Vector3(currentCandy.localPosition.x,1.5f,currentCandy.localPosition.z);
			}
			
		
		
			
			currentCandy.GetComponent<CandyCanes>().isMoving=true;
		
		
			candyCaneCount--;
		
		if(candyCaneCount==0)
			StartCoroutine(RestartCoinSequece());
			
		
	}
	
	public void GenerateCandyCane(Vector3 pos)
	{
		
			
			currentCandy = this.coin.Spawn(this.CandyCanes);//,new Vector3(enemyPositions[val],0.5f,50f),Quaternion.identity);//ObjectPoolManager.CreatePooled(CandyCanes,new Vector3(enemyPositions[val],0.5f,50f),Quaternion.identity) as GameObject;
		
			currentCandy.localPosition=pos;
			currentCandy.GetComponent<CandyCanes>().isMoving=true;
			
		
	}
	
/*	public void GenerateEnemy()
	{
		int val = Random.Range(0,3);
			
		
		currentCandy = this.coin.Spawn(EnemyPrefab[val]);
		
		
	
		currentCandy.localPosition=new Vector3(enemyPositions[val],0.5f,50f);
		
		
		currentCandy.GetComponent<CandyCanes>().isMoving=true;
		
		candyCaneCount--;
		
		if(candyCaneCount==0)
			StartCoroutine(RestartCoinSequece());
			
		
	}*/
	
	public void SpawnRandomEnemy()
	{
		//Enemy instantiated on the runner field...
		int val =0;
		int Chance = Random.Range(0,110);
		if(Chance<60)
		{
			val=0;
			
		}
		else if(Chance<88)
			val=1;
		else if(Chance<100)
			val=2;
		else if(Chance<110)
			val=3;
		
		currentEnemy = this.coin.Spawn(EnemyPrefab[val].transform);
		
		
		if(val==0)
		{
			currentEnemy.localPosition= new Vector3(enemyPositions[Random.Range(0,3)],.5f,90);
		}
		else if(val==3)
		{
			currentEnemy.localPosition= new Vector3(0,0,90);
		}
		else
		{
			
			currentEnemy.localPosition= new Vector3(0,.5f,90);
		}
		currentEnemy.GetComponent<Enemy>().enemyID=val;
		currentEnemy.GetComponent<Enemy>().isMoving=true;

		enemyCount--;
	}
	
	public void SpawnRamp()
	{
		//Enemy instantiated on the runner field...
		
		GameObject cc = Instantiate(Ramp.gameObject) as GameObject;
		
		
		
		cc.transform.localPosition= new Vector3(enemyPositions[Random.Range(0,3)],0,(currentEnemy.localPosition.z-5));
		
		for(int x=2;x<5;x++)
		GenerateCandyCane(new Vector3(cc.transform.localPosition.x,(cc.transform.localPosition.y+x),
			(cc.transform.localPosition.z+x)));
		
	
	}
	
	public IEnumerator RestartCoinSequece()
	{
		yield return new WaitForSeconds(3f);
		currentCandy=null;
	}
	

	// Update is called once per frame
	void Update () {
		
		
		if(!isGameover&&!(GameManager.instance.isGamePaused))
		{
		
			speedTimer+=Time.deltaTime;
			scoreIncTimer+=Time.deltaTime;
//			Debug.Log(scoreIncTimer);
		if(speedTimer>25f)
			{
				
				speed++;
				speedTimer=0;
				int val=Random.Range(2,4);
				
				
				if(currentLevel==1&&currentLevel==3)
					val=Random.Range(0,4);
				
				//if(currentLevel<5)
					islandPrefab[val].GetComponent<MovableSpawner>().isMoving=true;
				
				
				if(val==2||val==3)
				{
					isTunnel=true;
					
					thisBoat.AlignCenter();
					
				}
				
			}
			
			
		if(scoreIncTimer>4f)
			{
				score+=(2*(long)speed);
				scoreIncTimer=0;
			}
			
		foreach(GameObject obj in Roads)
		{
		
  	        obj.transform.Translate(Vector3.back * Time.deltaTime*speed);
			
        		if(obj.transform.localPosition.z<-70)
				{
					
					
				
					obj.transform.localPosition=new Vector3(0,0,obj.transform.localPosition.z+175f);
					
					
				}
     		
			}
		}
		
	}
	
	
	/// <summary>
	/// Pauses the game.
	/// </summary>
	public void PauseGame()
	{
		
		if(!isGamePaused)
		{
		isGamePaused=true;
		
		PauseGamePanel = Instantiate(pauseGamePanelPrefab) as GameObject;
		
		PauseGamePanel.transform.parent=parentWarning.transform;
			
		PauseGamePanel.transform.localPosition=Vector3.zero;
		PauseGamePanel.transform.localScale=new Vector3(.1f,.1f,.1f);
			
		}
	}
	
	/// <summary>
	/// Whens the resumed.
	/// </summary>
	void whenResumed()
	{
		Destroy(PauseGamePanel);
		isGamePaused=false;
	}
	
	/// <summary>
	/// Resumes the game.
	/// </summary>
	public void ResumeGame()
	{

		TweenScale temp =PauseGamePanel.GetComponents<TweenScale>()[1];
		
		temp.eventReceiver=this.gameObject;
		
		temp.callWhenFinished="whenResumed";
		
		temp.enabled=true;
		
	}
	/// <summary>
	/// Continues the game.
	/// </summary>
	public void ContinueGame()
	{
		isGameover=false;
		gameOverPanel.SetActive(false);
		
		//player.IncreaseHealth(3);
	}
	
	
	
	
	/// <summary>
	/// Fixeds the update.
	/// </summary>
	void FixedUpdate()
	{
		if(!isGameover&&!(isGamePaused))
		{
			
			
		if(currentCandy!=null)
			{
				
				if(currentCandy.transform.position.z<85f&&(candyCaneCount>0))
				{
					isCandyCanes=true;
					int candyTurn = Random.Range(0,11);
					
					if(currentEnemy.localPosition.x==enemyPositions[currentCandyPosition]&&currentEnemy.GetComponent<Enemy>().enemyID==0)
					{
						if(currentCandyPosition==0)
							currentCandyPosition=1;
						else if(currentCandyPosition==1)
							currentCandyPosition=Random.Range(1,3);
						else if(currentCandyPosition==2)
							currentCandyPosition=1;
					}
					else if(candyTurn<3)
					{
						
						if(currentCandyPosition==0)
							currentCandyPosition=1;
						else if(currentCandyPosition==1)
							currentCandyPosition=Random.Range(1,3);
						else if(currentCandyPosition==2)
							currentCandyPosition=1;
						
					}
					
					if(isTunnel)
						currentCandyPosition=1;
					
					GenerateCandyCane(currentCandyPosition);
					
				}
			}
			else
			{
				isCandyCanes=false;
				InitiateCandyCanes();
				
			}
				
	//	if(currentEnemy!=null)
		//	{
			float rndSpace = speed+35;
				
				if(currentEnemy.transform.position.z<rndSpace)
				{
				//	Debug.Log("hitted");
				
					if(!isTunnel)
					SpawnRandomEnemy();
				//	GenerateCandyCane(currentCandyPosition);
				//	enemyCount--;
				}
				
			//if(enemyCount==0);
		//			StartCoroutine(RestartEnemySequence());
		//	}
	//		else
	//		{
			//	isCandyCanes=false;
		//		InitiateEnemySequence();
				
		//	}
			
			
			if(!GameObject.FindGameObjectWithTag("Ramp")&&!(isCandyCanes))
			{
				if(!isTunnel)
				 	SpawnRamp();
				
			
			}
			
		if(!GameObject.FindGameObjectWithTag("Boost"))
			{
				if(!isTunnel)
					SpawnRandomBoost();
				
			//	if(currentLevel==1)
					SpawnRandomDolphins();
			}
			


			
//		GiftLabel.GetComponent<UILabel>().text=numOfGiftsSent.ToString();
		candyLabel.GetComponent<UILabel>().text="x " + numOfCandies.ToString();
			Scoreslabels.GetComponent<UILabel>().text= score.ToString();
			
			
		}
	}
	
	
	
	/// <summary>
	/// Boosts the warn.
	/// </summary>
	/// <param name='type'>
	/// Type.
	/// </param>
	public void BoostWarn(BoostsTypes type)
	{
		switch(type)
		{
		case BoostsTypes.GrinchAwayBoost:
				boostWarn = Instantiate(FartWarning) as GameObject;
			break;
		case BoostsTypes.ShieldBoost:
				boostWarn = Instantiate(ShieldWarning) as GameObject;
			break;
		case BoostsTypes.HealthBoost:
				boostWarn = Instantiate(HealthWarning) as GameObject;
			break;
			
		}
		
		boostWarn.transform.parent= parentWarning.transform;
					
		boostWarn.GetComponent<TweenScale>().callWhenFinished="Boosted";
					
		boostWarn.GetComponent<TweenScale>().eventReceiver=this.gameObject;
					
		boostWarn.transform.localScale=new Vector3(.5f,.5f,.5f);
					
		boostWarn.transform.localPosition=Vector3.zero;
					
		boostWarn.SetActive(true);
		
	}
	
	/// <summary>
	/// Boosted this instance.
	/// </summary>
	public void Boosted()
	{
		Destroy(boostWarn);
	}
	
	
	/// <summary>
	/// Spawns the random boost.
	/// </summary>
	public void SpawnRandomBoost()
	{
		int valu = Random.Range(0,2000);
		
		if(valu==1000)
		{
			
		
			GameObject enemy = Instantiate(BoostTypes[0]) as GameObject;
		
			
			enemy.transform.localPosition= new Vector3(enemyPositions[Random.Range(0,3)],.5f,90);
			
				
			enemy.GetComponent<Boosts>().isMoving=true;
		}
		
	}
	
	/// <summary>
	/// Spawns the random boost.
	/// </summary>
	public void SpawnRandomDolphins()
	{
		int valu = Random.Range(0,1000);
		
		if(valu==50)
		{
			int rnd = Random.Range(0,2);
		
			GameObject enemy = Instantiate(animePrefab[rnd]) as GameObject;
			
			float[] loc= new float[2]{-5f,5f};
			
			enemy.transform.localPosition= new Vector3(loc[rnd],0,90);
			
				
			enemy.GetComponent<MovableSpawner>().isMoving=true;
		}
		
	}
	
	
	
	/// <summary>
	/// Games the end.
	/// </summary>
	public void GameEnd()
	{
		long oldCandies=long.Parse(PlayerPrefs.GetString("numOfCandies","0"));
	
		oldCandies+=numOfCandies;
		
		PlayerPrefs.SetString("numOfCandies",oldCandies.ToString());
		
		long val = long.Parse(PlayerPrefs.GetString("levelScore"+(PlayerPrefs.GetInt("currentLevel",1)),"0"));
		
		if(val<score)
			PlayerPrefs.SetString("levelScore"+(PlayerPrefs.GetInt("currentLevel",1)),score.ToString());
		
	}
	/// <summary>
	/// Houses the hit effect.
	/// </summary>
	public void houseHitEffect()
	{
		GetComponent<AudioSource>().PlayOneShot(houseHit);
	}
	

}
