using UnityEngine;
using System.Collections;

public class LevelSelectUI : MonoBehaviour {
	
	public static LevelSelectUI instance;
	
	public GameObject arrowLeft;
	
	public GameObject arrowRight;
	
	public EventManager[] playLevel;
	
	public UILabel levelTitle;
	
	public UISprite levelSprite;
	
	public UILabel score;
	
	public UISprite Lock;
	
	void Start()
	{
		PlayerPrefs.SetInt("currentLevel",1);
		instance= this;
		ChangeLevel(1);
	}
	
	public void ChangeScore(int val)
	{
		
		score.text = PlayerPrefs.GetString("levelScore"+(val),"0");
		
	}
	
	public void ChangeLevel(int val)
	{
		ChangeScore(val);
		if(PlayerPrefs.GetInt("levelLock"+val,1)==1)
		{
			levelSprite.color=Color.grey;
			Lock.enabled=true;
			playLevel[0].buttonEvent=ButtonType.OpenPopup;
			LevelListManager.instance.PlayButton.GetComponent<EventManager>().buttonEvent=ButtonType.OpenPopup;
		}
		else
		{
			levelSprite.color=Color.white;
			Lock.enabled=false;
			playLevel[0].buttonEvent=ButtonType.PlayLevel;
			LevelListManager.instance.PlayButton.GetComponent<EventManager>().buttonEvent=ButtonType.PlayLevel;
		}
		
		
		switch(val)
		{
			
		case 1:
			levelTitle.text="Ocean";
			levelSprite.spriteName="ocean256";
			arrowLeft.SetActive(false);
			arrowRight.SetActive(true);
			
			break;
		case 2:
			levelTitle.text="River";
			levelSprite.spriteName="river256";
			arrowLeft.SetActive(true);
			arrowRight.SetActive(true);
			
			break;
		case 3:
			levelTitle.text="Lake";
			levelSprite.spriteName="lake256";
			arrowLeft.SetActive(true);
			arrowRight.SetActive(true);
			
			break;
		case 4:
			levelTitle.text="Jungle";
			levelSprite.spriteName="jungle256";
			arrowLeft.SetActive(true);
			arrowRight.SetActive(true);
		
			break;
			
		case 5:
			levelTitle.text="Swamp";
			levelSprite.spriteName="swamp256";
			arrowLeft.SetActive(true);
			arrowRight.SetActive(false);
		
			break;
			
		}
	}
}
