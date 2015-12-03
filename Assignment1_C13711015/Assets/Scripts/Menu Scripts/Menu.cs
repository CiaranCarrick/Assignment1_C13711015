using UnityEngine;
using System.Collections;

public class Menu : Main {
	GUIStyle guistyle = new GUIStyle();
	Stars stars;
	string Gameversion;
	// Use this for initialization
	void Start () {
		Gameversion = "ver 2.0.5";
		CreateStars (3,0.018f, 1.0f, 0f, -30, -70, true); //set _starCount amount here
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
		GUI.Label (new Rect (Screen.width/2+70, Screen.height/2+80, Screen.width, Screen.height), Gameversion, guistyle);
	}//end OnGui
}
