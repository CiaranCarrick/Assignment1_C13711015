using UnityEngine;
using System.Collections.Generic;
public class Main : MonoBehaviour {
	//Inheritance
	public Color[] EnemyType= {Color.red, Color.green, Color.yellow};// Enemy types(Colors) stored in array
	public static List <GameObject> EnemiesList=new List<GameObject>();
	public static int enemycount;
	public static List <GameObject> bullets;
	public static AudioSource bulletsound;
	public AudioSource pickupsound;
	public static AudioSource explosionsound; //Accessed from ShipShoot
	AudioClip bulletaudioclip;
	AudioClip explosionaudioclip;
	AudioClip pickupaudioclip;
	GUIT G; //instance of GUIT class

	GameObject background; //2d Background image
	public GameObject Button;

	protected static GameObject ParticleManager;// Used to clean up Hierarchy
	protected GameObject EnemyManager, StarManager;

	private float EnemySpawnTime; // How long between each spawn.

	protected float xPos,yPos,xScale,yScale,speed, minspeed, maxspeed;//these will be used to contain values for each methods constructory
	public Color color;//
	public int Health;//
	public bool alive;
	public GameObject _Target; //ship referance

	public float Enemyrotatespeed=40f;// Speed of lock on enemy rotation
	public float Zangle;//For Enemies
	public float Leveltime;

	public static GameObject ship;
	public static int score;
	public int mycooldown, Level, cooldown;
    public static int ScreenWidthLeft= -11;//Controls the spawning paremeters for objects in scene
	public static int ScreenWidthRight = 11;
	public static int ScreenHeight = 25;

	public bool debugmode =false;
	public Vector3 Targetposition =new Vector3(0,-10f,0);

	public bool Gamestart;

	void Player(){
		ship = new GameObject ();
		ship.AddComponent<MeshRenderer> ();
		ship.gameObject.tag="Player"; 
		ship.AddComponent<Ship> (); //Attach Ship script to ship GameObject
		//ship.AddComponent<Movement> ();
		Ship myship = ship.GetComponent<Ship> (); // Create Instance of ship called myship 
		myship.GetComponent<Ship> ().SetShip (0, -15.0f, 1.0f, 1.0f, 0.5f, new Color (1f, 1f, 1f, 1f));
	}//End Player
	
	
	void CreateEnemies(){
		for (int i=1; i<=1; i++) {
			GameObject enemy =new GameObject();
			enemy.AddComponent<MeshRenderer>();
			enemy.AddComponent<Enemies> ();
			//enemy.AddComponent<AudioSource>().clip=explosionaudioclip;
			Enemies myenemies = enemy.GetComponent<Enemies> (); // Create Instance of Enemies called myenemies
			myenemies.SetEnemies (0, 0, 1, 1, 0.06f,1,Level, true, 10);//_x, _y, _xScale, _yScale, _speed,  _color, _health _Level, alive, particles
			enemy.GetComponent<Renderer> ().material.shader = Shader.Find ("Sprites/Default");// Removes light effect on texture"Assets/StarSkyBox"
			//EnemiesList.Add (enemy);
			enemy.transform.parent=EnemyManager.transform;
		}
	}//End CreateEnemies
	

	//This method can only be accessed by inheriting classes, Used in derived class constructors to spawn particles
	protected void LoadParticles(Vector3 pos, Color _col, float _spd, int part_amount, Transform _parent){//Spawning the particles inside enemies at start to be activated once enemie dies
		GameObject Eman = new GameObject ();// Contains all particles in a scene
		Eman.name = "p";
		Eman.AddComponent<CleanUp>();
		Eman.transform.parent=_parent.transform;
		for (int i= 0; i <= part_amount; i++) {
			GameObject particle = GameObject.CreatePrimitive (PrimitiveType.Quad);
			particle.name = "BOOM";
			particle.AddComponent<Particles> ();
			Particles party = particle.GetComponent<Particles> ();
			Vector3 Direction = new Vector3 (Random.Range (-1f,1f), Random.Range (-1f, 1f), 0); //Random directions for particles
			party.SetupParticle (pos.x, pos.y, Random.Range (0.05f, 0.1f), Random.Range (0.05f, 0.1f), _spd, Direction, _col, 3);
			particle.transform.parent=Eman.transform;
			Eman.SetActive(false);
		}
	}


