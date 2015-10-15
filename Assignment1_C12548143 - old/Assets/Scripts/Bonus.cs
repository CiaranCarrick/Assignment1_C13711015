using UnityEngine;
using System.Collections;

public class Bonus : Main {
	//Move the bonuspick towards ship
	void Start () {

	}
	
	void Update () {
		ResetEnemies ();//when Bonus reachs Bottom of screen, call ResetEnemies from Main
		Move ();
	}


	public void Move(){
		transform.Translate (Vector3.down * 0.05f);
		if (ship != null) {
			float distance = (this.transform.position - ship.transform.position).magnitude;//creates a float which stores position between A & B
			if (distance <= 0.5f) {
				Main.mycooldown-=3; //Decrease Bullet cooldown by 3
				UI.ChangeScore(50); //Increase score for pick up
				Destroy(gameObject);
			}
		}
	}//end Move
}
