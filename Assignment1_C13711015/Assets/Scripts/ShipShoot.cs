using UnityEngine;
using System.Collections;

public class ShipShoot : Main {
	ShipShoot bullet;
	GameObject target;
	Vector3 dir;
	public bool bonus = false;

	public void SetBullet(float _x, float _y, float _z, float _xScale, float _yScale, float _speed,int _mycooldown, bool _alive, Vector3 _dir, bool _bonus) { //Constructor which inherits values from Main ship method
		xPos = _x;
		yPos = _y;
		xScale = _xScale;
		yScale = _yScale;
		Zangle = _z;
		speed = _speed;
		mycooldown = _mycooldown;
		alive = _alive;
		dir = _dir;
		bonus = _bonus;

		transform.eulerAngles = new Vector3 (0, 0, _z);//Rotate bullets to ships gameobject rotation its being shot from

		Vector3 pos = new Vector3 (xPos, yPos, 0.1f);
		transform.position = pos;
		Vector3 scale = new Vector3 (xScale, yScale, 0.1f);
		transform.localScale = scale;

	}
		
	// Use this for initialization
	void Start () {
		bullet = gameObject.GetComponent<ShipShoot> ();
	}


	
	// Update is called once per frame
	void Update () {
		transform.Translate (dir* speed);
		if ((transform.position.y) >= ScreenHeight || transform.position.y <= -ScreenHeight / 2) {//When bullets Y position is greater than ScreensHeight
			gameObject.SetActive (false);//Disable gameObject aka bullet
		}
		//COLLISIONS
		if(bonus && ship !=null){
			for(int i=0; i < EnemiesList.Count; i++){
				target = EnemiesList[i].gameObject;
				if(target!=null){// Checks to avoid missingexception
					Enemies enemy = target.GetComponent<Enemies>();// creates enemies instance that access referance that allows access to methods and variables within target
					float distance=(transform.position- target.transform.position).magnitude;//creates a float which stores position between 2 variables
					//print(target); //check the distance between two vectors
					if(distance <= 0.5f && bullet.alive){// Checks to avoid missingexception
						//print ("done");
						gameObject.SetActive(false);//Disable gameObject aka bullet
						bullet.alive=false;//Disable the bullet
						enemy.SubtractLife(target);//access enemy referance and use Subtract method to take HP away from target
					}
					
					//Score if killed
					if(enemy.Health==0){
						ChangeScore(enemy.pointvalue);
						enemy.alive=false;
						//parts.transform.parent = null;//Breaks particles away from Enemy
						GameObject parts= target.transform.FindChild("p").gameObject; 
						parts.transform.parent=ParticleManager.transform;
						parts.SetActive(true);
						//enemy.CreateParticles(transform.position, enemycolour, enemy.speed, 20); // Feed in particles spawn area, color and take in speed for effects
						EnemiesList.Remove(target.gameObject); //Remove enemy Gameobject from List, also avoids missingexception
						Color enemycolour=target.GetComponent<Renderer>().material.color;// referance colour
						if(enemycolour==EnemyType[1]){
							int spawn=Random.Range(0,10);
							if(spawn<=5){
								return;
							}
							else{
								CreateBonus(target.transform.position);
							}
						}
					}
				}//end target if
			}//end For loop
			//
		}
		if (!bonus && ship != null) {//Enemie bullet collision
			target = ship.gameObject;
			float distance = (ship.transform.position - this.transform.position).magnitude;
			if (distance <= 0.5f && bullet.alive) {// Checks to avoid missingexception
				bullet.gameObject.SetActive (false);
				bullet.alive = false;
				killplayer ();
			}
		}
//
//		if (mycooldown <=5){
//			transform.position += transform.right* Mathf.Sin (Time.time * 10) * 0.02f; //Awesome sine wave
//		}
		//COLLISIONS
	}//end update
}
