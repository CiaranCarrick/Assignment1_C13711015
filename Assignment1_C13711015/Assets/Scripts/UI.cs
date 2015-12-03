using UnityEngine;
using System.Collections;

public class UI: Main{
	GUIStyle guistyle = new GUIStyle();

	Main main;
	float deltaTime=0.0f;//FPS
	GameObject Bar;
	string guitext;

	void Start(){
		guitext = "game over";
		GameObject M = GameObject.Find("Main");
		main = M.GetComponent<Main> ();
		UIBar ();
	}
	
	void UIBar(){
		Bar = GameObject.CreatePrimitive (PrimitiveType.Quad);//assign Ship gameobject with a Cube
		Bar.GetComponent<MeshCollider> ().enabled = false;
		Bar.GetComponent<Renderer> ().material.shader = Shader.Find ("UI/Default");// Allows to change opacity with SetColour function
		
		Bar.GetComponent<Renderer> ().material.SetColor ("_Color", new Color(1,1,1, 0.55f));
		Bar.name = "Timer";
		if (debugmode == false) {
			Bar.AddComponent<Decreasebar> ();
		}
		Vector3 pos = new Vector3 ((-9.75f),18.25f, 0.1f);
		Bar.transform.position = pos;
		Vector3 scale = new Vector3 (2.5f, 0.5f, 0.1f);
		Bar.transform.localScale = scale;
		Bar.transform.parent=this.transform;
	}
	void Update(){
	}
	
	void OnGUI(){
		guistyle.font = Resources.Load ("Fonts/Spacefont") as Font; guistyle.normal.textColor = Color.white; guistyle.fontSize = 18;
		if(debugmode)
			GUI.Label (new Rect (Screen.width/2-50, 45, 100, 50), "DEBUG MODE");
		//GUI.Label (new Rect (Screen.width - 50, Screen.height- 15, Screen.width, Screen.height), "ver.2.0.5");
		GUI.color = new Color (1, 1, 1, 0.9f);//80% Opacity
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

			GUI.Label (new Rect (Screen.width/2-50 ,250, 80, 30), guitext, guistyle);
			GUI.Label (new Rect (Screen.width/2-25 ,300, 100, 50), "Score:");
			GUI.Label (new Rect (Screen.width/2+15 ,300, 100, 50), "" + score);

			if (GUI.Button (new Rect (Screen.width/2-30, 350, 80, 30), "Retry?")) {
				Application.LoadLevel (0);
				EnemiesList.Clear();
				return;
			}
		}
		
	}//end OnGui
}