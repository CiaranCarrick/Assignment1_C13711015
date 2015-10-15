using UnityEngine;
using System.Collections;

public class UI: Main{

	//public int displayScore; //Debug, I heard of Singletons and Debug mode via inspector, however, this was very Straight forward

	void Start(){
	}

	void Update(){
		//displayScore = Main.score; //Debug
	}

	void OnGUI(){
		if(debugmode)
			GUI.Label (new Rect (Screen.width/2-50, 45, 100, 50), "DEBUG MODE");

		GUI.color = new Color (1, 1, 1, 0.8f);//80% Opacity
		GUI.Label (new Rect (5, 5, 100, 50), "Score: " + Main.score);
		GUI.Label (new Rect (5, 25, 100, 50), "Time: " + Main.Leveltime.ToString ("f0"));
		GUI.Label (new Rect (5, 45, 100, 50), "Level: " + Main.Level);
		if (ship == null) {
			GUI.Label (new Rect (Screen.width/2-25 ,250, 80, 30), "Game Over");
			GUI.Label (new Rect (Screen.width/2-25, 300, 100, 50), "Score: " + Main.score);
			if (GUI.Button (new Rect (Screen.width/2-30, 350, 80, 30), "Retry?")) {
				Application.LoadLevel (0);
			}
		}
		
	}//end OnGui

	public static void ChangeScore (int NewScore) // Add Score Method
	{
		Main.score += NewScore;
		return;
	}
}
