﻿using UnityEngine;
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
				float distance=(transform.position- target.transform.position).magnitude;//creates a float which stores position between 2 variables
				//Debug.Log (target); //check the distance between two vectors
				if(distance <= 0.5f){
					enemy.SubtractLife(target);//access enemy referance and use Subtract method to take HP away from target
					Destroy(gameObject);//Destroy Bullet
				}
				//Score if killed
				if(enemy.Health==0){
					explosionsound.Play();//Play Bullet Sound clip
					CreateParticles(transform.position, enemycolour, enemy.speed, 20); // Feed in particles spawn area, color and take in speed for effects
					EnemiesList.Remove(target.gameObject); //Remove enemy Gameobject from List, also avoids missingexception
					
					if(enemycolour==EnemyType[0]){
						ChangeScore(10);///calls method inherited from main
					}
					if(enemycolour==EnemyType[1]){
						ChangeScore(25);//method inherited from main
						int spawn=Random.Range(0,10);
						if(spawn<=5){
							return;
						}
						else{
							CreateBonus(target.transform.position);
						}
					}
					if(enemycolour==EnemyType[2]){
						ChangeScore(20);//method inherited from main
					}
				}
			}//end target if
		}//end For loop
	}
}
