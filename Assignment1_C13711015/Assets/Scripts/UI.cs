using UnityEngine;
using System.Collections;

public class UI: Main{
	Main main;
	float deltaTime=0.0f;
	

	void Start(){
		GameObject M = GameObject.Find("Main");
		main = M.GetComponent<Main> ();
	}

	void Update(){
	}

	void OnGUI(){
		if(debugmode)
		GUI.Label (new Rect (Screen.width/2-50, 45, 100, 50), "DEBUG MODE");
		GUI.Label (new Rect (Screen.width - 40, Screen.height- 15, Screen.width, Screen.height), "ver.1.7");
		GUI.color = new Color (1, 1, 1, 0.8f);//80% Opacity
		GUI.Label (new Rect (5, 5, 100, 50), "Score: " + score.ToString("000000"));
		GUI.Label (new Rect (5, 25, 100, 50), "Time: " + main.Leveltime.ToString ("f1"));
		GUI.Label (new Rect (5, 45, 100, 50), "Level: " + main.Level);
		if (debugmode==true) {
			//FPS
			deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
			float fps = 1.0f / deltaTime;//Calculates fps
			string FPStext = string.Format ("{0:0.} fps", fps);// Store FPS in text string f
			GUI.Label (new Rect (Screen.width - 35, 0, Screen.width, Screen.height * 2 / 50), FPStext);
			//
		}
		if (ship == null) {
			GUI.Label (new Rect (Screen.width/2-25 ,250, 80, 30), "Game Over");
			GUI.Label (new Rect (Screen.width/2-25, 300, 100, 50), "Score: " + score);
			if (GUI.Button (new Rect (Screen.width/2-30, 350, 80, 30), "Retry?")) {
				Application.LoadLevel (0);
				EnemiesList.Clear();
				return;
			}
		}
		
	}//end OnGui
}
