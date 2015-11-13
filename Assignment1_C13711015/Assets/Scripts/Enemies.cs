using UnityEngine;
using System.Collections;

public class Enemies : Main {
	public int pointvalue;
	float timer;
	public Renderer rend;//Refernace to color
	public void SetEnemies(float _x, float _y, float _xScale, float _yScale, float _speed, int _health, int _level, bool _alive, int _pointvalue) {
		xPos = _x;
		yPos = _y;
		xScale = _xScale;
		yScale = _yScale;
		speed = _speed;
		Health = _health;
		Level = _level;
		alive = _alive;
		pointvalue = _pointvalue;
		//Apply Texture
		Material mat;
		mat = Resources.Load("Materials/EnemyShip") as Material;
		gameObject.GetComponent<Renderer>().material=mat;
		//
		if (Level <= 1 ) {
			GetComponent<Renderer>().material.color = EnemyType [0];//Spawn only reds in level 1
		}
		if (Level == 2) {
			GetComponent<Renderer>().material.color = EnemyType [Random.Range (0, 2)]; //red and green
		}
		if (Level > 2) {
			GetComponent<Renderer>().material.color = EnemyType [Random.Range (0, EnemyType.Length)];// red green and yellow
		}




		//ENEMY TYPES
		//Start position

		//Regular
		if (GetComponent<Renderer>().material.color == EnemyType [0]) {
			name="Enemy_R";//Uses default varibles supplied in Createnemy in main
			speed=0.06f;
			color=EnemyType[0];
		}
		if (GetComponent<Renderer>().material.color == EnemyType [1]) {
			name="Enemy_G";
			Health = 3;
			color=EnemyType[1];
			pointvalue=25;
			speed= Random.Range (0.08f, 0.08f);
		}
		//Lock on
		if (GetComponent<Renderer>().material.color == EnemyType [2]) {
			name="Enemy_Y";
			color=EnemyType[2];
			pointvalue=25;
			speed = Random.Range (0.08f, 0.14f);
//			Vector3 ypos = new Vector3 (-8,ScreenHeight/2, 0);// so to prevent spawning of screen the equation is My spawn areaa(pos)-half of the enemies widthx-xscale/2, then add its size again to keep it going 1 left and push it 1 right
//			transform.eulerAngles = new Vector3 (0, 0, 270f);
//			transform.position = ypos;
		}
		GetComponent<Renderer> ().enabled = true;//Reset renderer after object is Respawned in Main class
		
		LoadParticles(transform.position,color, speed,5,transform);//Once everything is set, create particles for each ship
		
		Vector3 scale = new Vector3(xScale, yScale, 0.1f);
		transform.localScale = scale;
		EnemiesList.Add (gameObject);
	}
	
	public void Resetpos(){
		xPos = Random.Range (ScreenWidthLeft+xScale, ScreenWidthRight);//Spawns objects in range of -8, 8 as ints
		yPos = ScreenHeight + yScale;// Spawns above range of bullets
		Vector3 pos = new Vector3 (Mathf.Round((xPos - xScale / 2)*10)/10, yPos, 0);// so to prevent spawning of screen the equation is My spawn areaa(pos)-half of the enemies widthx-xscale/2, then add its size again to keep it going 1 left and push it 1 right
		transform.eulerAngles = new Vector3 (0, 0, 180f);
		transform.position = pos;
	}

	public IEnumerator SpawnWave(){
		float timer=0;
		timer += Time.deltaTime;
		for (int i=1; i<=5; i++) {
			GameObject enemy = GameObject.CreatePrimitive (PrimitiveType.Quad);
			enemy.AddComponent<Enemies> ();
			//enemy.AddComponent<AudioSource>().clip=explosionaudioclip;
			Enemies myenemies = enemy.GetComponent<Enemies> (); // Create Instance of Enemies called myenemies
			myenemies.SetEnemies (0, 0, 1, 1, 0.06f, 1, Level, true, 10);//_x, _y, _xScale, _yScale, _speed,  _color, _health _Level, alive, particles
			enemy.GetComponent<Renderer> ().material.shader = Shader.Find ("Sprites/Default");// Removes light effect on texture"Assets/StarSkyBox"
			EnemiesList.Add (enemy);
			yield return new WaitForSeconds(0.5f);
		}
	}//End CreateEnemies

	public void Findplayer() {
		if (alive==true) {
			if (ship != null) {//Only excute when ship exists
				if (_Target == null) { //If no Target, keep searching for one
					//Targetlocked = false;
					if (GameObject.FindGameObjectWithTag ("Player")) {
						_Target = GameObject.FindGameObjectWithTag ("Player");// Assigns referance Ship
					} 
					else {
						return; // once found, exit if statement
					}// end else
				}//end _Target
				if (_Target != null) {//Once Target is found, execute below
					float distance = (transform.position - _Target.transform.position).magnitude;//creates a float which stores position between A & B
					if (distance <= 1f)//If less than 1f..
						killplayer();//Activate GameOver method
				}
				//HOMING ENEMIES
				if (GetComponent<Renderer>().material.color == EnemyType [2] && this.gameObject.transform.position.y >= _Target.transform.position.y){//If enemy is of type yellow, call lock on method and target enemy if its centre position is equal to half the players height
						Vector3 Dir = _Target.transform.position - transform.position;// Get a direction Vector from Target to enemy
						Dir.Normalize ();// Normalize it so that it's a unit direction Vector, gives it a size of 1
						
						//ROTATE Enemy ship towards player
						Zangle = Mathf.Atan2 (Dir.y, Dir.x) * Mathf.Rad2Deg - 90; // Draws an angle facing the players position
						Quaternion AngleRotation = Quaternion.Euler (0, 0, Zangle);// Which axis the rotation will take place, in this case the X-Axis
						transform.rotation = Quaternion.RotateTowards (transform.rotation, AngleRotation, Enemyrotatespeed * Time.deltaTime); //How fast enemy rotates towards player
						
				}//end enemytype2 if
				else
					return;
			} 
		}//end alive if
		else
			return;
	}//end FindPlayer
	
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		InvokeRepeating ("shoot", Random.Range(1,3), 8);
		xPos = Random.Range (ScreenWidthLeft+xScale, ScreenWidthRight);//Spawns objects in range of -8, 8 as ints
		yPos = ScreenHeight + yScale;// Spawns above range of bullets
		Vector3 pos = new Vector3 (Mathf.Round((xPos - xScale / 2)*10)/10, yPos, 0);// so to prevent spawning of screen the equation is My spawn areaa(pos)-half of the enemies widthx-xscale/2, then add its size again to keep it going 1 left and push it 1 right
		transform.eulerAngles = new Vector3 (0, 0, 180f);
		transform.position = pos;
	}
	void shoot(){
		if(color==EnemyType[0] && alive==true && ship)
			FireBullets (this.transform, (speed*Random.Range(1.25f, 2f)), false);
	}

	void Damageindicater(){
		timer += Time.deltaTime;
		if(timer>=0.05f){
			rend.material.color = color;
			timer=0;
		}
	}

	// Update is called once per frame
	void Update () {
		if (rend.material.color == Color.red) {
			Damageindicater();
		}

		transform.Translate (Vector3.up * speed);//Translate is less intense on the CPU, prevents object from colliding via translate movement

		ResetEnemies (GetComponent<Enemies>());
		//Enemy Updates
		if (debugmode!=true)
			Findplayer (); //Find distance between player and enemy
	}//end Update
}//end main class