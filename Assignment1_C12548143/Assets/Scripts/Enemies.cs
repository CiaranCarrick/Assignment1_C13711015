﻿using UnityEngine;
using System.Collections;

public class Enemies : Main {
	bool Targetlocked = true;

	public GameObject _Target; //ship referance
	public void SetEnemies(float _x, float _y, float _xScale, float _yScale, float _speed, Color _color, int _health, int _level){
		xPos = _x;
		yPos = _y;
		xScale = _xScale;
		yScale = _yScale;
		speed = _speed;
		color = _color;
		Health = _health;
		Level = _level;

		if (Level <= 1) {
				GetComponent<Renderer>().material.color = EnemyType [0];//Spawn only reds in level 1
		}
		if (Level == 2) {
				GetComponent<Renderer>().material.color = EnemyType [Random.Range (0, 2)]; //red and green
		}
		if (Level > 2) {
				GetComponent<Renderer>().material.color = EnemyType [Random.Range (0, EnemyType.Length)];// red green and yellow
		}

		//ENEMY TYPES

		//Regular
		if (GetComponent<Renderer>().material.color == EnemyType [0]) {
			if (Level == 5)
				Health = 2;
	
		}
		if (GetComponent<Renderer>().material.color == EnemyType [1]) {
			Health = 3;
			speed = Random.Range (0.11f, 0.15f);
		}
		//Lock on
		if (GetComponent<Renderer>().material.color == EnemyType [2]) {
			if (Level >= 5)
				Health = 2;
				speed = Random.Range (0.11f, 0.13f);
		}

		xPos = Random.Range (ScreenWidthLeft+xScale, ScreenWidthRight);//Spawns objects in range of -8, 8 as ints
		yPos = ScreenHeight + yScale;// Spawns above range of bullets
		Vector3 pos = new Vector3 (Mathf.Round((xPos - xScale / 2)*10)/10, yPos, 0);// so to prevent spawning of screen the equation is My spawn areaa(pos)-half of the enemies widthx-xscale/2, then add its size again to keep it going 1 left and push it 1 right

		transform.position = pos;
		Vector3 scale = new Vector3(xScale, yScale, 0.1f);
		transform.localScale = scale;

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
		//HOMING ENEMIES
		if (Targetlocked==true) {
			if (GetComponent<Renderer>().material.color == EnemyType [2]) {
				// Get a direction Vector from Target to enemy
				Vector3 Dir = _Target.transform.position - transform.position;
				// Normalize it so that it's a unit direction Vector
				Dir.Normalize ();
				
				//ROTATE Enemy ship towards player
				Zangle = Mathf.Atan2 (Dir.y, Dir.x) * Mathf.Rad2Deg + 90; // Draws an angle facing the players position
				Quaternion AngleRotation = Quaternion.Euler (0, 0, Zangle);// Which axis the rotation will take place, in this case the X-Axis
				transform.rotation = Quaternion.RotateTowards (transform.rotation, AngleRotation, Enemyrotatespeed * Time.deltaTime); //How fast enemy rotates towards player
				if (this.gameObject.transform.position.y<= yScale/2)
					{ 
						Targetlocked=false;//Sends enemy downwards when its centre position is equal to half the players height
					}
				}
		}//
	}//end FindPlayer



	
	// Use this for initialization
	void Start () {	
	}

	
	// Update is called once per frame
	void Update () {

		transform.Translate (Vector3.down * speed);
		ResetEnemies ();

		//Enemy Updates
		if(debugmode!=true)
		Findplayer (); //Find distance between player and enemy

	}//end Update
}//end main class
