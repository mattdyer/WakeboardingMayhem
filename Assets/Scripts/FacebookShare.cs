using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Prime31;

public class FacebookShare : MonoBehaviourGUI{
	
	
	

	/*public static FacebookShare instance;
#if UNITY_IPHONE || UNITY_ANDROID
	private string _userId;

	public static string screenshotFilename = "someScreenshot.png";



	// common event handler used for all graph requests that logs the data to the console
	void completionHandler( string error, object result )
	{
		if( error != null )
			Debug.LogError( error );
		else
			Prime31.Utils.logObject( result );
	}


	void Start()
	{
		instance=this;
		// dump custom data to log after a request completes
		FacebookManager.graphRequestCompletedEvent += result =>
		{
			Prime31.Utils.logObject( result );
		};

		// grab a screenshot for later use
		Application.CaptureScreenshot( screenshotFilename );

		// optionally enable logging of all requests that go through the Facebook class
		//Facebook.instance.debugRequests = true;
	}
	
	public void ShareScores()
	{
		
		var parameters = new Dictionary<string,object>
			{
				{ "link", "http://prime31.com" },
				{ "name", "link name goes here" },
				{ "picture", "http://prime31.com/assets/images/prime31logo.png" },
				{ "caption", "the caption for the image is here" },
				{ "description", "description of what this share dialog is all about" }
			};
			FacebookCombo.showFacebookShareDialog( parameters );
		
	}
#endif*/
	
}
