using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {
	
	public static TutorialManager instance;
	
	public bool isFingerLeft;
	public bool isFingerRight;
	public bool isFingerUp;
	public bool isFingerTap;
	
	public GameObject FingerLeft;
	public GameObject FingerRight;
	public GameObject FingerUp;
	public GameObject FingerTap;
	public GameObject[] circle;
	
	public GameObject[] FingerModes;
	
	void Start()
	{
		instance=this;
		StartCoroutine(EnableFingerTap());
	}
	
	
	
	public IEnumerator EnableFingerRight()
	{
		FingerModes[2].SetActive(false);
		yield return new WaitForSeconds(2f);
		GameManager.instance.isGamePaused=true;
		isFingerRight=true;
		FingerModes[0].SetActive(true);
		
		FingerRight.SetActive(true);
	}
	
	public IEnumerator EnableFingerLeft()
	{
		yield return new WaitForSeconds(2f);
		GameManager.instance.isGamePaused=true;
		isFingerLeft=true;
		FingerRight.SetActive(false);
		FingerLeft.SetActive(true);
	}
	
	public IEnumerator EnableFingerUp()
	{
		FingerModes[0].SetActive(false);
		yield return new WaitForSeconds(2f);
		GameManager.instance.isGamePaused=true;
		isFingerUp=true;
		
		FingerModes[1].SetActive(true);
		FingerUp.SetActive(true);
	}
	
	public IEnumerator EnableFingerTap()
	{
		yield return new WaitForSeconds(2.2f);
		GameManager.instance.isGamePaused=true;
		isFingerTap=true;
		//FingerModes[1].SetActive(false);
		FingerModes[2].SetActive(true);
		FingerTap.SetActive(true);
		while(isFingerTap)
		{
			circle[0].GetComponent<UISprite>().enabled=false;
			circle[1].GetComponent<UISprite>().enabled=false;
			yield return new WaitForSeconds(.5f);
			circle[0].GetComponent<UISprite>().enabled=true;
			yield return new WaitForSeconds(.5f);
			circle[0].GetComponent<UISprite>().enabled=false;
			circle[1].GetComponent<UISprite>().enabled=true;
			yield return new WaitForSeconds(.5f);
		}
		yield return null;
	}
	
	public void DestroyThis()
	{
//		GameManager.instance.isTutorialMode=false;
		Destroy(this.gameObject);
	}
}
