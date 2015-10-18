using UnityEngine;
using System.Collections;

public class ShipShoot : Main {
	
	Vector3 dir;
	public void SetBullet(float _x, float _y, float _z, float _xScale, float _yScale, float _speed,int _mycooldown, bool _alive, Vector3 _dir) { //Constructor which inherits values from Main ship method
		xPos = _x;
		yPos = _y;
		xScale = _xScale;
		yScale = _yScale;
		Zangle = _z;
		speed = _speed;
		mycooldown = _mycooldown;
		alive = _alive;
		dir = _dir;

		transform.eulerAngles = new Vector3 (0, 0, _z);

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
		transform.Translate (dir* speed);
		if ((transform.position.y) >= ScreenHeight) {//When bullets Y position is greater than ScreensHeight
			gameObject.SetActive(false);//Disable gameObject aka bullet
		}

		if (mycooldown <=5){
			transform.position += transform.right* Mathf.Sin (Time.time * 10) * 0.02f; //Awesome sine wave
		}
		//COLLISIONS
	}//end update
}
