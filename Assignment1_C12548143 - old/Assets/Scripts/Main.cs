using UnityEngine;
using System.Collections.Generic;

public class Main : MonoBehaviour {

	//Inheritance
	public Color[] EnemyType= {Color.red, Color.green, Color.yellow};// Enemy types(Colors) stored in array
	public static List <GameObject> EnemiesList= new List<GameObject>();

	public GameObject background; //2d Background image
	public static GameObject ParticleManager;// Used to clean up Hierarchy
	GameObject StarManager;//

	public float xPos;// These will be used to contain values for each methods constructor
	public float yPos;//
	public float xScale;//
	public float yScale;//
	public float speed;//
	public Color color;//
	public int Health;//

	public float EnemySpawnTime; // How long between each spawn.
	public float Enemyrotatespeed=40f;// Speed of lock on enemy rotation
	public float Zangle;

	int cooldown;//Weapon Cooldown
	int starCount=25; //set stars
	public int ScreenWidth = 7;
	public int ScreenHeight = 18;

	public static int mycooldown;//changes weapon cooldown across scripts
	public static int EnemyCount;
	public static GameObject ship;
	public static float Leveltime;
	public static int score; 
	public static int Level;
	public bool debugmode=false;



	void Player(){
		ship = GameObject.CreatePrimitive (PrimitiveType.Cube);//assign Ship gameobject with a Cube
		ship.name = "Ship";
		ship.AddComponent<Ship> (); //Attach Ship script to ship GameObject
		ship.AddComponent<Movement> ();
		Ship myship = ship.GetComponent<Ship> (); // Create Instance of Enemies called myenemies
		myship.GetComponent<Ship>().SetShip (0, 0, 1.0f, 1.0f, 0.4f, new Color(100,0f,255f, 1f));
	}//End Player
	


	void CreateEnemies(){
		for (int i=1; i<=Level; i++) {
			GameObject enemy = GameObject.CreatePrimitive (PrimitiveType.Cube);
			enemy.name = "Enemy";
			enemy.AddComponent<Enemies> ();
			Enemies myenemies = enemy.GetComponent<Enemies> (); // Create Instance of Enemies called myenemies
			myenemies.SetEnemies (0, 0, 1, 1, 0.1f, EnemyType [0], 1);//_x, _y, _xScale, _yScale, _speed,  _color, _health
			EnemiesList.Add (enemy);
		}
	}//End CreateEnemies


	void CreateBullets(){
			GameObject bullet = GameObject.CreatePrimitive (PrimitiveType.Cube);
			bullet.name = "Bullet";
			float _xpos = ship.transform.position.x;// gives same position of ship
			float _ypos = ship.transform.position.y;//sets bullet at tip of ship
			bullet.AddComponent<ShipShoot> ();
			ShipShoot sbullet = bullet.GetComponent<ShipShoot> ();
			sbullet.SetBullet (_xpos, _ypos, 0.2f, 0.3f, 0.4f); //float _x, float _y, float _xScale, float _yScale, float _speed
			
	}//End CreateBullets


	void CreateStars(int _starCount){ //Takes in starCount from field above
		for (int i =0; i<=starCount; i++) {
			GameObject stars=GameObject.CreatePrimitive(PrimitiveType.Cube);
			stars.name = "Star";
			stars.collider.enabled = false; 
			stars.AddComponent<Stars>();
			Stars sstars=stars.GetComponent<Stars>();
			sstars.SetStars(0, 0, 0.05f, 1.0f,Random.Range(-0.05f, -0.30f));
			stars.transform.parent=StarManager.transform;
		}
	}//End CreateStars

	
	void Background(){
		background = GameObject.CreatePrimitive (PrimitiveType.Quad); //Flat plane
		if (background != null) {
			background.renderer.material.mainTexture = Resources.Load<Texture2D> ("Level_1");//Quad Texture
			background.name = "BackGround";// Texture Name
			background.transform.position = new Vector3 (0, 5, 5);
			background.transform.localScale = new Vector3 (15f, 30f, 0);
			background.renderer.material.shader = Shader.Find ("Unlit/Texture");// Removes light effect on texture
			if(Level==5){
				background.renderer.material.mainTexture = Resources.Load<Texture2D> ("Level_final");
			} 
		}
	}//End Background
	

