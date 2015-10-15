using UnityEngine;
using System.Collections;

public class ShipShoot : Main {
	public Enemies enemy;// Referance to Enemies script

	public void SetBullet(float _x, float _y, float _xScale, float _yScale, float _speed) { //Constructor which inherits values from Main ship method
		xPos = _x;
		yPos = _y;
		xScale = _xScale;
		yScale = _yScale;
		speed = _speed;

		Vector3 pos = new Vector3 (xPos, yPos, 0.1f);
		transform.position = pos;
		Vector3 scale = new Vector3 (xScale, yScale, 0.1f);
		transform.localScale = scale;


	}
		
	// Use this for initialization
	void Start () {
	}
	
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.up * speed);
		if ((transform.position.y) >= ScreenHeight) {
			Destroy (gameObject);
		}
		//COLLISIONS
		for(int i=0; i < Main.EnemiesList.Count; i++){
			GameObject target = Main.EnemiesList[i].gameObject;
			if(target!=null){// Checks to avoid missingexception
			enemy = target.GetComponent<Enemies>();// allows access to methods and varibles within targets constructor

			float distance=(transform.position- target.transform.position).magnitude;//creates a float which stores position between 2 variables
			//Debug.Log (target); //check the distance between two vectors
			if(distance <= 0.5f){
				enemy.SubtractLife(target);//access enemy referance and use Subtract method to take HP away from target
				Destroy(gameObject);//Destroy Bullet
			}
			//Score if killed
				if(enemy.Health==0){
					EnemyCount-=1;// 1 Enemy has been hit, Deduct Health
					CreateParticles(transform.position, target.renderer.material.color, enemy.speed, 20); // Feed in particles spawn area, color and take in speed for effects
					Destroy(target.gameObject); //Destroy Enemy
					EnemiesList.Remove(target.gameObject); //Remove enemy Gameobject from List, also avoids missingexception

					if(target.renderer.material.color==EnemyType[0]){
						UI.ChangeScore(10);//Calls ChangeScore from UI Script
					}
					if(target.renderer.material.color==EnemyType[1]){
						UI.ChangeScore(25);
						int spawn=Random.Range(0,10);
						if(spawn<8)
							return;
						else{
						//BONUS PICK UP 
						GameObject bonus=GameObject.CreatePrimitive(PrimitiveType.Capsule);
						bonus.AddComponent<Bonus>();
						bonus.AddComponent<Strobe>();
						bonus.transform.localScale = new Vector3(0.2f, 0.2f, 0.1f);
						bonus.transform.position=target.transform.position;
						}
					}
					if(target.renderer.material.color==EnemyType[2]){
						UI.ChangeScore(20);
					}
				}
			}//end target if
		}//end For loop
	}//end update
}
