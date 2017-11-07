using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class LevelListManager : MonoBehaviour {
	
	//public long numOfCandies;
	private int specialGifts;
	
//	public bool isAmazon=false;
//	public bool isAdNetwork;
	
	public static LevelListManager instance;
	
	public GameObject candyLabel;
	
	public GameObject popUpPanel;
	
	public GameObject popUpLevelPanel;
	
	public GameObject popInsufficient;
	
	public GameObject WorldMap;
	
	public GameObject settingPanel;
	
	public GameObject storePrefab;
	
	
	public GameObject CharUI;
	
	public GameObject LevelSelect;
	
	public GameObject StartButton;
	
	public GameObject PlayButton;
	
	
	public GameObject purchasePrefab;
	
	public GameObject centerPanel;
	
	public GameObject GiftLocks;
	
	public GameObject congratsPanel;
	
	public GameObject textDialog;
	
	public GameObject dialogBlocker;
	
	public GameObject RightArrow;
	
	public GameObject LeftArrow;
	
	public GameObject[] levelLocks;
	
	public bool isStore=false;
	
	public int currentLevel;
	
	public bool isLevelMenu=false;
	
	public bool isAdvertise=true;
	
	public InterstitialAd interstitial;

	GameObject charui;
	
//	public TweenPosition CharUI;
	
	GameObject panel;
	
	public GameObject loading;

	private string AndroidAdUnitID = "ca-app-pub-6539635200421204/3678712575";
	private string IPhoneAdUnitID = "ca-app-pub-6539635200421204/2134223775";
	
	public Purchaser purchaser;

	// Use this for initialization
	void Start () {
	//	PlayerPrefs.DeleteAll();
		
		
		instance = this;
		
		purchaser = GetComponent<Purchaser>();
		
		//PlayerPrefs.DeleteAll();
		PlayerPrefs.SetInt("levelLock1",0);
	//	PlayerPrefs.SetInt("levelLock2",0);
	//	PlayerPrefs.SetInt("levelLock3",0);
	//	PlayerPrefs.SetInt("levelLock4",0);
	//	PlayerPrefs.SetInt("levelLock5",0);
		
		
		
		MusicSetup();
		
		InitiateLevels();
		
		StartCharUI();
		
		if(PlayerPrefs.GetInt("ads",1)==0)
		{
			isAdvertise=false;
		}
		else
		{
				isAdvertise=true;
		}

		LocationService location = Input.location;

		if(isAdvertise){
			RequestInterstitial();
		}

	}
	
	void FixedUpdate()
	{
		candyLabel.GetComponent<UILabel>().text=PlayerPrefs.GetString("numOfCandies","0");
	}
	
	public void StartCharUI()
	{
		charui =  Instantiate(CharUI) as GameObject;
		charui.GetComponents<TweenPosition>()[1].eventReceiver=this.gameObject;
		charui.GetComponents<TweenPosition>()[1].callWhenFinished="StopParticles";
		charui.transform.parent = LevelListManager.instance.centerPanel.transform;
				
		charui.transform.localPosition=new Vector3(0,0,-10f);
				
		charui.transform.localScale=Vector3.one;
		
		charui.GetComponents<TweenPosition>()[0].eventReceiver=this.gameObject;
		charui.GetComponents<TweenPosition>()[0].callWhenFinished="LevelLoad";
	}
	
	public void LevelLoad()
	{
		DialogBlocker(false);
		Destroy(charui);
	}
	
	public void StopParticles()
	{
		charui.GetComponentInChildren<ParticleSystem>().Pause(true);
		charui.GetComponentInChildren<ParticleSystem>().Clear();
	}
	
	public void InitiateLevels()
	{
		for(int x=1;x<=4;x++)
		{
			if(PlayerPrefs.GetInt("levelLock"+x,1)==0)
			{
				
		//		levelLocks[x-1].GetComponentsInChildren<UISprite>()[1].enabled=false;
			}
			else
			{
				//levelLocks[x-1].GetComponent<EventManager>().buttonEvent=ButtonType.OpenPopup;
			}
		}
	}
	
	
	public void OpenStore()
	{
		if(panel!=null)
		{
			Debug.Log(isLevelMenu);
			if(!isLevelMenu)
			{
				CloseDialog();
			}
			else
			{
				LevelSelectClose();
			}
			StartCoroutine(DelayOpenStore());
		}
		else
		{
		panel = Instantiate(storePrefab) as GameObject;
		
		panel.transform.parent=LevelListManager.instance.centerPanel.transform;
		panel.transform.localPosition=new Vector3(0,0,-10f);
		panel.SetActive(true);
		DialogBlocker(true);
		panel.GetComponents<TweenScale>()[1].eventReceiver=this.gameObject;
		panel.GetComponents<TweenScale>()[1].callWhenFinished="DialogClose";
		}
	}
	
	public IEnumerator DelayPopInsufficient()
	{
		yield return new WaitForSeconds(.5f);
		panel = Instantiate(popInsufficient) as GameObject;
		
		panel.transform.parent=LevelListManager.instance.centerPanel.transform;
		panel.transform.localPosition=new Vector3(0,0,-10f);
		panel.SetActive(true);
		DialogBlocker(true);
		panel.GetComponents<TweenScale>()[1].eventReceiver=this.gameObject;
		panel.GetComponents<TweenScale>()[1].callWhenFinished="DialogClose";
	}
	
	public IEnumerator DelayOpenPurchase()
	{
		
		yield return new WaitForSeconds(.5f);

		panel = Instantiate(purchasePrefab) as GameObject;
		
		panel.transform.parent=LevelListManager.instance.centerPanel.transform;
		panel.transform.localPosition=new Vector3(0,0,-10f);

		panel.SetActive(true);

		DialogBlocker(true);

		if(!isAdvertise){
			var label = GameObject.FindWithTag("AdItemName");
			var price = GameObject.FindWithTag("AdItemPrice").GetComponent<UILabel>();
			
			

			label.GetComponent<UILabel>().text = "Ads Disabled";
			price.text = "";

			//label.GetComponent<Transform>().position = new Vector3(0,0,0);

			Debug.Log(label);
		}

		panel.GetComponents<TweenScale>()[1].eventReceiver=this.gameObject;
		panel.GetComponents<TweenScale>()[1].callWhenFinished="DialogClose";
	}
	
	public IEnumerator DelayOpenStore()
	{
		yield return new WaitForSeconds(.5f);
		panel = Instantiate(storePrefab) as GameObject;
		
		panel.transform.parent=LevelListManager.instance.centerPanel.transform;
		panel.transform.localPosition=new Vector3(0,0,-10f);
		panel.SetActive(true);
		DialogBlocker(true);
		panel.GetComponents<TweenScale>()[1].eventReceiver=this.gameObject;
		panel.GetComponents<TweenScale>()[1].callWhenFinished="DialogClose";
	}
	
	public void OpenPopInsufficient()
	{
		if(panel!=null)
		{
			if(!isLevelMenu)
				CloseDialog();
			else
				LevelSelectClose();
			StartCoroutine(DelayPopInsufficient());
		}
		else
		{
		panel = Instantiate(popInsufficient) as GameObject;
		
		panel.transform.parent=LevelListManager.instance.centerPanel.transform;
		panel.transform.localPosition=new Vector3(0,0,-10f);
		panel.SetActive(true);
		DialogBlocker(true);
		panel.GetComponents<TweenScale>()[1].eventReceiver=this.gameObject;
		panel.GetComponents<TweenScale>()[1].callWhenFinished="DialogClose";
		}
	}
	
	public IEnumerator DelayLevelOpen()
	{
		LevelSelectClose();
		
		if(isAdvertise){
			ShowInterstitialAd();
		}
		yield return new WaitForSeconds(1f);
		DialogBlocker(true);
		loading.SetActive(true);
		
		

		yield return new WaitForSeconds(1f);

		Application.LoadLevel("Level"+( PlayerPrefs.GetInt("currentLevel",1)));
	}
	
	public void delayLevelOpen()
	{
			StartCoroutine(LevelListManager.instance.DelayLevelOpen());
	}
	
	public void OpenPurchase()
	{
		if(panel!=null)
		{
			if(!isLevelMenu)
				CloseDialog();
			else
				LevelSelectClose();
			StartCoroutine(DelayOpenPurchase());
		}
		else
		{
			panel = Instantiate(purchasePrefab) as GameObject;
			
			panel.transform.parent=LevelListManager.instance.centerPanel.transform;
			panel.transform.localPosition=new Vector3(0,0,-10f);
			panel.SetActive(true);
			DialogBlocker(true);
			panel.GetComponents<TweenScale>()[1].eventReceiver=this.gameObject;
			panel.GetComponents<TweenScale>()[1].callWhenFinished="DialogClose";

		}
	}
	
	public void OpenPopUp()
	{
		if(panel!=null)
		{
			if(!isLevelMenu)
				CloseDialog();
			else
				LevelSelectClose();
			
			StartCoroutine(WaitTillPopUpClose());
		}
		else
		{
		
		panel = Instantiate(LevelListManager.instance.popUpPanel)as GameObject;
				
		DialogBlocker(true);	
				
		panel.transform.parent = LevelListManager.instance.centerPanel.transform;
				
		panel.transform.localPosition=new Vector3(0,0,-10f);
				
		panel.transform.localScale=Vector3.one;
		
		panel.GetComponents<TweenScale>()[1].eventReceiver=this.gameObject;
		panel.GetComponents<TweenScale>()[1].callWhenFinished="DialogClose";
		
		}
	}
	
	public IEnumerator WaitTillPopUpClose()
	{
		yield return new WaitForSeconds(.4f);
		panel = Instantiate(LevelListManager.instance.popUpPanel)as GameObject;
				
		DialogBlocker(true);	
				
		panel.transform.parent = LevelListManager.instance.centerPanel.transform;
				
		panel.transform.localPosition=new Vector3(0,0,-10f);
				
		panel.transform.localScale=Vector3.one;
		
		panel.GetComponents<TweenScale>()[1].eventReceiver=this.gameObject;
		panel.GetComponents<TweenScale>()[1].callWhenFinished="DialogClose";
		
	}
	
	
	
	public void OpenLevelPopUp()
	{
		isLevelMenu=true;
		charui.GetComponentInChildren<ParticleSystem>().Play(true);
		
		
		charui.GetComponents<TweenPosition>()[0].enabled=true;
		
		panel = Instantiate(LevelSelect)as GameObject;
				
		DialogBlocker(true);	
				
		panel.transform.parent = LevelListManager.instance.centerPanel.transform;
				
		panel.transform.localPosition=new Vector3(1000,0,0);
				
		panel.transform.localScale=Vector3.one;
		
		StartButton.SetActive(false);
		PlayButton.SetActive(true);
		
		
		panel.GetComponents<TweenPosition>()[0].enabled=true;
		
		panel.GetComponents<TweenPosition>()[1].eventReceiver=this.gameObject;
		panel.GetComponents<TweenPosition>()[1].callWhenFinished="DialogClose";
		
		
	}
	
	public IEnumerator WaitTillSettings()
	{
		yield return new WaitForSeconds(.4f);
		panel = Instantiate(LevelListManager.instance.settingPanel)as GameObject;
				
		DialogBlocker(true);	
				
		panel.transform.parent = LevelListManager.instance.centerPanel.transform;
				
		panel.transform.localPosition=new Vector3(0,0,-10f);
				
		panel.transform.localScale=Vector3.one;
		
		panel.GetComponents<TweenScale>()[1].eventReceiver=this.gameObject;
		panel.GetComponents<TweenScale>()[1].callWhenFinished="DialogClose";
		
		if(PlayerPrefs.GetInt("isSound",1)==1)
		{
			panel.GetComponentsInChildren<UICheckbox>()[0].startsChecked=true;
		}
		else
		{
			panel.GetComponentsInChildren<UICheckbox>()[0].startsChecked=false;
		}	
			
		if(PlayerPrefs.GetInt("isMusic",1)==1)
		{
			panel.GetComponentsInChildren<UICheckbox>()[1].startsChecked=true;
		}
		else
		{
			panel.GetComponentsInChildren<UICheckbox>()[1].startsChecked=false;
		}
		
	}
	
	public void OpenSettings()
	{
		if(panel!=null)
		{
			if(!isLevelMenu)
				CloseDialog();
			else
				LevelSelectClose();
			
			StartCoroutine(WaitTillSettings());
		}
		else
		{
		
		
		
		panel = Instantiate(LevelListManager.instance.settingPanel)as GameObject;
				
		DialogBlocker(true);	
				
		panel.transform.parent = LevelListManager.instance.centerPanel.transform;
				
		panel.transform.localPosition=new Vector3(0,0,-10f);
				
		panel.transform.localScale=Vector3.one;
		
		panel.GetComponents<TweenScale>()[1].eventReceiver=this.gameObject;
		panel.GetComponents<TweenScale>()[1].callWhenFinished="DialogClose";
		
		if(PlayerPrefs.GetInt("isSound",1)==1)
		{
			panel.GetComponentsInChildren<UICheckbox>()[0].startsChecked=true;
		}
		else
		{
			panel.GetComponentsInChildren<UICheckbox>()[0].startsChecked=false;
		}	
			
		if(PlayerPrefs.GetInt("isMusic",1)==1)
		{
			panel.GetComponentsInChildren<UICheckbox>()[1].startsChecked=true;
		}
		else
		{
			panel.GetComponentsInChildren<UICheckbox>()[1].startsChecked=false;
		}
		}
	}
	

	
	public void DialogBlocker(bool istrue)
	{
		dialogBlocker.SetActive(istrue);
	}
	
	public void CloseDialog()
	{
		panel.GetComponents<TweenScale>()[1].enabled=true;
			
	}
	
	public void LevelSelectClose()
	{
		
		DialogBlocker(true);
		
		panel.GetComponents<TweenPosition>()[1].enabled=true;
			isLevelMenu=false;
		
		StartButton.SetActive(true);
		PlayButton.SetActive(false);
		StartCharUI();
	}
	
	public void DialogClose()
	{
		
		
		Destroy(panel);
		DialogBlocker(false);
	}
	
	public void ToggleMusic(bool val)
	{
		if(val)
			{
				PlayerPrefs.SetInt("isMusic",1);
				
			}
			else
			{
				PlayerPrefs.SetInt("isMusic",0);
			}
		
		MusicSetup();
	}
	
	public void MusicSetup()
	{
		
		if(PlayerPrefs.GetInt("isMusic",1)==1)
		{
			GetComponent<AudioSource>().enabled=true;
		}
		else
		{
			GetComponent<AudioSource>().enabled=false;
		}
		
	}
	
	public void IncreaseLevel()
	{
		int val = PlayerPrefs.GetInt("currentLevel",1);
		PlayerPrefs.SetInt("currentLevel",(++val));
		
		LevelSelectUI.instance.ChangeLevel(val);
		
		
	}
	
	
	
	public void DecreaseLevel()
	{
		
		int val = PlayerPrefs.GetInt("currentLevel",1);
		
		
		PlayerPrefs.SetInt("currentLevel",(--val));
		
		LevelSelectUI.instance.ChangeLevel(val);
		
		
		
	}

	private void RequestInterstitial()
	{
	    #if UNITY_ANDROID
	        string adUnitId = AndroidAdUnitID;
	    #elif UNITY_IPHONE
	        string adUnitId = IPhoneAdUnitID;
	    #else
	        string adUnitId = "unexpected_platform";
	    #endif

	    // Initialize an InterstitialAd.
	    interstitial = new InterstitialAd(adUnitId);
	    // Create an empty ad request.
	    AdRequest request = new AdRequest.Builder().AddTestDevice("897864A56D1153188070EC5A2D56F57D").AddTestDevice("48A7C1F024F41188265994DD6D115D07").Build();
	    // Load the interstitial with the request.
	    interstitial.LoadAd(request);
	}

	private void ShowInterstitialAd(){
		
		if(interstitial != null){
			if (interstitial.IsLoaded()) {
				interstitial.Show();
			}
		}else{
			RequestInterstitial();
		}
	}
}
