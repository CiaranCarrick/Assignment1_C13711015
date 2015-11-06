using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {
	public float twinkle;
	public int rateofchange;
	public bool Countup;
	Main main;
	//GUIT G;
	
	void strobe() {//Strobe effect for Bonus
		transform.localScale = new Vector3 (twinkle, twinkle, 0.1f);
		if (rateofchange < 5) {
			rateofchange++;
		}
		else {
			if (Countup) {
				if (twinkle <= 0.35f) {//max scale
					twinkle+=0.05f;
				}
				else {
					Countup = false;// no longer go up
				}
			}
			else {
				if (twinkle >= 0.25f) {//min scale
					twinkle-=0.05f;
				}
				else {
					Countup = true;
				}
			}
			rateofchange = 0;
		}
	}

//	public void message(string _text, Vector3 _trans, Vector3 _worldoffset){
//		GameObject go = new GameObject ();
//		go.AddComponent<GUIT> ();
//		GUIT mygui= go.GetComponent<GUIT> (); // Create Instance of Enemies called myenemies
//		mygui.SetText (_text, _trans,  new Color(1,1,1,1));//_x, _y, _xScale, _yScale, _speed,  _color, _health _Level, alive, particles
//		mygui.transform.parent = this.gameObject.transform;
//	}
	
	public void Move() {
		transform.Translate (Vector3.down * 0.05f);
		if (Main.ship != null) {
			float distance = (this.transform.position - Main.ship.transform.position).magnitude;//creates a float which stores position between A & B
			if (distance <= 0.5f) {
				if(main.mycooldown>5){
				main.Message("-Cooldown", transform.position);
				}
				else
				main.Message("+50", transform.position);

				main.ChangeScore(50); //Increase score for pick up
				main.pickupsound.Play();
				Destroy(gameObject);
				if (main.mycooldown<=5){
					return;
				}
				else
					main.mycooldown-=2; //Decrease Bullet cooldown by 2
			}
		}
	}//end Move

	
	
	// Use this for initialization
	void Start () {
		GameObject M = GameObject.Find("Main");
		main = M.GetComponent<Main> ();
		//G = M.GetComponent<GUIT> ();
		twinkle = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y <= -5f) {// Resets position once it reachs -1
			Destroy(this.gameObject);
		}
		Move ();
		strobe ();
		
	}
}
