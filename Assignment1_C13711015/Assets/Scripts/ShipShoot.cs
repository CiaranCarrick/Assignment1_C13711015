using UnityEngine;
using System.Collections;

public class ShipShoot : Main {
	ShipShoot bullet;
	Vector3 dir;
	public bool PlayerBullet= false;
	public Renderer rend;//Refernace to color
	GameObject c;
	
	public void SetBullet(float _x, float _y, float _z, float _xScale, float _yScale, float _speed,int _mycooldown, bool _alive, Vector3 _dir, bool _Pbullet) { //Constructor which inherits values from Main ship method
		xPos = _x;
		yPos = _y;
		xScale = _xScale;
		yScale = _yScale;
		Zangle = _z;
		speed = _speed;
		mycooldown = _mycooldown;
		alive = _alive;
		dir = _dir;
		PlayerBullet = _Pbullet;
		
		transform.eulerAngles = new Vector3 (0, 0, _z);//Rotate bullets to ships gameobject rotation its being shot from
		
		Vector3 pos = new Vector3 (xPos, yPos, 0.1f);
		transform.position = pos;
		Vector3 scale = new Vector3 (xScale, yScale, 0.1f);
		transform.localScale = scale;
		
	}
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		bullet = gameObject.GetComponent<ShipShoot> ();
	}
	
	
	// Update is called once per frame
	void Update () {
		transform.Translate (dir* speed);
		if ((transform.position.y) >= ScreenHeight-2 || transform.position.y <= (-ScreenHeight/2)-yScale*2) {//When bullets Y position is greater than ScreensHeight
			gameObject.SetActive (false);//Disable gameObject aka bullet
		}
		//COLLISIONS
		if(PlayerBullet && ship !=null){
			for(int i=0; i < EnemiesList.Count; i++){
				_Target = EnemiesList[i].gameObject;
				if(_Target!=null){// Checks to avoid missingexception
					Enemies enemy = _Target.GetComponent<Enemies>();// creates enemies instance that access referance that allows access to methods and variables within _Target
					float distance=(transform.position- _Target.transform.position).magnitude;//creates a float which stores position between 2 variables
					//print(_Target); //check the distance between two vectors
					if(distance <= 0.5f && bullet.alive){// Checks to avoid missingexception
						gameObject.SetActive(false);//Disable gameObject aka bullet
						bullet.alive=false;//Disable the bullet
						enemy.Enemyrend.material.color=Color.red;//
						enemy.SubtractLife(_Target);//access enemy referance and use Subtract method to take HP away from _Target
						
					}
					
					//Score if killed
					if(enemy.Health==0){
						enemy.rend.material.color=enemy.color;//set referance back to original colour so check below for enemytype[1] works
						Message("+"+enemy.pointvalue, enemy.transform.position);
						ChangeScore(enemy.pointvalue);
						enemy.alive=false;
						enemy.Enemy.GetComponent<MeshRenderer>().enabled=false;

						//parts.transform.parent = null;//Breaks particles away from Enemy
						GameObject parts= _Target.transform.FindChild("p").gameObject; 
						parts.transform.parent=ParticleManager.transform;
						parts.SetActive(true);
						//enemy.CreateParticles(transform.position, enemycolour, enemy.speed, 20); // Feed in particles spawn area, color and take in speed for effects
						EnemiesList.Remove(_Target.gameObject); //Remove enemy Gameobject from List, also avoids missingexception
						
						if(enemy.rend.material.color==EnemyType[1]){
							int spawn=Random.Range(0,100);
							if(spawn<=75){
								return;
							}
							else{
								CreateBonus(_Target.transform.position);
							}
						}
					}
				}//end _Target if
			}//end For loop
			//
		}
		if (!PlayerBullet && ship != null) {//Enemie bullet collision
			_Target = ship.gameObject;
			float distance = (_Target.transform.position - this.transform.position).magnitude;
			//print(distance);
			rend.material.SetColor("_TintColor",new Color(166f/255f, 255f/255f, 0f/255f,1));//Greenish goo
			if (distance <= 0.55f && bullet.alive) {// Checks to avoid missingexception
				c = GameObject.Find ("Ship/Shield");
				if(c){
					bullet.gameObject.SetActive (false);
					bullet.alive = false;
					killplayer(c);
					return;
				}
				else
					bullet.gameObject.SetActive (false);
				bullet.alive = false;
				killplayer (_Target);
			}
		}
		if(PlayerBullet){//If Ship shoots the bullet
			rend.material.SetColor("_TintColor",new Color(255f/255f, 255f/255f, 255f/255f,1));// Blue
			if (mycooldown <= 5) {
				transform.position += transform.right * Mathf.Sin (Time.time * 10) * 0.02f; //Awesome sine wave
			}
		}
		//COLLISIONS
	}//end update
}