using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Store : MonoBehaviour {
	
	public static Store instance;
	
	public Transform Board;
	
	
	public Material[] boardsMat;
	
	public Mesh[] boardMesh;
	
	public GameObject RightButton;
	
	public GameObject LeftButton;
	
	public GameObject buyButton;
	
	
	public UILabel RiverCostLabel;
	
	public UILabel SwampCostLabel;
	
	public UILabel JungleCostLabel;
	
	public UILabel LakeCostLabel;
	
	public GameObject buyBoards;
	
	public GameObject buyLevels;
	
	public BoxCollider buyBoardBtn;
	
	public BoxCollider buyLevelBtn;
	
	private int currentBoard=1;
	
	
	// Use this for initialization
	void Start () {
		instance =this;
		InitiateStore();
		
	}
	
	public void openLevelsShop()
	{
		buyLevels.SetActive(true);
		buyBoards.SetActive(false);
		buyBoardBtn.enabled=true;
		buyLevelBtn.enabled=false;
	}
	
	public void openBoardsShop()
	{
		buyLevels.SetActive(false);
		buyBoards.SetActive(true);
		
		buyBoardBtn.enabled=false;
		buyLevelBtn.enabled=true;
		currentBoard=1;
		PlayerPrefs.SetInt("board1",1);
		ChangeBoard(currentBoard);
		LeftButton.SetActive(false);
		RightButton.SetActive(true);
		
		
	}
	
	public void ChangeBoard(int val)
	{
		
		Board.GetComponent<Renderer>().material= boardsMat[(val-1)];
		if(PlayerPrefs.GetInt("board"+val,0)==1)
		{
			buyButton.GetComponentInChildren<UILabel>().text="Equip";
			
			if(PlayerPrefs.GetInt("currentBoard",1)==val)
			{
				buyButton.GetComponentInChildren<UILabel>().text="Equiped";
			}
			
		}
		else
		{
			buyButton.GetComponentInChildren<UILabel>().text="Buy : 4000";
		}
		
	}
	
	public void BuyAndEquipBoard()
	{
		if(PlayerPrefs.GetInt("board"+currentBoard,0)==0)
		{
		if(ReduceCandies(4000))
		{
			PlayerPrefs.SetInt("board"+currentBoard,1);
			PlayerPrefs.SetInt("currentBoard",currentBoard);
			buyButton.GetComponentInChildren<UILabel>().text="Equiped";
		}
		else
		{
			LevelListManager.instance.OpenPopInsufficient();
		}
		}
		else
		{
			PlayerPrefs.SetInt("currentBoard",currentBoard);
			buyButton.GetComponentInChildren<UILabel>().text="Equiped";
		}
	}
	
	public void RightBoard()
	{
		if(currentBoard<3)
		{
			ChangeBoard(++currentBoard);
			LeftButton.SetActive(true);
		}
		
		if(currentBoard==3)
			RightButton.SetActive(false);
	}
	
	public void LeftBoard()
	{
		
		
		if(currentBoard>1)
		{
			ChangeBoard(--currentBoard);
			
			RightButton.SetActive(true);
		}
		
		if(currentBoard==1)
			LeftButton.SetActive(false);
		
	}
	
	public void InitiateStore()
	{
		if(PlayerPrefs.GetInt("levelLock2",1)==0)
		{
			RiverCostLabel.text="Buy River : Bought";
			RiverCostLabel.transform.parent.GetComponent<BoxCollider>().enabled=false;
		}
		
		if(PlayerPrefs.GetInt("levelLock5",1)==0)
		{
			SwampCostLabel.text="Buy Swamp : Bought";
			SwampCostLabel.transform.parent.GetComponent<BoxCollider>().enabled=false;
		}
		
		if(PlayerPrefs.GetInt("levelLock4",1)==0)
		{
			JungleCostLabel.text="Buy Jungle : Bought";
			JungleCostLabel.transform.parent.GetComponent<BoxCollider>().enabled=false;
		}
		
		if(PlayerPrefs.GetInt("levelLock3",1)==0)
		{
			LakeCostLabel.text="Buy Lake : Bought";
			LakeCostLabel.transform.parent.GetComponent<BoxCollider>().enabled=false;
		}
	}
	
	
	public bool ReduceCandies(long candyCost)
	{
		
		long candy = long.Parse(PlayerPrefs.GetString("numOfCandies","0"));
		
		if(candy>=candyCost)
		{
			candy-=candyCost;
			PlayerPrefs.SetString("numOfCandies",candy.ToString());
			return true;
		}
		else
			return false;
	}
	
	public void IncreaseCandies(long candies)
	{
		long candy = long.Parse(PlayerPrefs.GetString("numOfCandies","0"));
		
		candy+=candies;
		
		PlayerPrefs.SetString("numOfCandies",candy.ToString());
	}
	
	
}
