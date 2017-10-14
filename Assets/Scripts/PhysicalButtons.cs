using UnityEngine;
using System.Collections;

public class PhysicalButtons : MonoBehaviour {

	public bool inGame;
	
	public string prevPage;
	

	// Update is called once per frame
	void Update () {

			if(inGame)
			{
				if (Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
				{
					
					GameManager.instance.PauseGame();
				}
			}
			else
			{
				if (Input.GetKey(KeyCode.Escape))
				{
					
						if(prevPage=="None")
						{
							Application.Quit();
						}
						else
						{
							
							Application.LoadLevel(prevPage);
							
							
						}
				}
			}
			
	//	}
	}
}