	public void CreateBonus(Vector3 _pos){//Insert Vector3 
		GameObject bonus=GameObject.CreatePrimitive(PrimitiveType.Capsule);
		bonus.AddComponent<Bonus> ();
		bonus.transform.localScale = new Vector3(0.2f, 0.2f, 0.1f);
		bonus.transform.position = _pos;

	}
	
	public void LoadShip(int _bullets){
		GameObject Clip = new GameObject();//Clean up hierachy 
		Clip.name = "GameClip";//Name empty Gameobject
		bullets=new List<GameObject>();//intialize bulllets list
		for (int i =1; i<=_bullets; i++) {
			GameObject bullet = GameObject.CreatePrimitive (PrimitiveType.Quad);
			bullet.name = "Bullet";
			//bullet.AddComponent<Collisions>();
			Material mat;
			mat = Resources.Load("Materials/Bullet") as Material;
			bullet.AddComponent<ShipShoot> ();//Add script to each bullet
			bullet.GetComponent<Renderer>().material=mat;
			bullet.GetComponent<Renderer> ().material.shader = Shader.Find ("Particles/Additive");// Removes light effect on texture"Assets/StarSkyBox"
			bullet.SetActive(false);
			bullets.Add (bullet);//Add gameobject to list
			bullet.transform.parent = Clip.transform; //Makes LTruster a child to Ship
		}
	}//End CreateBullets

	public void FireBullets(Transform _T, float _speed, bool _bonus){
		for (int i =0; i<bullets.Count; i++) {
			GameObject bul = bullets[i].gameObject;
			if(!bullets[i].activeInHierarchy)
			{
				bool alive=true;
				float _xpos = _T.transform.position.x;// gives same position of ship
				float _ypos = _T.transform.position.y;//sets bullet at tip of ship
				float _Zrot = _T.transform.eulerAngles.z;
				bul.GetComponent<ShipShoot>().SetBullet(_xpos, _ypos, _Zrot, 0.4f, 0.6f, _speed, mycooldown, alive, Vector3.up, _bonus); //float _x, float _y, float _xScale, float _yScale, float _speed
				bullets[i].SetActive(true);
				bulletsound.Play();
				break;
				}
			}
	}//End CreateBullets
	
	protected void CreateStars(int _starCount, float _xScale, float _yScale, float _speed, float _min, float _max, bool _scale){ //Takes in starCount from field above
		for (int i =1; i<=_starCount; i++) {
			GameObject stars=GameObject.CreatePrimitive(PrimitiveType.Quad);
			stars.GetComponent<Collider>().enabled = false; 
			stars.AddComponent<Stars>();
			Stars sstars=stars.GetComponent<Stars>();
			_speed=Random.Range(_min, _max);
			sstars.SetStars(0, 0, _xScale, _yScale, _speed, _min, _max, _scale);
			if(StarManager)
			stars.transform.parent=StarManager.transform;
		}
	}//End CreateStars

	
	void Background(){
		background = GameObject.CreatePrimitive (PrimitiveType.Quad); //Flat plane
		if (background != null) {
			//background.AddComponent<BackgroundScroll> ();
			background.GetComponent<Renderer> ().material.mainTexture = Resources.Load<Texture2D> ("isi_textures/Level_1");//Quad Texture
			background.name = "BackGround";// Texture Name
			background.transform.position = new Vector3 (0f,5f, 5f);
			background.GetComponent<Renderer> ().material.mainTextureScale = new Vector2 (1, 1);//Controls tiling on tecture
			background.transform.localScale = new Vector3 (19f+Screen.width/100, 36f, 0f);
			background.isStatic=true;
			background.GetComponent<Renderer> ().material.shader = Shader.Find ("Unlit/Texture");// Removes light effect on texture"Assets/StarSkyBox"
			}

	}//End Background
	

	public void ScoreM(){
		GameObject SM = new GameObject ();//assign Ship gameobject with a Cube
		SM.transform.position = new Vector3 (0, 0, -10f);
		SM.name = "ScoreM";
		SM.AddComponent<UI> ();
	}//End ScoreM


