using UnityEngine;
using System.Collections;

public class Collisions : Main {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		//COLLISIONS
		for(int i=0; i < EnemiesList.Count; i++){
			GameObject target = EnemiesList[i].gameObject;
			if(target!=null){// Checks to avoid missingexception
				Enemies enemy = target.GetComponent<Enemies>();// creates enemies instance that access referance that allows access to methods and variables within target
				Color enemycolour=target.GetComponent<Renderer>().material.color;// referance colour
				GameObject parts= target.transform.FindChild("p").gameObject; 
				float distance=(transform.position- target.transform.position).magnitude;//creates a float which stores position between 2 variables
				//print(target); //check the distance between two vectors
				if(distance <= 0.5f && gameObject.GetComponent<ShipShoot>().alive==true){// Checks to avoid missingexception
					//print ("done");
					gameObject.SetActive(false);//Disable gameObject aka bullet
					gameObject.GetComponent<ShipShoot>().alive=false;//Disable the bullet
					enemy.SubtractLife(target);//access enemy referance and use Subtract method to take HP away from target
				}

				//Score if killed
				if(enemy.Health==0){
					ChangeScore(enemy.pointvalue);
					enemy.alive=false;
					//parts.transform.parent = null;//Breaks particles away from Enemy
					parts.transform.parent=ParticleManager.transform;
					parts.SetActive(true);
					//enemy.CreateParticles(transform.position, enemycolour, enemy.speed, 20); // Feed in particles spawn area, color and take in speed for effects
					EnemiesList.Remove(target.gameObject); //Remove enemy Gameobject from List, also avoids missingexception
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
	}
}
