using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.SetResolution (480, 700, false, 60);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if (GUI.Button (new Rect (Screen.width/2-50, Screen.height/2, 100, 30), "Start Game?")) {
			Application.LoadLevel ("Main");
		}
		if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2 + 30, 100, 30), "Quit")) {
			Application.Quit ();
			return;
		}
		
	}//end OnGui
}