	public void CreateParticles(Vector3 pos, Color _col, float _spd, int part_amount){// Targets pos, Targets Color & Target speed
		for (int i = 0; i < part_amount; i++) {
			GameObject particle = GameObject.CreatePrimitive (PrimitiveType.Quad);
			particle.name = "Particle";
			particle.AddComponent<Particles> ();
			Particles party = particle.GetComponent<Particles> ();
			Vector3 Direction = new Vector3 (Random.Range (-1f, 1f), Random.Range (-1f, 1f), 0); //Random directions for particles
			party.SetupParticle (pos.x, pos.y, Random.Range (0.05f, 0.1f), Random.Range (0.05f, 0.1f), _spd, Direction, _col, 3);
			particle.transform.parent=ParticleManager.transform;
			
		}
	}

	public void SubtractLife(GameObject _Tar) { //Method with GameObject, Parameter that decreses Health Variable, used in ShipShoot's update
		//Debug.Log ("HIT " + Health);
		Health--;
		if (Health > 0) {
			return;
		} else
			explosionsound.Play ();
			_Tar.GetComponent<Renderer> ().enabled = false;
			EnemiesList.Remove(_Tar.gameObject); //Remove enemy Gameobject from List, also avoids missingexception

	}
	
	void Respawn(Enemies _Tar){
		if (ship != null) {
			//print ("Yes Respawn ");
			_Tar.SetEnemies (0, 0, 1, 1, 0.1f, 1, Level, true, 10);//_x, _y, _xScale, _yScale, _speed,  _color, _health _Level, alive, pointsvalue
		}
	}


	void NextLevel() {
		//Adjust Difficulty //Every new level, enemies will spawn quicker, regardless of player kills.
		//Refresh Invoke
		CancelInvoke ("CreateEnemies");
		if (EnemySpawnTime != 1f) {
			EnemySpawnTime -= 0.25f; //Decrease EnemySpawnTime
		}
		InvokeRepeating ("CreateEnemies", 0f, EnemySpawnTime); //Re-intialize Invoke with new EnemySpawnTime value
		//end Adjust
		foreach (GameObject enemy in EnemiesList)//Clear Screen of enemies
		{
			EnemiesList.Remove(gameObject); //Remove enemy Gameobject from List, also avoids missingexception
			Destroy(enemy.gameObject);
		}
		foreach (GameObject bullet in bullets)//Clear Screen of enemies
		{
			bullet.SetActive(false);
		}
		if (Level < 5) {
			ship.transform.position = new Vector3 (0, -15f, 0);
		}
		//Gamestart = false;
		Leveltime = 50;//temp code
		Decreasebar.size = 100;//
		Decreasebar.Scaler = (Mathf.Round(Decreasebar.size/Leveltime));//100/30==3.33333(rounded=)30f//

		if (Level!=6)
			Level++;
		if(Level>=3){
			background.GetComponent<Renderer>().material.mainTexture = Resources.Load<Texture2D> ("isi_Textures/Level_3"); //Apply texture to level 5 from resource folder
		} 
		if(Level>=5){
			background.GetComponent<Renderer>().material.mainTexture = Resources.Load<Texture2D> ("Textures/Level_final"); //Apply texture to level 5 from resource folder
		} 
		if (Level == 6) {
			GameOver();
			Leveltime = 0;
			InvokeRepeating("Partytime", 1f, 1f); //Call after final level is complete
		}
	}//end NextLevel


	void Partytime(){//No game is complete without some Confetti!
		Vector3 Party = new Vector3 (Random.Range (ScreenWidthLeft, ScreenWidthRight), Random.Range (-ScreenHeight, ScreenHeight), 0.1f);
		CreateParticles(Party, new Color(Random.Range(0.1f, 1f),Random.Range(0.1f, 1f),Random.Range(0.1f, 1f),0), 0.1f, 30); // Shoot randomly coloured particles around
	}
	public void killplayer(GameObject _T){
		if (_T != ship) {
			CreateParticles (_T.transform.position, _T.GetComponent<Renderer> ().material.color, 0.08f, 10);
		} else
			CreateParticles (_T.transform.position, _T.GetComponent<Renderer> ().material.color, 0.08f, 100);
		explosionsound.Play ();
		Destroy (_T.gameObject);
		if (ship == null) {
			CancelInvoke ("CreateEnemies");
		}
	}
	