	public void ScoreM(){
		GameObject SM = new GameObject ();//assign Ship gameobject with a Cube
		SM.transform.position = new Vector3 (0, 0, -10f);
		SM.name = "ScoreM";
		SM.GetComponent<UI> ();
		SM.AddComponent<UI> ();
	}//End ScoreM
	

	public void CreateParticles(Vector3 pos, Color _col, float _spd, int part_amount){// Targets pos, Targets Color & Target speed
		for (int i = 0; i < part_amount; i++) {
			GameObject particle = GameObject.CreatePrimitive (PrimitiveType.Cube);
			particle.name = "Particle";
			particle.AddComponent<Particles> ();
			Particles party = particle.GetComponent<Particles> ();
			Vector3 Direction = new Vector3 (Random.Range (-1f, 1f), Random.Range (-1f, 1f), 0); //Random directions for particles
			party.SetupParticle (pos.x, pos.y, Random.Range (0.05f, 0.1f), Random.Range (0.05f, 0.1f), _spd, Direction, _col, 3);
			particle.transform.parent=ParticleManager.transform;
		}
	}


	void AdjustDifficulty(){
		//Refresh Invoke
		EnemyCount = 5; //resets EnemyCount, when 5 enemies are destroyed.
		CancelInvoke ("CreateEnemies");
		if (EnemySpawnTime != 0.50f) {
			EnemySpawnTime -= 0.25f; //Decrease EnemySpawnTime
		}
		InvokeRepeating ("CreateEnemies", 1f, EnemySpawnTime); //Re-intialize Invoke with new EnemySpawnTime value

	}

	public void GameOver(){
		Destroy (ship.gameObject);
		CreateParticles (transform.position, ship.renderer.material.color, speed, 100);
	}


	void NextLevel() {

		foreach (GameObject enemy in EnemiesList)//Clear Screen of enemies
		{
			EnemiesList.Remove(gameObject);
			Destroy (enemy.gameObject);
		}
		Leveltime = 60; 
		if (Level!=6)
			Level++;
		if(Level==5){
			background.renderer.material.mainTexture = Resources.Load<Texture2D> ("Level_final");
		} 
		if (Level == 6) {
			Level= 1;
			GameOver();
			InvokeRepeating("Partytime", 1f, 1f);
		}
	}
	
	
	public void ResetEnemies(){
		if (transform.position.y <= -3f) {// Resets position once it reachs -1
			Destroy(gameObject);
		}
	}


	void Partytime(){//No game is complete without some Confetti!
		Vector3 Party = new Vector3 (Random.Range (-ScreenWidth, ScreenWidth), Random.Range (0, ScreenHeight), 0.1f);
		CreateParticles(Party, new Color(Random.Range(0.1f, 1f),Random.Range(0.1f, 1f),Random.Range(0.1f, 1f),0), 0.1f, 20); // Feed in particles spawn area, color and take in speed for effects
	}


	void Start () {
		StarManager = new GameObject ();// Contains all stars in a scene
		StarManager.name="Stars";
		ParticleManager = new GameObject ();// Contains all particles in a scene
		ParticleManager.name="Particles";
		mycooldown = 15;
		cooldown = 0;
		EnemySpawnTime = 2.50f;
		Leveltime = 60;
		Level = 1;
		EnemyCount = 5;// Counter for use with Enemy difficulty
		score = 0;
		Player ();
		Background ();
		ScoreM ();
		CreateStars (starCount); 
		InvokeRepeating ("CreateEnemies", 1f, EnemySpawnTime);
	}//End Start
	

	void Update () {
		if (ship) //Ship must exist or not be null for bullets to be fired
		{
			if (cooldown>0)
			{
				cooldown--;
			}
			if (Input.GetKey(KeyCode.Space)&& cooldown ==0)
			{
				CreateBullets(); //call the CreateBullet function
				cooldown=mycooldown;
				if (mycooldown<=3)
					mycooldown=3; //max speed of bullets after all bonus' are picked up
			}
			if (debugmode!=true) {
				if (Leveltime <= 0)
				{
					NextLevel();
					AdjustDifficulty();//Every new level, enemies will spawn quicker, regardless of player kills.
				} 
				
				else 
				{
					Leveltime -= Time.deltaTime;
				}
			}//end Debug
		}//end ship
		if (EnemyCount==0 && EnemySpawnTime>=0.50f)  
			AdjustDifficulty();// Calls Method that adjusts enemies Difficulty
	}//End Update

}//End Main Class
