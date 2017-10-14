using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class EventManager : MonoBehaviour {
	
	public ButtonType buttonEvent;
	
	public int giftNo=0;
	
	void start(){
		if(buttonEvent==ButtonType.candyContinue)
		{
			this.gameObject.GetComponentInChildren<UILabel>().text="Continue : 500";
			this.gameObject.GetComponentInChildren<UILabel>().color=Color.black;
		}

		
	}
	
	void OnClick()
	{
		switch(buttonEvent)
		{
		
		case ButtonType.None:
			Debug.Log("Clicked");
			break;
		case ButtonType.Restart:
			GameManager.instance.GameEnd();
			Application.LoadLevel("Level"+PlayerPrefs.GetInt("currentLevel",1));
			break;
		case ButtonType.Continue:
				GameManager.instance.ResumeGame();
			break;
		case ButtonType.End:
			GameManager.instance.GameEnd();
				Application.LoadLevel("LevelList");
			break;
		case ButtonType.Pause:
			GameManager.instance.PauseGame();
			break;
		case ButtonType.Exit:
			GameManager.instance.GameEnd();
			Application.Quit();
			break;
		
		case ButtonType.LoadLevelList:
			
			LevelListManager.instance.OpenLevelPopUp();
			//Application.LoadLevel("LevelList");
			break;

		case ButtonType.candyContinue:
			
			long oldCandies=long.Parse(PlayerPrefs.GetString("numOfCandies","0"));
			
			if((oldCandies)>=500)
			{
				
				oldCandies-=500;
				PlayerPrefs.SetString("numOfCandies",oldCandies.ToString());
				GameManager.instance.ContinueGame();
				
			}
			else if((oldCandies+GameManager.instance.numOfCandies)>=500)
			{
				oldCandies=500-oldCandies;
				PlayerPrefs.SetString("numOfCandies","0");
			   GameManager.instance.numOfCandies-=oldCandies;
				
				
				GameManager.instance.ContinueGame();
				
				
				
				
			}
			else
			{
				this.gameObject.GetComponentInChildren<UILabel>().text="Insufficient";
				this.gameObject.GetComponentInChildren<UILabel>().color=Color.red;
			}
			break;
			
		case ButtonType.BuyRiver:
			
				if(PlayerPrefs.GetInt("levelLock2",1)==1)
			{
			if(Store.instance.ReduceCandies(5000))
			{
				
				PlayerPrefs.SetInt("levelLock2",0);
			//	Store.instance.InitiateStore();
			//	LevelListManager.instance.InitiateLevels();
			}
			else
			{
				LevelListManager.instance.OpenPopInsufficient();
			}
			}
			
			
			break;
		case ButtonType.BuySwamp:
			if(PlayerPrefs.GetInt("levelLock5",1)==1)
			{
				if(Store.instance.ReduceCandies(5000))
				{
				
					PlayerPrefs.SetInt("levelLock5",0);
					Store.instance.InitiateStore();
					LevelListManager.instance.InitiateLevels();
				}
				else
				{
					LevelListManager.instance.OpenPopInsufficient();
				}
			}
			break;
		case ButtonType.BuyJungle:
				if(PlayerPrefs.GetInt("levelLock4",1)==1)
			{
			if(Store.instance.ReduceCandies(5000))
			{
				
				PlayerPrefs.SetInt("levelLock4",0);
				Store.instance.InitiateStore();
				LevelListManager.instance.InitiateLevels();
			}
			else
			{
				LevelListManager.instance.OpenPopInsufficient();
			}
			}
			break;
		case ButtonType.Buylake:
			if(PlayerPrefs.GetInt("levelLock3",1)==1)
			{
			if(Store.instance.ReduceCandies(5000))
			{
				
				PlayerPrefs.SetInt("levelLock3",0);
				Store.instance.InitiateStore();
				LevelListManager.instance.InitiateLevels();
			}
			else
			{
				LevelListManager.instance.OpenPopInsufficient();
			}
			}
			break;
		case ButtonType.StoreOpen:
			LevelListManager.instance.OpenStore();
			break;
			
		case ButtonType.OpenPopup:
			LevelListManager.instance.OpenPopUp();
			break;
			
			
		case ButtonType.StoreClose:
			
				LevelListManager.instance.CloseDialog();
//	com.soomla.unity.StoreInventory.BuyItem(CandyStoreInfo.VirtualCurrencyPacks[0].ItemId);
			break;
		case ButtonType.GiftClick:
			if(PlayerPrefs.GetInt("SpecialGift"+giftNo,0)==3)
			{
					Debug.Log("Gift is UnLocked");
			}
			else
			{
				
				LevelListManager.instance.OpenPopUp();
			}
			break;
			
		case ButtonType.PlayLevel:
			
			LevelListManager.instance.delayLevelOpen();
			
			break;
			
			
	
		case ButtonType.DialogClose:
				LevelListManager.instance.CloseDialog();
			break;
		case ButtonType.CloseLevelList:
			
			LevelListManager.instance.LevelSelectClose();
			
			break;
			
		case ButtonType.Settings:
				LevelListManager.instance.OpenSettings();
			break;
			
		case ButtonType.OpenCoinPurchase:
			LevelListManager.instance.OpenPurchase();
			break;
			
		case ButtonType.CoinPackage1:
			
			LevelListManager.instance.purchaser.BuyProduct("Coins2000");
			
			break;
			
		case ButtonType.CoinPackage2:
			
			LevelListManager.instance.purchaser.BuyProduct("Coins4000");
			
			break;
			
		case ButtonType.CoinPackage3:
			
			LevelListManager.instance.purchaser.BuyProduct("Coins10000");
			
			break;
			
		case ButtonType.DisableAds:
			
			if(LevelListManager.instance.isAdvertise){
				LevelListManager.instance.purchaser.BuyProduct("NoAds");
			}

			break;
			
		case ButtonType.ToggleTutorial:
				if(PlayerPrefs.GetInt("TutorMode",1)==1)
				{
					PlayerPrefs.SetInt("TutorMode",0);
			this.gameObject.GetComponent<UICheckbox>().startsChecked=false;
				}
				else
			{
				PlayerPrefs.SetInt("TutorMode",1);
				this.gameObject.GetComponent<UICheckbox>().startsChecked=true;
			}
			break;
			
		case ButtonType.SwitchBoards:
			Store.instance.openBoardsShop();
			
			break;
			
		case ButtonType.SwitchLevels:
			Store.instance.openLevelsShop();
			
			break;
			
		case ButtonType.SoundToggle:
			if(this.GetComponent<UICheckbox>().isChecked)
			{
				
			}
			else
			{
				
			}
			break;
		case ButtonType.MusicToggle:
			LevelListManager.instance.ToggleMusic(this.GetComponent<UICheckbox>().isChecked);
		
			break;
			
		case ButtonType.IncreaseLevel:
			LevelListManager.instance.IncreaseLevel();
			break;
		case ButtonType.DecreaseLevel:
			LevelListManager.instance.DecreaseLevel();
			break;
			
		case ButtonType.SelectRight:
			
			Store.instance.RightBoard();
			
			break;
			
		case ButtonType.SelectLeft:
			
			Store.instance.LeftBoard();
			
			break;
			
		case ButtonType.BuyBoard:
			
			Store.instance.BuyAndEquipBoard();
			
			break;
			
		case ButtonType.FacebookShare:
			//FacebookComboUI.instance.ShareScore();
//			FacebookShare.instance.ShareScores();
			
			break;
		}
		
	}


}
