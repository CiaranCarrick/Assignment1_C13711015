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
				if(target!=null){
				Enemies enemy = target.GetComponent<Enemies>();// creates enemies instance that access referance that allows access to methods and variables within target
				if(target!=null && enemy.alive==true){// Checks to avoid missingexception

				//Color enemycolour=target.GetComponent<Renderer>().material.color;// referance colour
				GameObject parts= target.transform.FindChild("p").gameObject; 
				float distance=(transform.position- target.transform.position).magnitude;//creates a float which stores position between 2 variables
				//Debug.Log (target); //check the distance between two vectors
				if(distance <= 0.5f){
					// Enable renderer accordingly
					enemy.SubtractLife(enemy);//access enemy referance and use Subtract method to take HP away from target
					gameObject.SetActive(false);//Disable gameObject aka bullet
				}
				//Score if killed
				if(enemy.Health==0){
					enemy.alive=false;
					parts.transform.parent = null;//Breaks particles away from Enemy
					parts.transform.parent=ParticleManager.transform;
					parts.SetActive(true);
					//enemy.CreateParticles(transform.position, enemycolour, enemy.speed, 20); // Feed in particles spawn area, color and take in speed for effects
					EnemiesList.Remove(target.gameObject); //Remove enemy Gameobject from List, also avoids missingexception
					if(target.GetComponent<Renderer>().material.color==EnemyType[1]){
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
			}
		}//end For loop
	}
}
