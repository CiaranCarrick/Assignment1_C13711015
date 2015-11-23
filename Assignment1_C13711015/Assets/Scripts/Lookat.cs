using UnityEngine;
using System.Collections;

public class Lookat : MonoBehaviour{
	public GameObject Target;
	public float Rotatespeed= 40f;// Speed of lock on enemy rotation
	public float Zangle;
	// Use this for initialization
	void Start () {
	
	}
	void look(GameObject _T){
		if (_T == null) { //If no Target, keep searching for one
			if (GameObject.FindGameObjectWithTag ("Player")) {
				_T = GameObject.FindGameObjectWithTag ("Player");// Assigns referance Ship
			} 
			else {
				return; // once found, exit if statement
			}// end else
		}//end _Target
		Vector3 Dir = _T.transform.position - transform.position;// Get a direction Vector from Target to enemy
		Dir.Normalize ();// Normalize it so that it's a unit direction Vector, gives it a size of 1
	
		//ROTATE Enemy ship towards player
		Zangle = Mathf.Atan2 (Dir.y, Dir.x) * Mathf.Rad2Deg - 90; // Draws an angle facing the players position
		Quaternion AngleRotation = Quaternion.Euler (0, 0, Zangle);// Which axis the rotation will take place, in this case the X-Axis
		transform.rotation = Quaternion.RotateTowards (transform.rotation, AngleRotation, Rotatespeed * Time.deltaTime); //How fast enemy rotates towards player
	}
	
	// Update is called once per frame
	void Update () {
		look(Target);
	}
}
