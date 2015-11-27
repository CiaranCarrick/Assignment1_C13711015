using UnityEngine;
using System.Collections;

public class Menu : Main {
	GUIStyle guistyle = new GUIStyle();
	Stars stars;
	// Use this for initialization
	void Start () {
		CreateStars (5,0.018f, 1.0f, 0f, -20, -50, true); //set _starCount amount here

		Screen.SetResolution (480, 700, false, 60);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void BeginGame(){
		Application.LoadLevel (1);
	}
	public void EndGame(){
		Application.Quit ();
		return;
	}

	void OnGUI(){
		guistyle.fontSize = 8;guistyle.normal.textColor = Color.white;
//		if (GUI.Button (new Rect (Screen.width/2-50, Screen.height/2, 100, 30), "Start Game?")) {
//			Application.LoadLevel(1);;
//		}
//		if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2 + 30, 100, 30), "Quit")) {
//			Application.Quit ();
//			return;
//		}
//		
		GUI.Label (new Rect (Screen.width/2+60, Screen.height/2+80, Screen.width, Screen.height), "ver.2.0.3", guistyle);
	}//end OnGui
}
