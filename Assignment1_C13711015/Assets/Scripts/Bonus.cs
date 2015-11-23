using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {
	public Transform mytransform;
	public float twinkle;
	public int rateofchange;
	public bool Countup;
	Main main;
	GameObject shield;
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
		if (Main.ship != null&&this.gameObject!=null) {
			float distance = (this.transform.position - Main.ship.transform.position).magnitude;//creates a float which stores position between A & B
			if (distance <= 1.5f){
				mytransform.position = Vector3.MoveTowards(transform.position, Main.ship.transform.position, 4f*Time.deltaTime);
			}

			if (distance <= 0.5f) {
				if(GetComponent<Renderer>().material.color!=Color.white){
					GiveShield(new Vector3(2.2f, 2.2f, 3.5f), new Vector3(0,-20,0));
					main.ChangeScore(10); //Increase score for pick up
					main.pickupsound.Play();
					main.Message("Shield", mytransform.position);
					Destroy(gameObject);
					return;
				}
				if(main.mycooldown>5 ){
				main.Message("-Cooldown", mytransform.position);
				}
				else
				main.Message("+50", mytransform.position);

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

	void GiveShield(Vector3 _size, Vector3 _pos){
		shield=GameObject.CreatePrimitive(PrimitiveType.Sphere);
		shield.GetComponent<Renderer> ().material.shader = Shader.Find ("Legacy Shaders/Transparent/Diffuse");// Allows to change opacity with SetColour function
		shield.GetComponent<Renderer> ().material.SetColor ("_Color", new Color(0f/255f,213f/255f,255f/255f,70/255f));
		shield.name = "Shield";
		shield.transform.localScale=new Vector3(_size.x, _size.y, _size.z);
		shield.transform.position=new Vector3(_pos.x, _pos.y, _pos.z);
		shield.AddComponent<Rotatearound>();
	}

	
	
	// Use this for initialization
	void Start () {
		if (Random.Range (0, 5) > 2 && GameObject.Find("Ship/Shield") == null) {
			GetComponent<Renderer> ().material.color = new Color (0f / 255f, 213f / 255f, 255f / 255f, 70 / 255f);
		}
		mytransform = transform;
		GameObject M = GameObject.Find("Main");
		main = M.GetComponent<Main> ();
		//G = M.GetComponent<GUIT> ();
		twinkle = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y <= -Main.ScreenHeight/2-2) {// Resets position once it reachs -1
			Destroy(this.gameObject);
		}
		Move ();
		strobe ();
	}
}