	public void GameOver(){
		CancelInvoke("CreateEnemies"); //When player is hit by enemie bullet, stop spawning
		foreach (GameObject enemy in EnemiesList)//Clear Screen of enemies
		{
			EnemiesList.Remove(gameObject); //Remove enemy Gameobject from List, also avoids missingexception
			Destroy(enemy.gameObject);
		}
		if (Gamestart == true) {
			Gamestart = false;
		}
		ship = null;
	}
	
	
	public void ResetEnemies(Enemies _tar){
		if(_tar.alive==false){
			Respawn(_tar);
			_tar.Resetpos ();
		}
		else{
			_tar.Resetpos();
		}
		
	}

	public void ChangeScore (int NewScore) // Add Score Method
	{	
		score += NewScore;
	}
	
	public void Message(string _text, Vector3 _trans){
		GameObject GUIPopup = new GameObject ();
		GUIPopup.AddComponent<GUIT> ();
		GUIT G = GUIPopup.GetComponent<GUIT> ();
		G.SetText (_text, _trans);
		Destroy (GUIPopup.gameObject, 2f);
		GUIPopup.transform.parent = ParticleManager.transform;
	}



	void Start () {
		if (Button == null) {
			Button = GameObject.Find ("Toggle");
		}
		CreateBonus (new Vector3 (0, 5, 0));//Spawn Shield for beta purposes

		Screen.SetResolution (480, 700, false, 60);
		//SET AUDIO
		Gamestart = false;
			bulletsound = gameObject.AddComponent<AudioSource> ();//Adding AudioSource Components
			bulletsound.volume = 0.05f;
			explosionsound = gameObject.AddComponent<AudioSource> ();//
			explosionsound.volume = 0.1f;
			pickupsound= gameObject.AddComponent<AudioSource> ();//
			pickupsound.volume = 0.07f;
			pickupaudioclip = (AudioClip)Resources.Load ("Sounds/Pick up");// Loading the tracks from Resources
			bulletaudioclip = (AudioClip)Resources.Load ("Sounds/Shoot1");// Loading the tracks from Resources
			explosionaudioclip = (AudioClip)Resources.Load ("Sounds/Explosion4");//
			bulletsound.clip = bulletaudioclip; //Assigning the bullet clips to the AudioSource Components
			explosionsound.clip = explosionaudioclip;//
			pickupsound.clip = pickupaudioclip;//
			Background ();
			StarManager = new GameObject ();// Contains all stars in a scene
			StarManager.name = "Stars";
			ParticleManager = new GameObject ();// Contains all particles in a scene
			ParticleManager.name = "Particles";
			EnemyManager = new GameObject ();// Contains all enemies in a scene
			EnemyManager.name = "EM";
			mycooldown = 19;//Default bullet speed fire every 0.25 seconds
			EnemySpawnTime = 4.00f;
			Leveltime = 30;
			Level = 1;
			score = 0;
			Player ();
			LoadShip (30);
			CreateStars (5,0.018f, 1.0f, 0f, -20, -50, true); //set _starCount amount here
			CreateStars (100, 0.05f, 0.02f,0, -1, -2, false);//Background stars, false means they will not change scale when they reset 
			ScoreM ();
			InvokeRepeating ("CreateEnemies", 0f, EnemySpawnTime);

	}//End Start
	

	void Update () {

		//CreateEnemies ();
		if (ship) //Ship must exist or not be null for bullets to be fired
		{
			if (cooldown>0)
			{
				cooldown--;
			}
			if (cooldown ==0)
			{
				if(ship.transform.position.y==Targetposition.y){
				FireBullets(ship.transform, 0.4f, true);
				cooldown=mycooldown;
				if (mycooldown<=3)
					mycooldown=3; //max speed of bullets after all bonus' are picked up
				}
			}
			if (debugmode!=true) {
				if (Leveltime <= 0)
				{
					NextLevel();
				} 
				
				else if(ship.transform.position.y==Targetposition.y)
				{
					Gamestart=true;
					Leveltime -= Time.deltaTime;
				}
				else
					Gamestart=false;

			}//end Debug
		}//end ship
		else
			CancelInvoke("CreateEnemies"); //When player is hit by enemie bullet, stop spawning

	}//End Update

}//End Main Class
