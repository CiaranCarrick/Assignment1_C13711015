using UnityEngine;
using System.Collections;

public class UI: Main{
	Main main;
	float deltaTime=0.0f;//FPS
	float bartrans=0.0f;
	GameObject Bar;
	void Start(){
		GameObject M = GameObject.Find("Main");
		main = M.GetComponent<Main> ();
		UIBar ();
	}
	void UIBar(){
		Bar = GameObject.CreatePrimitive (PrimitiveType.Quad);//assign Ship gameobject with a Cube
		Bar.GetComponent<MeshCollider> ().enabled = false;
		Bar.GetComponent<Renderer> ().material.shader = Shader.Find ("UI/Default");// Allows to change opacity with SetColour function
		float bartrans;
		bartrans = 0.55f;//Set transperacny to 3/5 of original
		Bar.GetComponent<Renderer> ().material.SetColor ("_Color", new Color(1,1,1,bartrans));
		Bar.name = "Timer";
		if (debugmode == false) {
			Bar.AddComponent<Decreasebar> ();
		}
		Vector3 pos = new Vector3 ((-7.0f), 18.80f, 0.1f);
		Bar.transform.position = pos;
		Vector3 scale = new Vector3 (2.5f, 0.5f, 0.1f);
		Bar.transform.localScale = scale;
		Bar.transform.parent=this.transform;
	}
	void Update(){
	}

	void OnGUI(){
		if(debugmode)
		GUI.Label (new Rect (Screen.width/2-50, 45, 100, 50), "DEBUG MODE");
		GUI.Label (new Rect (Screen.width - 50, Screen.height- 15, Screen.width, Screen.height), "ver.1.9.5");
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
