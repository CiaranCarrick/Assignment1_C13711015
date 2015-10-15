using UnityEngine;
using System.Collections;

public class Enemies : Main {
	bool Targetlocked=true;
	public GameObject _Target; //ship referance

	public void SetEnemies(float _x, float _y, float _xScale, float _yScale, float _speed, Color _color, int _health){
		xPos = _x;
		yPos = _y;
		xScale = _xScale;
		yScale = _yScale;
		speed = _speed;
		color = _color;
		Health =_health;

		if (Main.Level <= 1) {
			renderer.material.color = EnemyType [0];
		}
		if (Main.Level == 2) {
			renderer.material.color = EnemyType [Random.Range (0, 2)];
		}
		if (Main.Level>2) {
			renderer.material.color = EnemyType [Random.Range (0, EnemyType.Length)];
		}

		//ENEMY TYPES

		//Fast
		if (renderer.material.color==EnemyType[1])
		{
			Health=3;
			speed+=0.05f;
		}
		//Lock on
		if (renderer.material.color==EnemyType[2])
		{
			speed=0.07f;
		}

		xPos = Random.Range (-ScreenWidth+xScale, ScreenWidth-xScale);
		yPos = ScreenHeight + yScale;// Spawns above range of bullets
		Vector3 pos = new Vector3 (xPos, yPos, 0);
		transform.position = pos;
		Vector3 scale = new Vector3(xScale, yScale, 0.1f);
		transform.localScale = scale;
	}

	
	public void SubtractLife(GameObject _Tar) { //Method with GameObject Parameter that decreses Health Variable, used in ShipShoot's update
		Health--;
	}


	public void Findplayer() {
		if (_Target == null) { //If no Target, search for one
			if (GameObject.Find ("Ship")) {
				_Target = GameObject.Find ("Ship");// Assigns referance Ship
			} 
			else {
				return; // once found, exit if statement
			}// end else
		}//end _Target
		if (_Target != null) {//Once Target is found, execute below
			float distance = (transform.position - _Target.transform.position).magnitude;//creates a float which stores position between A & B
			if (distance <= 1f)
				GameOver ();
		}
	}


	void Targetlockon() { //Tracks the target and travels towards it
		if (Targetlocked==true) {
			if (_Target!=null) {//Once Target is found, execute below
				// Get a direction Vector from Target to enemy
				Vector3 Dir = _Target.transform.position - transform.position;
				// Normalize it so that it's a unit direction Vector
				Dir.Normalize();
				
				//ROTATE Enemy ship towards player
				Zangle= Mathf.Atan2(Dir.y, Dir.x )* Mathf.Rad2Deg+90; // Draws an angle facing the players position
				Quaternion AngleRotation = Quaternion.Euler(0, 0, Zangle);// Which axis the rotation will take place, in this case the X-Axis
				transform.rotation= Quaternion.RotateTowards(transform.rotation, AngleRotation, Enemyrotatespeed*Time.deltaTime); //How fast enemy rotates towards player
				if (this.gameObject.transform.position.y<= yScale/2)
				{ 
					Targetlocked=false;//Sends enemy downwards when its centre position is equal to half the players height
				}
			}
		}//end Targetlocked
	}//end Targetlockon


	
	// Use this for initialization
	void Start () {	
	}

	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.down * speed);
		ResetEnemies ();
		if (this.gameObject==null)
			Main.EnemiesList.Remove(gameObject);
		
		//Enemy Updates
		if(debugmode!=true)
		Findplayer (); //Find distance between player and enemy
		
		if (renderer.material.color == EnemyType [2])//If enemy is of type yellow, call lock on method
		{
			Targetlockon ();
		}				
	}//end Update
}//end main class
